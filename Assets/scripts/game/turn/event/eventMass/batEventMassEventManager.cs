using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public partial class EventMassEventManager {
    static public void runBatEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((thiefInvades, 4));
        tEventList.Add((pocketMoneyTax, 4));
        tEventList.Add((lostSupportMoneyToLower, 2));
        tEventList.Add((backToStart, 1));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //泥棒
    static public void thiefInvades(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(aTraveler.mProperty * UnityEngine.Random.Range(5f, 7f) / 100);
        aMaster.mUiMain.displayEventDescription("物件に泥棒が侵入した!\n-" + tMoney.ToString() + "金", () => {
            giveDamage(aTraveler, tMoney, aMaster, aCallback);
        });
    }
    //所持金税
    static public void pocketMoneyTax(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tRate = UnityEngine.Random.Range(10, 15);
        int tMoney = (int)(aTraveler.mMoney * tRate / 100f);
        aMaster.mUiMain.displayEventDescription("所持金の" + tRate.ToString() + "%を\n税金として納める\n-" + tMoney.ToString() + "金", () => {
            giveDamage(aTraveler, tMoney, aMaster, aCallback);
        });
    }
    //下位への支援金
    static public void lostSupportMoneyToLower(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(20 + 50 * UnityEngine.Random.Range(0f, 1f) + aTraveler.mOrbit * UnityEngine.Random.Range(0f, 0.5f));
        aMaster.mUiMain.displayEventDescription("自分より下位のトラベラーへ\n支援金として" + tMoney.ToString() + "金ずつ\n払う", () => {
            List<(TravelerStatus, int)> tMoneyList = new List<(TravelerStatus, int)>();
            foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                if (tTraveler.mIsRetired) continue;
                if (tTraveler == aTraveler) continue;
                if (aTraveler.mRanking >= tTraveler.mRanking) continue;
                tMoneyList.Add((tTraveler, tMoney));
            }
            tMoneyList.Insert(0, (aTraveler, -tMoney * tMoneyList.Count));
            continuousGiveBonus(tMoneyList, aMaster, aCallback);
        });
    }
    //スタートに戻る
    static public void backToStart(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        aMaster.mUiMain.displayEventDescription("スタートに戻る・・・", () => {
            StartMass tStart = aMaster.getStart(aTraveler);
            warp(aTraveler, tStart, aMaster.getTweakComaPosition(0, 1), aMaster, true, ()=> {
                MyBehaviour.setTimeoutToIns(0.5f, aCallback);
            });
        });
    }
}
