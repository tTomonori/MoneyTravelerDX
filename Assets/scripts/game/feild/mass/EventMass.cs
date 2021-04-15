using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventMass : GameMass {
    [SerializeField]
    public EventMassType mEventType;
}

public enum EventMassType {
    heart, bat, magic, disaster
}
static public class EventMassTypeMethods {
    static public string getName(this EventMassType e) {
        switch (e) {
            case EventMassType.heart:
                return "ハート";
            case EventMassType.bat:
                return "バット";
            case EventMassType.magic:
                return "マジック";
            case EventMassType.disaster:
                return "災害";
        }
        throw new Exception();
    }
}