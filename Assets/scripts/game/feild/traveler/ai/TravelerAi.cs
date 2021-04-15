using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class TravelerAi {
    abstract public void rollDice(DiceManager aDiceManager,GameMaster aMaster);
    abstract public void endRollDice();

    abstract public void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback);
    abstract public void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback);
    abstract public void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback);
}
