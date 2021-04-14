using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TravelerAi {
    abstract public void rollDice(DiceManager aDiceManager);
    abstract public void endRollDice();
}
