using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public partial class EventMassEventManager {
    static public void runMagicEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((heavenlyBlessing, 2));
        tEventList.Add((punishment, 2));
        tEventList.Add((punishPropertyTop, 2));
        tEventList.Add((landPurchaseSupportFund, 2));
        tEventList.Add((rankingSupportFund, 2));
        tEventList.Add((runDisasterEvent, 6));
        tEventList.Add((gather, 1));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //天の恵み
    static public void heavenlyBlessing(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(100 + 150 * UnityEngine.Random.Range(0f, 1f));
        aMaster.mUiMain.displayEventDescription("天の恵み!\n全員+" + tMoney.ToString() + "金", () => {
            List<TravelerStatus> tTravelerList = new List<TravelerStatus>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                tTravelerList.Add(tTraveler);
            }
            continuousGiveBonusSameAmount(tTravelerList, tMoney, aMaster, aCallback);
        });
    }
    //天罰
    static public void punishment(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(100 + 100 * UnityEngine.Random.Range(0f, 1f));
        aMaster.mUiMain.displayEventDescription("天罰が降った!\n全員-" + tMoney.ToString() + "金", () => {
            List<(TravelerStatus, int)> tPunishmentList = new List<(TravelerStatus, int)>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                tPunishmentList.Add((tTraveler, -tMoney));
            }
            continuousGiveBonus(tPunishmentList, aMaster, aCallback);
        });
    }
    //物件価値合計トップ罰金
    static public void punishPropertyTop(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(75 + 100 * UnityEngine.Random.Range(0f, 1f));
        aMaster.mUiMain.displayEventDescription("物件価値合計が\n最も多いトラベラーは\n税金を払う-" + tMoney.ToString() + "金", () => {
            List<(TravelerStatus, int)> tTopList = new List<(TravelerStatus, int)>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                if (tTopList.Count == 0) {
                    tTopList.Add((tTraveler, -tMoney));
                    continue;
                }
                if (tTopList[0].Item1.mProperty == tTraveler.mProperty) {
                    tTopList.Add((tTraveler, -tMoney));
                    continue;
                }
                if (tTopList[0].Item1.mProperty < tTraveler.mProperty) {
                    tTopList.Clear();
                    tTopList.Add((tTraveler, -tMoney));
                    continue;
                }
            }
            continuousGiveBonus(tTopList, aMaster, aCallback);
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
    //全員集合
    static public void gather(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        aMaster.mUiMain.displayEventDescription("全員このマスに集合", () => {
            List<TravelerStatus> tTravelers = aMaster.getActiveTravelers();
            int tNumberOfTraveler = tTravelers.Count - 1;
            CallbackSystem tSystem = new CallbackSystem();
            foreach (TravelerStatus tTraveler in tTravelers) {
                if (tTraveler == aTraveler) continue;
                Action tCounter = tSystem.getCounter();
                int tNumberOfOrder = 0;
                foreach (TravelerStatus tComparison in tTravelers) {
                    if (tComparison == aTraveler) continue;
                    if (tTraveler.mPlayerNumber >= tComparison.mPlayerNumber)
                        tNumberOfOrder++;
                }
                warp(tTraveler, aMaster.mFeild.mMassList[aTraveler.mCurrentMassNumber], aMaster.getTweakComaPosition(tNumberOfTraveler, tNumberOfOrder), aMaster, false, tCounter);
            }
            tSystem.then(() => {
                MyBehaviour.setTimeoutToIns(0.5f, aCallback);
            });
        });
    }
}
