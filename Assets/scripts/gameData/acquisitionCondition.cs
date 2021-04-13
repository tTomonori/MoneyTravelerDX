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
}