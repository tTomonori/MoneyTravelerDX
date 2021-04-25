using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class OrbitEventManager {
    static public void run(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((addBonus, 3));
        tEventList.Add((EventMassEventManager.selectPurchase, 5));
        tEventList.Add((EventMassEventManager.selectIncrease, 5));
        EventMassEventManager.pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //追加ボーナス
    static public void addBonus(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(50 * UnityEngine.Random.Range(2, 6));
        aMaster.mUiMain.displayEventDescription("追加ボーナス!\n+" + tMoney.ToString() + "金", () => {
            EventMassEventManager.giveMoney(aTraveler, tMoney, aMaster, aCallback);
        });
    }
}
