using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventMass : GameMass {
    [SerializeField]
    public EventMassType mEventType;
    public EventMassRunType mRunType;
}

public enum EventMassType {
    heart, bat, magic, disaster, question
}
public enum EventMassRunType {
    stop, pass
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
            case EventMassType.question:
                return "ハテナ";
        }
        throw new Exception();
    }
}
static public class EventMassRunTypeMethods {
    static public string getName(this EventMassRunType e) {
        switch (e) {
            case EventMassRunType.stop:
                return "停止";
            case EventMassRunType.pass:
                return "通過";
        }
        throw new Exception();
    }
}