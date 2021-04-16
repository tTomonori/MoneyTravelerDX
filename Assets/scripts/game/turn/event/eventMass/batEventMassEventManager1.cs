using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public partial class EventMassEventManager {
    static public void runBatEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((thiefInvades, 5));
        tEventList.Add((pocketMoneyTax, 5));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //泥棒
    static public void thiefInvades(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(aTraveler.mProperty * UnityEngine.Random.Range(5f, 7f) / 100);
        aMaster.mUiMain.displayEventDescription("物件に泥棒が侵入した!\n-" + tMoney.ToString() + "金", () => {
            GameEffector.lostCoin(aTraveler.mComa.worldPosition, "-" + tMoney.ToString(), () => {
                aTraveler.lostMoney(tMoney);
                aMaster.updateStatusDisplay();
                BankruptcyEventManager.checkRankruptcy(aTraveler, aMaster, aCallback);
            });
        });
    }
    //所持金税
    static public void pocketMoneyTax(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tRate = UnityEngine.Random.Range(10, 15);
        int tMoney = (int)(aTraveler.mMoney * tRate / 100f);
        aMaster.mUiMain.displayEventDescription("所持金の"+tRate.ToString()+"%を\n税金として納める\n-" + tMoney.ToString() + "金", () => {
            GameEffector.lostCoin(aTraveler.mComa.worldPosition, "-" + tMoney.ToString(), () => {
                aTraveler.lostMoney(tMoney);
                aMaster.updateStatusDisplay();
                BankruptcyEventManager.checkRankruptcy(aTraveler, aMaster, aCallback);
            });
        });
    }
}
