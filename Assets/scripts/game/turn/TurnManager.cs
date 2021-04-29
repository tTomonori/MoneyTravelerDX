using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager {
    static public readonly float mMoveSpeed = 8;
    public GameMaster mMaster;
    //ターン開始
    public void startTurn(TravelerStatus aTurnTraveler, Action aCallback) {
        mMaster.mUiMain.animateTurnStart(aTurnTraveler, () => { });
        mMaster.mCamera.mTarget = aTurnTraveler.mComa;
        mMaster.tweakComaPosition(aTurnTraveler, () => {
            //ダイス
            mMaster.mUiMain.setDice((aDiceManager) => {
                aTurnTraveler.mAi.rollDice(aDiceManager, mMaster);
            }, (aNumber) => {
                aTurnTraveler.mAi.endRollDice();
                move(aTurnTraveler, aNumber, aCallback);
            });
        });
    }
    //指定マス数移動する
    private void move(TravelerStatus aTurnTraveler, int aNumber, Action aCallback) {
        aTurnTraveler.mComa.mNumberMesh.text = aNumber.ToString();
        moveToNextMass(aTurnTraveler, () => {
            //マスに止まった
            if (aNumber == 1) {
                aTurnTraveler.mComa.mNumberMesh.text = "";
                MassEventManager.runStopEvent(aTurnTraveler, mMaster.mFeild.mMassList[aTurnTraveler.mCurrentMassNumber], mMaster, aCallback);
                return;
            }
            //マスを通過する
            aTurnTraveler.mComa.mNumberMesh.text = (aNumber - 1).ToString();
            MassEventManager.runPassEvent(aTurnTraveler, mMaster.mFeild.mMassList[aTurnTraveler.mCurrentMassNumber], mMaster, () => {
                if (aTurnTraveler.mIsRetired) {
                    aCallback();
                    return;
                }
                GameData.mStageData.mCamera.mTarget = aTurnTraveler.mComa;
                move(aTurnTraveler, aNumber - 1, aCallback);
            });
        });
    }
    //次のマスへ移動させる(空マスは通過する)
    private void moveToNextMass(TravelerStatus aTraveler, Action aCallback) {
        int tNextMassNumber = (aTraveler.mCurrentMassNumber + 1) % mMaster.mFeild.mMassList.Count;
        GameMass tCurrentMass = mMaster.mFeild.mMassList[aTraveler.mCurrentMassNumber].getNotShared();
        GameMass tNextMass = mMaster.mFeild.mMassList[tNextMassNumber].getNotShared();
        if (!(tCurrentMass is SpecialMoveMass && tNextMass is SpecialMoveMass)) {
            walkToNextMass(aTraveler, aCallback);
            return;
        }
        //特殊な移動演出のマス
        if (tCurrentMass is JumpMassNumberMass) {
            JumpMassNumberMass tJumpMass = (JumpMassNumberMass)tCurrentMass;
            JumpMassNumberMass tNextJumpMass = (JumpMassNumberMass)tJumpMass.getJumpToMass(aTraveler, mMaster);
            tJumpMass.effectMove(aTraveler, tNextJumpMass, () => {
                aTraveler.mCurrentMassNumber = mMaster.mFeild.mMassList.IndexOf(tNextJumpMass);
                moveToNextMass(aTraveler, aCallback);
            });
            return;
        }
        ((SpecialMoveMass)tCurrentMass).effectMove(aTraveler, (SpecialMoveMass)tNextMass, () => {
            aTraveler.mCurrentMassNumber = tNextMassNumber;
            moveToNextMass(aTraveler, aCallback);
        });
    }
    private void walkToNextMass(TravelerStatus aTraveler, Action aCallback) {
        int tNextMassNumber = (aTraveler.mCurrentMassNumber + 1) % mMaster.mFeild.mMassList.Count;
        GameMass tNextMass = mMaster.mFeild.mMassList[tNextMassNumber].getNotShared();
        aTraveler.mComa.moveToWithSpeed(tNextMass.worldPosition, mMoveSpeed, () => {
            aTraveler.mCurrentMassNumber = tNextMassNumber;
            if (tNextMass is EmptyMass || tNextMass is SpecialMoveMass) {
                moveToNextMass(aTraveler, aCallback);
                return;
            }
            MySoundPlayer.playSe("step");
            aTraveler.moved(1);
            aCallback();
        });
    }
}
