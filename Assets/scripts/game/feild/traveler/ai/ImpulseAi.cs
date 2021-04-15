using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //常に購入する
        aCallback(true);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //常に増資する
        aCallback(true);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //常に買収する
        aCallback(true);
    }
}
