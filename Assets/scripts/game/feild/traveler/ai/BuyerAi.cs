using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に購入する
        aCallback(true);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所有者がいる土地が8割を超えている場合増資する
        List<LandMass> tAllLands = aMaster.mFeild.getAllLands();
        List<LandMass> tOwnedLands = aMaster.mFeild.getOtherOwnedLand(null);
        if (tOwnedLands.Count / (float)(tAllLands.Count) > 0.8f) {
            aCallback(true);
            return;
        }
        //所持金が所有者がいないマスの購入価格の平均を下回らなければ増資する
        if (aMaster.mFeild.calculatePriceAverage() < aMyStatus.mMoney - aLand.mIncreaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に買収する
        aCallback(true);
    }
}
