using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class TravelerStatusDisplayFactory {
    static public (MyBehaviour, List<TravelerStatusDisplay>) create(List<TravelerStatus> aTravelersTurnOrder) {
        List<TravelerStatusDisplay> tDisplays = new List<TravelerStatusDisplay>() { null, null, null, null };
        MyBehaviour tContainer = MyBehaviour.create<MyBehaviour>();
        tContainer.name = "travelerStatusDisplayContainer";
        for (int i = 0; i < aTravelersTurnOrder.Count; i++) {
            TravelerStatusDisplay tDisplay = GameObject.Instantiate(Resources.Load<TravelerStatusDisplay>("prefabs/game/ui/travelerStatusDisplay"));
            tDisplay.name = "travelerStatus" + aTravelersTurnOrder[i].mPlayerNumber.ToString();
            tDisplay.transform.SetParent(tContainer.transform);
            tDisplay.position2D = new Vector2(-5.4f + 3.6f * i, -3.57f);
            tDisplay.initialize(aTravelersTurnOrder[i]);
            tDisplays[aTravelersTurnOrder[i].mPlayerNumber - 1] = tDisplay;
        }
        tContainer.changeLayer(5, true);
        return (tContainer, tDisplays);
    }
}
