using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public partial class EventMassEventManager {
    static public void runMagicEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((heavenlyBlessing, 3));
        tEventList.Add((landPurchaseSupportFund, 2));
        tEventList.Add((rankingSupportFund, 2));
        tEventList.Add((runDisasterEvent, 3));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //天の恵み
    static public void heavenlyBlessing(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(100 + 200 * UnityEngine.Random.Range(0f, 1f));
        aMaster.mUiMain.displayEventDescription("天の恵み!\n全員+" + tMoney.ToString() + "金", () => {
            List<(TravelerStatus, int)> tBonusList = new List<(TravelerStatus, int)>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                tBonusList.Add((tTraveler, tMoney));
            }
            continuousGiveBonus(tBonusList, aMaster, aCallback);
        });
    }
    //土地購入支援金
    static public void landPurchaseSupportFund(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(100 + 200 * UnityEngine.Random.Range(0f, 1f));
        aMaster.mUiMain.displayEventDescription("所有地が最も少ない\nトラベラーに\n土地購入支援金が配布された\n+" + tMoney.ToString() + "金", () => {
            List<(TravelerStatus, int)> tBonusList = new List<(TravelerStatus, int)>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                if (tBonusList.Count == 0) {
                    tBonusList.Add((tTraveler, tMoney));
                    continue;
                }
                if (tBonusList[0].Item1.mLandNumber == tTraveler.mLandNumber) {
                    tBonusList.Add((tTraveler, tMoney));
                    continue;
                }
                if (tBonusList[0].Item1.mLandNumber > tTraveler.mLandNumber) {
                    tBonusList.Clear();
                    tBonusList.Add((tTraveler, tMoney));
                    continue;
                }
            }
            continuousGiveBonus(tBonusList, aMaster, aCallback);
        });
    }
    //順位支援金
    static public void rankingSupportFund(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(100 + 200 * UnityEngine.Random.Range(0f, 1f));
        aMaster.mUiMain.displayEventDescription("総資産が最も少ない\nトラベラーに\n支援金が配布された\n+" + tMoney.ToString() + "金", () => {
            List<(TravelerStatus, int)> tBonusList = new List<(TravelerStatus, int)>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                if (tBonusList.Count == 0) {
                    tBonusList.Add((tTraveler, tMoney));
                    continue;
                }
                if (tBonusList[0].Item1.mAssets == tTraveler.mAssets) {
                    tBonusList.Add((tTraveler, tMoney));
                    continue;
                }
                if (tBonusList[0].Item1.mAssets > tTraveler.mAssets) {
                    tBonusList.Clear();
                    tBonusList.Add((tTraveler, tMoney));
                    continue;
                }
            }
            continuousGiveBonus(tBonusList, aMaster, aCallback);
        });
    }
}
