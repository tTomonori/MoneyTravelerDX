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
            runOrbitEvent(aTraveler, aMaster, aCallback);
        });
    }
    static public void runPassEvent(TravelerStatus aTraveler, StartMass aStart, GameMaster aMaster, Action aCallback) {
        GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + aStart.mPassBonus.ToString(), () => {
            aTraveler.getMoney(aStart.mPassBonus);
            aTraveler.mOrbit++;
            aMaster.updateStatusDisplay();
            runOrbitEvent(aTraveler, aMaster, aCallback);
        });
    }
    static public void runEvent(TravelerStatus aTraveler, StartMass aStart, GameMaster aMaster, Action aCallback) {
        GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + aStart.mPassBonus.ToString(), () => {
            aTraveler.getMoney(aStart.mPassBonus);
            aTraveler.mOrbit++;
            aMaster.updateStatusDisplay();
            runOrbitEvent(aTraveler, aMaster, aCallback);
        });
    }
    static public void runOrbitEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        if (!GameData.mGameSetting.mOrbitEventType.canRunEvent(aTraveler, aMaster)) {
            aCallback();
            return;
        }
        OrbitEventManager.run(aTraveler, aMaster, aCallback);
    }
}
