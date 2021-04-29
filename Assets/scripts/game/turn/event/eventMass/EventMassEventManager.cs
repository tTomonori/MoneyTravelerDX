using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public partial class EventMassEventManager {
    static public void run(TravelerStatus aTraveler, EventMass aMass, GameMaster aMaster, Action aCallback) {
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
            case EventMassType.question:
                runQuestionEvent(aTraveler, aMaster, aCallback);
                return;
            case EventMassType.building:
                runBuildingEvent(aTraveler, aMaster, aCallback);
                return;
        }
    }
    //リストの中から実行するイベントを決める
    static public Action<TravelerStatus, GameMaster, Action> pickEvent(List<(Action<TravelerStatus, GameMaster, Action>, float)> aEventList) {
        float tTotalWeight = 0;
        foreach ((Action<TravelerStatus, GameMaster, Action>, float) tTuple in aEventList)
            tTotalWeight += tTuple.Item2;

        float tWeight = UnityEngine.Random.Range(0f, tTotalWeight);
        foreach ((Action<TravelerStatus, GameMaster, Action>, float) tTuple in aEventList) {
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
    //指定したトラベラー全員に指定した額のボーナスを与える
    static public void continuousGiveBonusSameAmount(List<TravelerStatus> aTravelerList, int aMoney, GameMaster aMaster, Action aCallback) {
        CallbackSystem tSystem = new CallbackSystem();
        foreach (TravelerStatus tTraveler in aTravelerList) {
            Action tCounter = tSystem.getCounter();
            GameEffector.getCoin(tTraveler.mComa.worldPosition, "+" + aMoney.ToString(), () => {
                tTraveler.getMoney(aMoney);
                tCounter();
            });
        }
        tSystem.then(() => {
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
            } else if (aMoneyList[aIndex].Item2 < 0) {
                giveDamage(aMoneyList[aIndex].Item1, -aMoneyList[aIndex].Item2, aMaster, () => { tCallNext(aIndex); });
                return;
            } else if (aMoneyList[aIndex].Item2 > 0) {
                giveMoney(aMoneyList[aIndex].Item1, aMoneyList[aIndex].Item2, aMaster, () => { tCallNext(aIndex); });
                return;
            }
        };
        tFunction(0);
    }
    //トラベラーを指定したマスにワープ
    static public void warp(TravelerStatus aTraveler, GameMass aMass, Vector3 aTweakPosition, GameMaster aMaster, bool aTargetCamera, Action aCallback) {
        int tMassNumber = aMaster.mFeild.mMassList.IndexOf(aMass);
        if (aTargetCamera)
            GameData.mStageData.mCamera.mTarget = aMaster.mFeild.mMassList[aTraveler.mCurrentMassNumber];
        MySoundPlayer.playSe("jump", false);
        aTraveler.mComa.moveToWithSpeed(new Vector3(aTraveler.mComa.positionX, GameData.mStageData.mCeiling, aTraveler.mComa.positionZ), 15, () => {
            if (aTargetCamera)
                GameData.mStageData.mCamera.mTarget = aMass;
            aTraveler.mComa.position = new Vector3(aMass.worldPosition.x + aTweakPosition.x, GameData.mStageData.mCeiling, aMass.worldPosition.z + aTweakPosition.z);
            aTraveler.mComa.moveToWithSpeed(aMass.worldPosition + aTweakPosition, 15, () => {
                aTraveler.mCurrentMassNumber = tMassNumber;
                aCallback();
            });
        });
    }
}
