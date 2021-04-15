using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public class StartMassEventManager {
    static public void runStopEvent(TravelerStatus aTraveler, StartMass aStart, GameMaster aMaster, Action aCallback) {
        GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + aStart.mStopBonus.ToString(), () => {
            aTraveler.getMoney(aStart.mStopBonus);
            aTraveler.mOrbit++;
            aMaster.updateStatusDisplay();
            aCallback();
        });
    }
    static public void runPassEvent(TravelerStatus aTraveler, StartMass aStart, GameMaster aMaster, Action aCallback) {
        GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + aStart.mPassBonus.ToString(), () => {
            aTraveler.getMoney(aStart.mPassBonus);
            aTraveler.mOrbit++;
            aMaster.updateStatusDisplay();
            aCallback();
        });
    }
}
