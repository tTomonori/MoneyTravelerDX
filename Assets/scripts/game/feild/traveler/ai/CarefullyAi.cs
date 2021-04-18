using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarefullyAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が現在の最も料金が高いマスの料金を下回らなければ購入する
        LandMass tLand = aMaster.mFeild.searchExpensivestFeeLand(aMyStatus);
        if (tLand == null) {
            aCallback(true);
            return;
        }
        if (tLand.mFeeCost < aMyStatus.mMoney - aLand.mPurchaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が現在の最も料金が高いマスの料金を下回らなければ増資する
        LandMass tLand = aMaster.mFeild.searchExpensivestFeeLand(aMyStatus);
        if (tLand == null) {
            aCallback(true);
            return;
        }
        if (tLand.mFeeCost < aMyStatus.mMoney - aLand.mIncreaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が現在の最も料金が高いマスの料金を下回らなければ買収する
        LandMass tLand = aMaster.mFeild.searchExpensivestFeeLand(aMyStatus, aLand);
        if (tLand == null) {
            aCallback(true);
            return;
        }
        if (tLand.mFeeCost < aMyStatus.mMoney - aLand.mAcquisitionCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void selectIncrease(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //所持金が現在の最も料金が高いマスの料金を下回らなければ最も増資コストが安い土地に増資する
        LandMass tHighestFeeLand = aMaster.mFeild.searchExpensivestFeeLand(aMyStatus);
        int tHighestFee = (tHighestFeeLand == null) ? 0 : tHighestFeeLand.mFeeCost;
        LandMass tTarget = null;
        foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(aMyStatus)) {
            if (tLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel) continue;
            if (tHighestFee > aMyStatus.mMoney - tLand.mIncreaseCost) continue;
            if (tTarget == null) {
                tTarget = tLand;
                continue;
            }
            if (tTarget.mIncreaseCost > tLand.mIncreaseCost) {
                tTarget = tLand;
                continue;
            }
        }
        aCallback(tTarget);
    }
}
