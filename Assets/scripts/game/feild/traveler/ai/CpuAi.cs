using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CpuAi : TravelerAi {
    public override void rollDice(DiceManager aDiceManager, GameMaster aMaster) {
        MyBehaviour.setTimeoutToIns(0.4f, () => { aDiceManager.open1(); });
        MyBehaviour.setTimeoutToIns(0.8f, () => { aDiceManager.open2(); });
        MyBehaviour.setTimeoutToIns(1.2f, () => { aDiceManager.open3(); });
    }
    public override void endRollDice() { }
}
