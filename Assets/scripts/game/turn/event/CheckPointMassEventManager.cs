using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class CheckPointMassEventManager {
    static public void runStopEvent(TravelerStatus aTraveler, CheckPointMass aCheck, GameMaster aMaster, Action aCallback) {
        GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + aCheck.mStopBonus.ToString(), () => {
            aTraveler.getMoney(aCheck.mStopBonus);
            aMaster.updateStatusDisplay();
            aCallback();
        });
    }
    static public void runPassEvent(TravelerStatus aTraveler, CheckPointMass aCheck, GameMaster aMaster, Action aCallback) {
        GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + aCheck.mPassBonus.ToString(), () => {
            aTraveler.getMoney(aCheck.mPassBonus);
            aMaster.updateStatusDisplay();
            aCallback();
        });
    }
}
