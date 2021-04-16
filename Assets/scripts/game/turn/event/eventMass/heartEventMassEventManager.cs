using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public partial class EventMassEventManager {
    static public void runHeartEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((winLottery, 3));
        tEventList.Add((findFlirt, 1));
        tEventList.Add((getGiftCertificates, 6));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //宝くじ
    static public void winLottery(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(50 + 100 * UnityEngine.Random.Range(0f, 1f) + 5 * UnityEngine.Random.Range(0f, (float)aTraveler.mOrbit));
        aMaster.mUiMain.displayEventDescription("宝くじが当たった!\n+" + tMoney.ToString() + "金", () => {
            GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + tMoney.ToString(), () => {
                aTraveler.getMoney(tMoney);
                aMaster.updateStatusDisplay();
                aCallback();
            });
        });
    }
    //へそくり
    static public void findFlirt(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(50 * UnityEngine.Random.Range(0.1f, 1f) + aTraveler.mAssets * UnityEngine.Random.Range(1f, 5f) / 100f);
        aMaster.mUiMain.displayEventDescription("へそくりを見つけた!\n+" + tMoney.ToString() + "金", () => {
            GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + tMoney.ToString(), () => {
                aTraveler.getMoney(tMoney);
                aMaster.updateStatusDisplay();
                aCallback();
            });
        });
    }
    //商品券
    static public void getGiftCertificates(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tMoney = (int)(UnityEngine.Random.Range(1, 3)) * 100;
        aMaster.mUiMain.displayEventDescription("商品券を手に入れた!\n+" + tMoney.ToString() + "金", () => {
            GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + tMoney.ToString(), () => {
                aTraveler.getMoney(tMoney);
                aMaster.updateStatusDisplay();
                aCallback();
            });
        });
    }
}
