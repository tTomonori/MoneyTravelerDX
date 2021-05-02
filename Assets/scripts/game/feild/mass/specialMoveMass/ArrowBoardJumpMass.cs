using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBoardJumpMass : JumpMassNumberMass {
    [NonSerialized] public int mCount;
    [SerializeField]
    public ArrowBoard mBoard;

    public override SpecialMoveMass getJumpToMass(TravelerStatus aTraveler, GameMaster aMaster) {
        if (mCount == 1)
            return mJumpToMassList[1];
        else
            return mJumpToMassList[0];
    }
    public override void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        changeBoard(() => {
            moveTraveler(aTraveler, aNextMass, aCallback);
        });
    }
    //看板の表示を更新
    public void changeBoard(Action aCallback) {
        mCount--;
        if (mCount < 0) {
            mCount = (int)(getTravelerNumber() * UnityEngine.Random.Range(2f, 5f));
            mBoard.mNumber.text = mCount.ToString();
            MySoundPlayer.playSe("hit", false);
            MyBehaviour.setTimeoutToIns(1f, () => {
                mBoard.mArrow.rotateZ = 180;
                MySoundPlayer.playSe("drow", false);
                MyBehaviour.setTimeoutToIns(0.6f, aCallback);
            });
            return;
        } else if (mCount == 0) {
            mBoard.mNumber.text = mCount.ToString();
            MySoundPlayer.playSe("hit", false);
            MyBehaviour.setTimeoutToIns(1f, () => {
                mBoard.mArrow.rotateZ = 0;
                MySoundPlayer.playSe("drow", false);
                MyBehaviour.setTimeoutToIns(0.6f, aCallback);
            });
            return;
        } else {
            mBoard.mNumber.text = mCount.ToString();
            MySoundPlayer.playSe("hit", false);
            MyBehaviour.setTimeoutToIns(0.6f, aCallback);
            return;
        }
    }
    //移動演出
    public void moveTraveler(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        aTraveler.mComa.moveToWithSpeed(aNextMass.worldPosition, TurnManager.mMoveSpeed, aCallback);
    }
    //トラベラーの人数を数える
    public int getTravelerNumber() {
        int tNumber = 0;
        foreach (TravelerData tData in GameData.mGameSetting.mTravelerData) {
            if (tData != null)
                tNumber++;
        }
        return tNumber;
    }
}
