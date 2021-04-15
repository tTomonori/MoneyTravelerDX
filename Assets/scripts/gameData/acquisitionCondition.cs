using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AcquisitionCondition {
    none, always, soldOut, increaseMax
}

static public class AcquisitionConditionMethods {
    static public string getName(this AcquisitionCondition e) {
        switch (e) {
            case AcquisitionCondition.none:
                return "常に不可";
            case AcquisitionCondition.always:
                return "常に可能";
            case AcquisitionCondition.soldOut:
                return "土地完売";
            case AcquisitionCondition.increaseMax:
                return "増資MAX";
        }
        throw new Exception();
    }
    static public bool canAcquisition(this AcquisitionCondition e, GameMaster aMaster, TravelerStatus aTraveler) {
        switch (e) {
            case AcquisitionCondition.none:
                return false;
            case AcquisitionCondition.always:
                return true;
            case AcquisitionCondition.soldOut:
                foreach (GameMass tMass in aMaster.mFeild.mMassList) {
                    if (!(tMass is LandMass)) continue;
                    LandMass tLand = (LandMass)tMass;
                    if (tLand.mOwner == null)
                        return false;
                }
                return true;
            case AcquisitionCondition.increaseMax:
                foreach (GameMass tMass in aMaster.mFeild.mMassList) {
                    if (!(tMass is LandMass)) continue;
                    LandMass tLand = (LandMass)tMass;
                    if (tLand.mOwner == null)
                        return false;
                    if (tLand.mOwner == aTraveler && tLand.mIncreaseLevel != LandMass.mMaxIncreaseLevel)
                        return false;
                }
                return true;
        }
        throw new Exception();
    }
}