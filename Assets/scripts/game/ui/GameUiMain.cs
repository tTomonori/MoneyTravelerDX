using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameUiMain : MyBehaviour {
    public MyBehaviour mTravelerStatusDisplayContainer;
    public List<TravelerStatusDisplay> mTravelerStatusDisplays;
    public TurnClock mTurnClock;
    //ターンの順番を決める
    public void decideTurnOrder(List<TravelerStatus> aTravelers, Action<List<TravelerStatus>> aCallback) {
        //順番を決める
        List<TravelerStatus> tTurnOrder = new List<TravelerStatus>();
        foreach (TravelerStatus tTraveler in aTravelers) {
            if (tTraveler == null) continue;
            tTurnOrder.Add(tTraveler);
        }
        tTurnOrder = tTurnOrder.OrderBy(i => Guid.NewGuid()).ToList();
        //演出
        TurnOrderAnimator tAnimator = this.createChild<TurnOrderAnimator>("turnOrderAnimator");
        tAnimator.animate(aTravelers, tTurnOrder, () => {
            tAnimator.delete();
            aCallback(tTurnOrder);
        });
    }
    //トラベラーのステータスを表示
    public void displayStatus(List<TravelerStatus> aTravelersTurnOrder, Action aCallback) {
        (mTravelerStatusDisplayContainer, mTravelerStatusDisplays) = TravelerStatusDisplayFactory.create(aTravelersTurnOrder);
        mTravelerStatusDisplayContainer.transform.SetParent(this.transform, false);
        mTravelerStatusDisplayContainer.position2D = new Vector2(0, -4);
        mTravelerStatusDisplayContainer.moveTo(new Vector2(0, 0), 0.5f, aCallback);
    }
    //トラベラーのステータスを更新
    public void updateStatus(List<TravelerStatus> aTravelers) {
        foreach (TravelerStatus tTraveler in aTravelers) {
            if (tTraveler == null) continue;
            mTravelerStatusDisplays[tTraveler.mPlayerNumber - 1].updateStatus(tTraveler);
        }
    }
    //ターン開始演出
    public void animateTurnStart(TravelerStatus aTurnTraveler, Action aCallback) {
        TurnEnvelope tEnvelope = TurnEnvelope.create(aTurnTraveler);
        tEnvelope.transform.SetParent(this.transform, false);
        tEnvelope.animate(() => {
            tEnvelope.delete();
            aCallback();
        });
    }
    //ダイス
    public void setDice(Action<DiceManager> aPrepared, Action<int> aEnd) {
        DiceManager tManager = null;
        DiceManager.setDice(this.gameObject, (aManager) => {
            tManager = aManager;
            aPrepared(aManager);
        }, (aNum) => {
            tManager.delete();
            aEnd(aNum);
        });
    }
    //マスのステータス表示
    public MassStatusDisplay displayMassStatus(GameMass aMass, List<MassStatusUiButtonData> aButtonDataList) {
        MassStatusDisplay tDisplay = MassStatusDisplay.create(aMass, aButtonDataList);
        tDisplay.transform.SetParent(this.transform, false);
        tDisplay.position2D = new Vector2(0, 1.5f);
        return tDisplay;
    }
    //hangingBoard
    public HangingBoard displayHangingBoard(HangingBoard.BoardImage aBoardImage) {
        HangingBoard tBoard = HangingBoard.create(aBoardImage);
        tBoard.transform.SetParent(this.transform, false);
        return tBoard;
    }
    //イベント内容表示
    public void displayEventDescription(string aText, Action aCallback) {
        EventDocuments tDocuments = EventDocuments.create(aText);
        tDocuments.transform.SetParent(this.transform, false);
        tDocuments.positionZ = 2;
        tDocuments.animateOpen(() => {
            MyBehaviour.setTimeoutToIns(1.2f, () => {
                tDocuments.delete();
                aCallback();
            });
        });
    }
    //ターンクロック生成
    public void createTurnDisplay() {
        mTurnClock = TurnClock.create();
    }
    //ターンクロック更新
    public void updateTurnDisplay(string tText) {
        mTurnClock.setTurn(tText);
    }
    //所有地リスト表示
    public LandOwnedDisplay displayLandOwnedDisplay(TravelerStatus aTraveler, List<LandMass> aLans, Action<LandMass> aDetailButtonPushed, List<MassStatusUiButtonData> aUiButtondata) {
        LandOwnedDisplay tDisplay = LandOwnedDisplay.create(aTraveler, aLans, aDetailButtonPushed, aUiButtondata);
        tDisplay.transform.SetParent(this.transform, false);
        tDisplay.position2D = new Vector2(0, 1.5f);
        return tDisplay;
    }
}
