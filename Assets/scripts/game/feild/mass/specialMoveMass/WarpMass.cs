using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpMass : SpecialMoveMass {
    public override void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        MySoundPlayer.playSe("warp",true);
        MyBehaviour.setTimeoutToIns(1f, () => {
            aTraveler.mComa.position = aNextMass.worldPosition;
            MySoundPlayer.playSe("warp", true);
            MyBehaviour.setTimeoutToIns(1f, () => {
                aCallback();
            });
        });
    }
}
