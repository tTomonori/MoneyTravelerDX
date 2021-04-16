using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class TravelerStatusAdministrator {
    //初期所持金設定
    static public void setInitialMoney(this TravelerStatus aTraveler, int aInitialMoney) {
        aTraveler.mMoney = aInitialMoney;
        aTraveler.mMaxAssets = aInitialMoney;
    }
    //最大総資産更新
    static public void updateMaxAssets(this TravelerStatus aTraveler) {
        if (aTraveler.mAssets > aTraveler.mMaxAssets)
            aTraveler.mMaxAssets = aTraveler.mAssets;
    }
    //ボーナスマネー取得
    static public void getMoney(this TravelerStatus aTraveler, int aMoney) {
        aTraveler.mMoney += aMoney;
        aTraveler.updateMaxAssets();
    }
    //イベント支出
    static public void lostMoney(this TravelerStatus aTraveler, int aMoney) {
        aTraveler.mMoney -= aMoney;
    }
    //災害被害
    static public void disasterDamage(this TravelerStatus aTraveler, int aMoney) {
        aTraveler.mMoney -= aMoney;
        aTraveler.mDisasterDamageAmount += aMoney;
    }
    //移動した
    static public void moved(this TravelerStatus aTraveler, int aNumber = 1) {
        aTraveler.mMoveDistance += aNumber;
    }
    //土地を購入した
    static public void purchased(this TravelerStatus aTraveler, LandMass aLand) {
        aTraveler.mMoney -= aLand.mPurchaseCost;
        aTraveler.mProperty += aLand.mTotalValue;
        aTraveler.mLandNumber += 1;
        aTraveler.updateMaxAssets();
    }
    //土地を増資した
    static public void increased(this TravelerStatus aTraveler, LandMass aLand) {
        aTraveler.mMoney -= aLand.getIncreaseCost(aLand.mIncreaseLevel - 1);
        aTraveler.mProperty += aLand.mTotalValue - aLand.getTotalValue(aLand.mIncreaseLevel - 1);
    }
    //料金を支払った
    static public void paidFee(this TravelerStatus aTraveler, LandMass aLand) {
        aTraveler.mMoney -= aLand.mFeeCost;
        aTraveler.mFeeAmount += aLand.mFeeCost;
    }
    //料金を受け取った
    static public void receivedFee(this TravelerStatus aTraveler, LandMass aLand) {
        aTraveler.mMoney += aLand.mFeeCost;
        aTraveler.updateMaxAssets();
    }
    //買収した
    static public void acquired(this TravelerStatus aTraveler, LandMass aLand) {
        aTraveler.mMoney -= aLand.mAcquisitionCost;
        aTraveler.mLandNumber += 1;
    }
    //買収された
    static public void beAcquired(this TravelerStatus aTraveler, LandMass aLand) {
        aTraveler.mMoney += aLand.mAcquisitionTakeCost;
        aTraveler.mLandNumber -= 1;
    }
    //土地を売却した
    static public void soldLand(this TravelerStatus aTraveler, LandMass aLand) {
        aTraveler.mMoney += aLand.mSellCost;
        aTraveler.mProperty -= aLand.mTotalValue;
        aTraveler.mLandNumber -= 1;
    }
}
