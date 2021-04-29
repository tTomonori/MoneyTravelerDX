using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryLiftMass : JumpMassNumberMass {
    [SerializeField]
    public FactoryLift mLift;
    public override SpecialMoveMass getJumpToMass(TravelerStatus aTraveler, GameMaster aMaster) {
        return mJumpToMassList[UnityEngine.Random.Range(0, mJumpToMassList.Count)];
    }
    public override void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        mLift.moveLift(aTraveler.mComa.positionY, () => {
            aTraveler.mComa.moveToWithSpeed(mLift.worldPosition, TurnManager.mMoveSpeed, () => {
                mLift.moveLift(aNextMass.positionY, () => { });
                aTraveler.mComa.moveToWithSpeed(new Vector3(mLift.positionX, aNextMass.positionY, mLift.positionZ), FactoryLift.mSpeed, () => {
                    aTraveler.mComa.moveToWithSpeed(aNextMass.worldPosition, TurnManager.mMoveSpeed, () => {
                        aCallback();
                    });
                });
            });
        });
    }
}
