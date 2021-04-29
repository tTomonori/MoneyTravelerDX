using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public partial class EventMassEventManager {
    static public void runBuildingEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((selectIncrease, 1));
        tEventList.Add((selectPurchase, 1));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //好きな土地に増資
    static public void selectIncrease(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        aMaster.mUiMain.displayEventDescription("好きな土地に\n増資できます", () => {
            bool tOk = false;
            foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(aTraveler)) {
                if (tLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel) continue;
                if (tLand.mIncreaseCost > aTraveler.mMoney) continue;
                tOk = true;
                break;
            }
            if (!tOk) {
                aCallback();
                return;
            }
            aTraveler.mAi.selectIncrease(aTraveler, aMaster, (aLand) => {
                if (aLand == null) {
                    aCallback();
                    return;
                }
                //増資する
                LandMassEventManager.increaseLand(aTraveler, aLand, aMaster, aCallback);
            });
        });
    }
    //好きな土地を購入
    static public void selectPurchase(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        aMaster.mUiMain.displayEventDescription("好きな土地を\n購入できます", () => {
            bool tOk = false;
            foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(null)) {
                if (tLand.mPurchaseCost > aTraveler.mMoney) continue;
                tOk = true;
                break;
            }
            if (!tOk) {
                aCallback();
                return;
            }
            aTraveler.mAi.selectPurchase(aTraveler, aMaster, (aLand) => {
                if (aLand == null) {
                    aCallback();
                    return;
                }
                //購入する
                LandMassEventManager.purchaseLand(aTraveler, aLand, aMaster, aCallback);
            });
        });
    }
}
