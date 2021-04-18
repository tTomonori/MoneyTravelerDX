using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public partial class EventMassEventManager {
    static public void run(TravelerStatus aTraveler,EventMass aMass,GameMaster aMaster,Action aCallback) {
        switch (aMass.mEventType) {
            case EventMassType.heart:
                runHeartEvent(aTraveler, aMaster, aCallback);
                return;
            case EventMassType.bat:
                runBatEvent(aTraveler, aMaster, aCallback);
                return;
            case EventMassType.magic:
                runMagicEvent(aTraveler, aMaster, aCallback);
                return;
            case EventMassType.disaster:
                runDisasterEvent(aTraveler, aMaster, aCallback);
                return;
        }
    }
    //リストの中から実行するイベントを決める
    static public Action<TravelerStatus,GameMaster,Action> pickEvent(List<(Action<TravelerStatus, GameMaster, Action>,float)> aEventList) {
        float tTotalWeight = 0;
        foreach ((Action<TravelerStatus, GameMaster, Action>, float) tTuple in aEventList)
            tTotalWeight += tTuple.Item2;

        float tWeight = UnityEngine.Random.Range(0f, tTotalWeight);
        foreach((Action<TravelerStatus, GameMaster, Action>, float) tTuple in aEventList) {
            tWeight -= tTuple.Item2;
            if (tWeight > 0) continue;
            return tTuple.Item1;
        }
        return aEventList[aEventList.Count - 1].Item1;
    }
    //指定したトラベラーに指定した額の損害を与える
    static public void giveDamage(TravelerStatus aTraveler, int aMoney, GameMaster aMaster, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, "-" + aMoney.ToString(), () => {
            aTraveler.lostMoney(aMoney);
            aMaster.updateStatusDisplay();
            BankruptcyEventManager.checkRankruptcy(aTraveler, aMaster, aCallback);
        });
    }
    //指定したトラベラーに指定した額のボーナスを与える
    static public void giveMoney(TravelerStatus aTraveler, int aMoney, GameMaster aMaster, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        GameEffector.getCoin(aTraveler.mComa.worldPosition, "+" + aMoney.ToString(), () => {
            aTraveler.getMoney(aMoney);
            aMaster.updateStatusDisplay();
            aCallback();
        });
    }
    //それぞれのトラベラーにそれぞれ指定した額の損害もしくはボーナスを与える
    static public void continuousGiveBonus(List<(TravelerStatus, int)> aMoneyList, GameMaster aMaster, Action aCallback) {
        int tLength = aMoneyList.Count;
        Action<int> tFunction = null;
        Action<int> tCallNext = (aIndex) => {
            if (aIndex + 1 < tLength) {
                tFunction(aIndex + 1);
                return;
            }
            aCallback();
        };
        tFunction = (aIndex) => {
            if (aMoneyList[aIndex].Item2 == 0) {
                tCallNext(aIndex);
                return;
            }else if (aMoneyList[aIndex].Item2 < 0) {
                giveDamage(aMoneyList[aIndex].Item1, -aMoneyList[aIndex].Item2, aMaster, () => { tCallNext(aIndex); });
                return;
            }else if (aMoneyList[aIndex].Item2 > 0) {
                giveMoney(aMoneyList[aIndex].Item1, aMoneyList[aIndex].Item2, aMaster, () => { tCallNext(aIndex); });
                return;
            }
        };
        tFunction(0);
    }
}
