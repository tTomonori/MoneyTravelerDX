using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMass : GameMass {
    [SerializeField]
    public EventMassType mEventType;
}

public enum EventMassType {
    heart,bat,magic,disaster
}