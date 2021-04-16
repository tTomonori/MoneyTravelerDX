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
}
