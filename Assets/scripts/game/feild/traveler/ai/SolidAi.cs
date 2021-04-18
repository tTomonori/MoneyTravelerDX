using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SolidAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が現在の全てのマスの料金の平均を下回らなければ購入する
        if (aMaster.mFeild.calcurateFeeAverage(aMyStatus) < aMyStatus.mMoney - aLand.mPurchaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が現在の全てのマスの料金の平均を下回らなければ増資する
        if (aMaster.mFeild.calcurateFeeAverage(aMyStatus) < aMyStatus.mMoney - aLand.mIncreaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が現在の全てのマスの料金の平均を下回らなければ買収する
        if (aMaster.mFeild.calcurateFeeAverage(aMyStatus, aLand) < aMyStatus.mMoney - aLand.mAcquisitionCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void selectIncrease(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //所持金が現在の全てのマスの料金の平均を下回らない最も価値が高い土地に増資する
        float tFeeAve = aMaster.mFeild.calcurateFeeAverage(aMyStatus);
        LandMass tTarget = null;
        foreach(LandMass tLand in aMaster.mFeild.getOwnedLand(aMyStatus)) {
            if (tLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel) continue;
            if (tFeeAve > aMyStatus.mMoney - tLand.mIncreaseCost) continue;
            if (tTarget == null) {
                tTarget = tLand;
                continue;
            }
            if (tTarget.mTotalValue < tLand.mTotalValue) {
                tTarget = tLand;
                continue;
            }
        }
        aCallback(tTarget);
    }
}
