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
                move(aTurnTraveler, aNumber, aCallback);
            });
        });
    }
    //指定マス数移動する
    private void move(TravelerStatus aTurnTraveler,int aNumber,Action aCallback) {
        aTurnTraveler.mComa.mNumberMesh.text = aNumber.ToString();
        moveToNextMass(aTurnTraveler, () => {
            //マスに止まった
            if (aNumber == 1) {
                aTurnTraveler.mComa.mNumberMesh.text = "";
                MassEventManager.runStopEvent(aTurnTraveler, mMaster, aCallback);
                return;
            }
            //マスを通過する
            MassEventManager.runPassEvent(aTurnTraveler, mMaster, () => {
                move(aTurnTraveler, aNumber - 1, aCallback);
            });
        });
    }
    //次のマスへ移動させる(空マスは通過する)
    private void moveToNextMass(TravelerStatus aTraveler,Action aCallback) {
        int tNextMassNumber = (aTraveler.mCurrentMassNumber + 1) % mMaster.mFeild.mMassList.Count;
        aTraveler.mComa.moveToWithSpeed(mMaster.mFeild.mMassList[tNextMassNumber].worldPosition, 8, () => {
            aTraveler.mCurrentMassNumber = tNextMassNumber;
            if(mMaster.mFeild.mMassList[tNextMassNumber] is EmptyMass) {
                moveToNextMass(aTraveler, aCallback);
                return;
            }
            MySoundPlayer.playSe("step");
            aTraveler.moved(1);
            aCallback();
        });
    }
}
