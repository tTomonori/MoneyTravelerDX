using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneRoadJumpMass : JumpMassNumberMass {
    public override SpecialMoveMass getJumpToMass(TravelerStatus aTraveler, GameMaster aMaster) {
        return mJumpToMassList[0];
    }
    public override void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        aTraveler.mComa.moveToWithSpeed(aNextMass.worldPosition, TurnManager.mMoveSpeed, aCallback);
    }
}
