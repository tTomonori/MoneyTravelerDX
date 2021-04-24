using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaserAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が自分の土地の増資コストの平均を下回らなければ購入する
        float tAve = aMaster.mFeild.calculateIncreaseCostAverage(aMyStatus);
        if (aMyStatus.mMoney - aLand.mIncreaseCost < tAve) {
            aCallback(false);
            return;
        }
        aCallback(true);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に増資する
        aCallback(true);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が自分の土地の増資コストの平均を下回らないかつ100を下回らないなら購入する
        float tAve = aMaster.mFeild.calculateIncreaseCostAverage(aMyStatus);
        if (aMyStatus.mMoney - aLand.mAcquisitionCost < tAve || aMyStatus.mMoney - aLand.mAcquisitionCost < 100) {
            aCallback(false);
            return;
        }
        aCallback(true);
    }
    public override void selectIncrease(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //最も増資コストが高い土地に増資する
        LandMass tExpensivest = null;
        foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(aMyStatus)) {
            if (tLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel) continue;
            if (aMyStatus.mMoney < tLand.mIncreaseCost) continue;
            if (tExpensivest == null) {
                tExpensivest = tLand;
                continue;
            }
            if (tExpensivest.mIncreaseCost <= tLand.mIncreaseCost) {
                tExpensivest = tLand;
            }
        }
        aCallback(tExpensivest);
    }
}
