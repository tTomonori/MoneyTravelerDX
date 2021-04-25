using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SecondHandPrice {
    initialValue,currentValue,acquisitionPrice,cannotPurchase
}

static public class SecondHandPriceMethods {
    static public string getName(this SecondHandPrice e) {
        switch (e) {
            case SecondHandPrice.initialValue:
                return "初期価値";
            case SecondHandPrice.currentValue:
                return "現在価値";
            case SecondHandPrice.acquisitionPrice:
                return "買収価格";
            case SecondHandPrice.cannotPurchase:
                return "購入不可";
        }
        throw new Exception();
    }
}