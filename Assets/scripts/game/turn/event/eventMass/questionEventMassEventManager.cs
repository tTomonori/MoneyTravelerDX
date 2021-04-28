using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public partial class EventMassEventManager {
    static public void runQuestionEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((runHeartEvent, 1));
        tEventList.Add((runBatEvent, 1));
        tEventList.Add((runMagicEvent, 1));
        tEventList.Add((runDisasterEvent, 1));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
}
