using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public partial class EventMassEventManager {
    static public void runHeartEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((winLottery, 2));
        tEventList.Add((findFlirt, 2));
        tEventList.Add((getGiftCertificates, 3));
        tEventList.Add((getSupportMoneyFromHigher, 3));
        tEventList.Add((selectIncrease, 1));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //宝くじ
    static public void winLottery(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(50 + 100 * UnityEngine.Random.Range(0f, 1f) + 5 * UnityEngine.Random.Range(0f, (float)aTraveler.mOrbit));
        aMaster.mUiMain.displayEventDescription("宝くじが当たった!\n+" + tMoney.ToString() + "金", () => {
            giveMoney(aTraveler, tMoney, aMaster, aCallback);
        });
    }
    //へそくり
    static public void findFlirt(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(50 * UnityEngine.Random.Range(0.1f, 1f) + aTraveler.mAssets * UnityEngine.Random.Range(1f, 5f) / 100f);
        aMaster.mUiMain.displayEventDescription("へそくりを見つけた!\n+" + tMoney.ToString() + "金", () => {
            giveMoney(aTraveler, tMoney, aMaster, aCallback);
        });
    }
    //商品券
    static public void getGiftCertificates(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(UnityEngine.Random.Range(2, 6)) * 50;
        aMaster.mUiMain.displayEventDescription("商品券を手に入れた!\n+" + tMoney.ToString() + "金", () => {
            giveMoney(aTraveler, tMoney, aMaster, aCallback);
        });
    }
    //上位からの支援金
    static public void getSupportMoneyFromHigher(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(30 + 70 * UnityEngine.Random.Range(0f, 1f) + aTraveler.mOrbit * UnityEngine.Random.Range(0f, 0.5f));
        aMaster.mUiMain.displayEventDescription("自分より上位のトラベラーから\n支援金として" + tMoney.ToString() + "金ずつ\nもらう", () => {
            List<(TravelerStatus, int)> tMoneyList = new List<(TravelerStatus, int)>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                if (tTraveler == aTraveler) continue;
                if (aTraveler.mRanking <= tTraveler.mRanking) continue;
                tMoneyList.Add((tTraveler, -tMoney));
            }
            tMoneyList.Add((aTraveler, tMoney * tMoneyList.Count));
            continuousGiveBonus(tMoneyList, aMaster, aCallback);
        });
    }
    //好きな土地に増資
    static public void selectIncrease(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        aMaster.mUiMain.displayEventDescription("好きな土地に\n増資できます", () => {
            bool tOk = false;
            foreach(LandMass tLand in aMaster.mFeild.getOwnedLand(aTraveler)) {
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
}
