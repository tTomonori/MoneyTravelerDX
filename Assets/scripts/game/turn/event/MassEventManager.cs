using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class MassEventManager {
    static public void runPassEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        switch (aMaster.mFeild.mMassList[aTraveler.mCurrentMassNumber]) {
            case LandMass tLand:
                aCallback();
                return;
            case EventMass tEvent:
                aCallback();
                return;
            case CheckPointMass tCheck:
                CheckPointMassEventManager.runPassEvent(aTraveler, tCheck, aMaster, aCallback);
                return;
            case StartMass tStart:
                StartMassEventManager.runPassEvent(aTraveler, tStart, aMaster, aCallback);
                return;
        }
    }
    static public void runStopEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        switch (aMaster.mFeild.mMassList[aTraveler.mCurrentMassNumber]) {
            case LandMass tLand:
                LandMassEventManager.runStopEvent(aTraveler, tLand, aMaster, aCallback);
                return;
            case EventMass tEvent:
                aCallback();
                return;
            case CheckPointMass tCheck:
                CheckPointMassEventManager.runStopEvent(aTraveler, tCheck, aMaster, aCallback);
                return;
            case StartMass tStart:
                StartMassEventManager.runStopEvent(aTraveler, tStart, aMaster, aCallback);
                break;
        }
    }
}
