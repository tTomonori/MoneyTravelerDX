using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMass : MyBehaviour {
    public virtual void initialize() { }
    public virtual GameMass getNotShared() { return this; }
}
