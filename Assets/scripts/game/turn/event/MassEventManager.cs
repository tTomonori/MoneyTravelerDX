using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class MassEventManager {
    static public void runPassEvent(TravelerStatus aTraveler, GameMass aMass, GameMaster aMaster, Action aCallback) {
        switch (aMass) {
            case LandMass tLand:
                aCallback();
                return;
            case EventMass tEvent:
                if (tEvent.mRunType == EventMassRunType.pass) {
                    EventMassEventManager.run(aTraveler, tEvent, aMaster, aCallback);
                    return;
                }
                aCallback();
                return;
            case CheckPointMass tCheck:
                CheckPointMassEventManager.runPassEvent(aTraveler, tCheck, aMaster, aCallback);
                return;
            case StartMass tStart:
                StartMassEventManager.runPassEvent(aTraveler, tStart, aMaster, aCallback);
                return;
            case ShareMass tShare:
                if (tShare.mSharedMass is ShareMass)
                    throw new Exception();
                runPassEvent(aTraveler, tShare.mSharedMass, aMaster, aCallback);
                return;
        }
    }
    static public void runStopEvent(TravelerStatus aTraveler, GameMass aMass, GameMaster aMaster, Action aCallback) {
        switch (aMass) {
            case LandMass tLand:
                LandMassEventManager.runStopEvent(aTraveler, tLand, aMaster, aCallback);
                return;
            case EventMass tEvent:
                EventMassEventManager.run(aTraveler, tEvent, aMaster, aCallback);
                return;
            case CheckPointMass tCheck:
                CheckPointMassEventManager.runStopEvent(aTraveler, tCheck, aMaster, aCallback);
                return;
            case StartMass tStart:
                StartMassEventManager.runStopEvent(aTraveler, tStart, aMaster, aCallback);
                break;
            case ShareMass tShare:
                if (tShare.mSharedMass is ShareMass)
                    throw new Exception();
                runStopEvent(aTraveler, tShare.mSharedMass, aMaster, aCallback);
                return;
        }
    }
}
