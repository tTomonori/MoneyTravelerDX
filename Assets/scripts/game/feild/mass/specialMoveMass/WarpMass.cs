using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpMass : SpecialMoveMass {
    public override void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        MySoundPlayer.playSe("warp", true);
        GameEffector.generateAura(this.worldPosition, new Color(0.5f, 1, 1), () => { });
        MyBehaviour.setTimeoutToIns(1f, () => {
            aTraveler.mComa.position = aNextMass.worldPosition;
            MySoundPlayer.playSe("warp", true);
            GameEffector.generateAura(aNextMass.worldPosition, new Color(0.5f, 1, 1), () => { });
            MyBehaviour.setTimeoutToIns(1f, () => {
                aCallback();
            });
        });
    }
}
