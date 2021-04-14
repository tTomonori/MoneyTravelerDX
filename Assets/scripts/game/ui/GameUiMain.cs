using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameUiMain : MyBehaviour {
    //ターンの順番を決める
    public void decideTurnOrder(List<TravelerStatus> aTravelers, Action<List<TravelerStatus>> aCallback) {
        //順番を決める
        List<TravelerStatus> tTurnOrder = new List<TravelerStatus>();
        foreach (TravelerStatus tTraveler in aTravelers) {
            if (tTraveler == null) continue;
            tTurnOrder.Add(tTraveler);
        }
        tTurnOrder = tTurnOrder.OrderBy(i => Guid.NewGuid()).ToList();
        //演出
        TurnOrderAnimator tAnimator = this.createChild<TurnOrderAnimator>("turnOrderAnimator");
        tAnimator.animate(aTravelers, tTurnOrder, () => {
            tAnimator.delete();
            aCallback(tTurnOrder);
        });
    }
    //トラベラーのステータスを表示
    public void displayStatus(List<TravelerStatus> aTravelers,Action aCallback) {

    }
}
