using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SolidAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //所持金が現在の全てのマスの料金の平均を下回らなければ購入する
        if (mMaster.mFeild.calcurateFeeAverage(aMyStatus) < aMyStatus.mMoney - aLand.mPurchaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //所持金が現在の全てのマスの料金の平均を下回らなければ増資する
        if (mMaster.mFeild.calcurateFeeAverage(aMyStatus) < aMyStatus.mMoney - aLand.mIncreaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        //所持金が現在の全てのマスの料金の平均を下回らなければ買収する
        if (mMaster.mFeild.calcurateFeeAverage(aMyStatus, aLand) < aMyStatus.mMoney - aLand.mAcquisitionCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
}
