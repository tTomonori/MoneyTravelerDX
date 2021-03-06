using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameMaster {
    public GameFeild mFeild;
    public GameCamera mCamera;
    public GameUiMain mUiMain;
    public List<TravelerStatus> mTravelers;
    public List<TravelerStatus> mTurnOrder;
    public int mTurnIndex = -1;
    public TurnManager mTurnManager;
    public int mTurnNumber = 1;
    public Action mEndCallback;
    public GameMaster(GameFeild aFeild, GameCamera aCamera) {
        mFeild = aFeild;
        mCamera = aCamera;
    }
    public void prepare(Action aCallback) {
        foreach (GameMass tMass in mFeild.mMassList)
            tMass.initialize();
        mTravelers = TravelerFactory.create(mFeild);
        mTurnManager = new TurnManager();
        mTurnManager.mMaster = this;
        //トラベラーのうちの一人を映す
        foreach (TravelerStatus tTraveler in mTravelers) {
            if (tTraveler == null) continue;
            mCamera.shoot(tTraveler.mComa);
            tweakComaPosition(tTraveler, aCallback);
            break;
        }
    }
    public void gameStart(Action aEndCallback) {
        mEndCallback = aEndCallback;
        //uiのシーンを開く
        MySceneManager.openScene("gameUi", null, (aScene) => {
            mUiMain = GameObject.Find("gameUiMain").GetComponent<GameUiMain>();
            //ターンの順番を決める
            mUiMain.decideTurnOrder(mTravelers, (aTurnOrder) => {
                mTurnOrder = aTurnOrder;
                distributeInitialMoney();
                //ステータス表示
                MyBehaviour.setTimeoutToIns(0.5f, () => {
                    mUiMain.displayStatus(mTurnOrder, () => {
                        //ターン表示
                        mUiMain.createTurnDisplay();
                        mUiMain.updateTurnDisplay(mTurnNumber.ToString());
                        //ターン開始
                        TravelerStatus.mRetiredCounter = 0;
                        recordAssetsTransition();
                        nextTurn();
                    });
                });
            });
        });
    }
    //初期所持金を配布
    private void distributeInitialMoney() {
        foreach (TravelerStatus tTraveler in mTurnOrder) {
            tTraveler.setInitialMoney((int)(GameData.mStageData.mInitialMoney * GameData.mGameSetting.mInitialMoney));
        }
    }
    //次のターン開始
    private void nextTurn() {
        //終了判定
        if (GameData.mGameSetting.mBattleMethod.isFinish(this)) {
            endGame();
            return;
        }
        //次のターンのトラベラーを探す
        int tPreTurnIndex = mTurnIndex;
        mTurnIndex = (mTurnIndex + 1) % mTurnOrder.Count;
        while (mTurnOrder[mTurnIndex].mIsRetired)
            mTurnIndex = (mTurnIndex + 1) % mTurnOrder.Count;

        if (mTurnIndex < tPreTurnIndex) {
            mTurnNumber++;
            recordAssetsTransition();
            mUiMain.updateTurnDisplay(mTurnNumber.ToString());
        }
        mTurnManager.startTurn(mTurnOrder[mTurnIndex], this.nextTurn);
    }
    //ゲーム終了
    private void endGame() {
        recordAssetsTransition();
        mEndCallback();
    }
    //ステータス表示更新
    public void updateStatusDisplay() {
        updateRanking();
        mUiMain.updateStatus(mTurnOrder);
    }
    //トラベラーの順位更新
    public void updateRanking() {
        foreach (TravelerStatus tTraveler in mTurnOrder) {
            if (tTraveler.mIsRetired) continue;
            int tRanking = 1;
            foreach (TravelerStatus tComparison in mTurnOrder) {
                if (tComparison.mIsRetired) continue;
                if (tTraveler.mAssets < tComparison.mAssets) {
                    tRanking++;
                }
            }
            tTraveler.mRanking = tRanking;
        }
    }
    //総資産の推移を記録
    public void recordAssetsTransition() {
        foreach (TravelerStatus aTraveler in mTurnOrder) {
            aTraveler.recordAssetsTransition();
        }
    }
    //同じマス内でキャラが重ならないように移動して調整
    public void tweakComaPosition(TravelerStatus aTurnTraveler, Action aCallback) {
        List<(int, int)> tNums = new List<(int, int)>();//ターンの人を除き(同じマスにいる人数,同じマスにいる人の中での番号)
        foreach (TravelerStatus tTraveler in mTravelers) {
            if (tTraveler == null || tTraveler.mIsRetired) {
                tNums.Add((-1, -1));
                continue;
            }
            if (tTraveler == aTurnTraveler) {
                tNums.Add((0, 0));
                continue;
            }
            (int, int) tNum = (0, 0);
            foreach (TravelerStatus tComparison in mTravelers) {
                if (tComparison == null || tComparison.mIsRetired || tComparison == aTurnTraveler)
                    continue;
                if (mFeild.getMassNumberConsiderShareMass(tComparison) != mFeild.getMassNumberConsiderShareMass(tTraveler))
                    continue;
                tNum.Item1++;
                if (tComparison.mPlayerNumber <= tTraveler.mPlayerNumber)
                    tNum.Item2++;
            }
            tNums.Add(tNum);
        }
        CallbackSystem tSystem = new CallbackSystem();
        for (int i = 0; i < mTravelers.Count; i++) {
            (int, int) tRes = tNums[i];
            if (tRes.Item1 == -1) continue;
            mTravelers[i].mComa.moveTo(mFeild.mMassList[mTravelers[i].mCurrentMassNumber].worldPosition + getTweakComaPosition(tRes.Item1, tRes.Item2), 0.2f, tSystem.getCounter());
        }
        tSystem.then(aCallback);
    }
    //マス内での相対待機座標
    public Vector3 getTweakComaPosition(int aNumberOfTraveler, int aNumberOfOrder) {
        switch (aNumberOfTraveler) {
            case -1://null or retire
                break;
            case 0://ターンのトラベラー
                return new Vector3(0, 0, 0);
            case 1:
                return new Vector3(1f, 0, 0.5f);
            case 2:
                switch (aNumberOfOrder) {
                    case 1:
                        return new Vector3(-1f, 0, 0.5f);
                    case 2:
                        return new Vector3(1f, 0, 0.5f);
                }
                break;
            case 3:
                switch (aNumberOfOrder) {
                    case 1:
                        return new Vector3(-1f, 0, 0.5f);
                    case 2:
                        return new Vector3(1.2f, 0, 0.3f);
                    case 3:
                        return new Vector3(0.6f, 0, 0.5f);
                }
                break;
            case 4:
                switch (aNumberOfOrder) {
                    case 1:
                        return new Vector3(-1.2f, 0, 0.3f);
                    case 2:
                        return new Vector3(-0.6f, 0, 0.5f);
                    case 3:
                        return new Vector3(0.6f, 0, 0.5f);
                    case 4:
                        return new Vector3(1.2f, 0, 0.3f);
                }
                break;
            case 5:
                switch (aNumberOfOrder) {
                    case 1:
                        return new Vector3(-1.2f, 0, 0.3f);
                    case 2:
                        return new Vector3(-0.6f, 0, 0.5f);
                    case 3:
                        return new Vector3(0.4f, 0, 0.7f);
                    case 4:
                        return new Vector3(0.9f, 0, 0.5f);
                    case 5:
                        return new Vector3(1.4f, 0, 0.3f);
                }
                break;
        }
        throw new Exception(aNumberOfTraveler.ToString() + ":" + aNumberOfOrder);
    }
}
