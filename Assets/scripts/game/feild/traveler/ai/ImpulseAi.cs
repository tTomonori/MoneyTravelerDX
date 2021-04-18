﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に購入する
        aCallback(true);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に増資する
        aCallback(true);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に買収する
        aCallback(true);
    }
    public override void selectIncrease(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //最も増資コストが高い土地に増資する
        LandMass tTarget = null;
        foreach(LandMass tLand in aMaster.mFeild.getOwnedLand(aMyStatus)) {
            if (tLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel) continue;
            if (aMyStatus.mMoney < tLand.mIncreaseCost) continue;
            if (tTarget == null) {
                tTarget = tLand;
                continue;
            }
            if (tTarget.mIncreaseCost < tLand.mIncreaseCost) {
                tTarget = tLand;
                continue;
            }
        }
        aCallback(tTarget);
    }
}
