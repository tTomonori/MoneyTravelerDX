using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TravelerAiPattern {
    player,solid,carefully,impulse,buyer
}

static public class TravelerAiPatternMethods {
    static public string getName(this TravelerAiPattern e) {
        switch (e) {
            case TravelerAiPattern.player:
                return "プレイヤー";
            case TravelerAiPattern.solid:
                return "堅実";
            case TravelerAiPattern.carefully:
                return "慎重";
            case TravelerAiPattern.impulse:
                return "衝動";
            case TravelerAiPattern.buyer:
                return "土地買い";
        }
        throw new Exception();
    }
}