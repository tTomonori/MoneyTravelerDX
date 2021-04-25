using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecondHandPriceSetting : FlowerBoadSetting {
    static public int mSettingPlayerNumber;
    public override void set(Action aCallback) {
        displayBoard("secondHandPriceSetting", () => {
            Subject.addObserver(new Observer("secondHandPriceSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "initialValuePushed":
                        GameData.mGameSetting.mSecondHandPrice = SecondHandPrice.initialValue;
                        break;
                    case "currentValuePushed":
                        GameData.mGameSetting.mSecondHandPrice = SecondHandPrice.currentValue;
                        break;
                    case "acquisitionPricePushed":
                        GameData.mGameSetting.mSecondHandPrice = SecondHandPrice.acquisitionPrice;
                        break;
                    case "cannotPurchasePushed":
                        GameData.mGameSetting.mSecondHandPrice = SecondHandPrice.cannotPurchase;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("secondHandPriceSetting");
                    aCallback();
                });
            }));
        });
    }
}
