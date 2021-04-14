using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager {
    public GameMaster mMaster;
    //ターン開始
    public void startTurn(TravelerStatus aTurnTraveler, Action aCallback) {
        mMaster.mUiMain.animateTurnStart(aTurnTraveler, () => { });
        mMaster.mCamera.mTarget = aTurnTraveler.mComa;
        mMaster.tweakComaPosition(aTurnTraveler, () => {
            //ダイス
            mMaster.mUiMain.setDice((aDiceManager) => {
                aTurnTraveler.mAi.rollDice(aDiceManager);
            },(aNumber)=> {
                aTurnTraveler.mAi.endRollDice();

            });
        });
    }
}
