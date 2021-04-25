using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class LandMassEventManager {
    static public void runStopEvent(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        if (aLand.mOwner == null && !aLand.mSecondHand) {
            runStopFreeLandEvent(aTraveler, aLand, aMaster, aCallback);
            return;
        }
        if (aLand.mOwner == null && aLand.mSecondHand) {
            runStopSecondHandLandEvent(aTraveler, aLand, aMaster, aCallback);
            return;
        }
        if (aLand.mOwner == aTraveler) {
            runStopMyLandEvent(aTraveler, aLand, aMaster, aCallback);
            return;
        }
        runStopEnemyLandEvent(aTraveler, aLand, aMaster, aCallback);
    }
    static public void runStopFreeLandEvent(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        if (aTraveler.mMoney < aLand.mPurchaseCost) {
            //金が足りない
            aCallback();
            return;
        }
        aTraveler.mAi.purchaseLand(aTraveler, aLand, aMaster, (aAns) => {
            if (!aAns) {
                aCallback();
                return;
            }
            //購入する
            purchaseLand(aTraveler, aLand, aMaster, aCallback);
        });
    }
    static public void runStopMyLandEvent(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        if (aTraveler.mMoney < aLand.mIncreaseCost) {
            //金が足りない
            aCallback();
            return;
        }
        if (aLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel) {
            //増資MAX
            aCallback();
            return;
        }
        aTraveler.mAi.increaseLand(aTraveler, aLand, aMaster, (aAns) => {
            if (!aAns) {
                aCallback();
                return;
            }
            //増資する
            increaseLand(aTraveler, aLand, aMaster, aCallback);
        });
    }
    static public void runStopEnemyLandEvent(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, (-aLand.mFeeCost).ToString(), () => {
            aTraveler.paidFee(aLand);
            GameData.mStageData.mCamera.mTarget = aLand.mOwner.mComa;
            GameEffector.getCoin(aLand.mOwner.mComa.worldPosition, "+" + aLand.mFeeCost.ToString(), () => {
                aLand.mOwner.receivedFee(aLand);
                aMaster.updateStatusDisplay();
                //買収イベント
                if (GameData.mGameSetting.mAcquisitionCondition.canAcquisition(aMaster, aTraveler)) {
                    runAcquisitionEvent(aTraveler, aLand, aMaster, () => {
                        BankruptcyEventManager.checkRankruptcy(aTraveler, aMaster, aCallback);
                    });
                    return;
                }
                //破産チェック
                BankruptcyEventManager.checkRankruptcy(aTraveler, aMaster, aCallback);
            });
        });
    }
    static public void runStopSecondHandLandEvent(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        if (!GameData.mGameSetting.mSecondHandFee) {
            runPurchaseSecondHandLandEvent(aTraveler, aLand, aMaster, aCallback);
            return;
        }
        //中古土地料金あり
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, (-aLand.mFeeCost).ToString(), () => {
            aTraveler.paidFee(aLand);
            aMaster.updateStatusDisplay();
            BankruptcyEventManager.checkRankruptcy(aTraveler, aMaster, () => {
                if (aTraveler.mIsRetired) {
                    aCallback();
                    return;
                }
                runPurchaseSecondHandLandEvent(aTraveler, aLand, aMaster, aCallback);
            });
        });
    }
    static public void runPurchaseSecondHandLandEvent(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        if (GameData.mGameSetting.mSecondHandPrice == SecondHandPrice.cannotPurchase) {
            //購入不可の設定
            aCallback();
            return;
        }
        if (aTraveler.mMoney < aLand.mPurchaseCost) {
            //金が足りない
            aCallback();
            return;
        }
        aTraveler.mAi.purchaseLand(aTraveler, aLand, aMaster, (aAns) => {
            if (!aAns) {
                aCallback();
                return;
            }
            //購入する
            purchaseSecondHandLand(aTraveler, aLand, aMaster, aCallback);
        });
    }
    static public void runAcquisitionEvent(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        if (aTraveler.mMoney < aLand.mAcquisitionCost) {
            //金が足りない
            aCallback();
            return;
        }
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        aTraveler.mAi.acquireLand(aTraveler, aLand, aMaster, (aAns) => {
            if (!aAns) {
                aCallback();
                return;
            }
            //買収する
            acquireLand(aTraveler, aLand, aMaster, aCallback);
        });
    }
    static public void purchaseLand(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, (-aLand.mPurchaseCost).ToString(), () => {
            GameData.mStageData.mCamera.mTarget = aLand;
            aLand.changeOrner(aTraveler, () => { });
            aLand.changeIncreaseLevel(aLand.mIncreaseLevel, () => {
                aTraveler.purchased(aLand);
                aMaster.updateStatusDisplay();
                aCallback();
            });
        });
    }
    static public void purchaseSecondHandLand(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, (-aLand.mPurchaseCost).ToString(), () => {
            GameData.mStageData.mCamera.mTarget = aLand;
            aLand.changeOrner(aTraveler, () => {
                aTraveler.purchasedSecondHandLand(aLand);
                aMaster.updateStatusDisplay();
                aCallback();
            });
        });
    }
    static public void increaseLand(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, (-aLand.mIncreaseCost).ToString(), () => {
            GameData.mStageData.mCamera.mTarget = aLand;
            aLand.changeIncreaseLevel(aLand.mIncreaseLevel + 1, () => {
                aTraveler.increased(aLand);
                aMaster.updateStatusDisplay();
                aCallback();
            });
        });
    }
    static public void acquireLand(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        TravelerStatus tPreOwner = aLand.mOwner;
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, (-aLand.mAcquisitionCost).ToString(), () => {
            GameData.mStageData.mCamera.mTarget = aLand;
            aLand.changeOrner(aTraveler, () => {
                aTraveler.acquired(aLand);
                GameData.mStageData.mCamera.mTarget = tPreOwner.mComa;
                GameEffector.getCoin(tPreOwner.mComa.worldPosition, "+" + aLand.mAcquisitionTakeCost.ToString(), () => {
                    tPreOwner.beAcquired(aLand);
                    aMaster.updateStatusDisplay();
                    aCallback();
                });
            });
        });
    }
    static public void sellLand(TravelerStatus aTraveler, LandMass aLand, GameMaster aMaster, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aLand;
        GameEffector.lostCoin(aLand.worldPosition, "", () => {
            aLand.changeOrner(null, () => {
                aTraveler.soldLand(aLand);
                aMaster.updateStatusDisplay();
                aCallback();
            });
        });
    }
}
