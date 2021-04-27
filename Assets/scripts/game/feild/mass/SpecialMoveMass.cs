using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class SpecialMoveMass : GameMass {
    //移動演出
    abstract public void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback);
}
