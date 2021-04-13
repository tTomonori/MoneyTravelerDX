using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FeeSetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("feeSetting", () => {
            Subject.addObserver(new Observer("feeSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "0.5Pushed":
                        GameData.mGameSetting.mFee = 0.5f;
                        break;
                    case "1Pushed":
                        GameData.mGameSetting.mFee = 1;
                        break;
                    case "2Pushed":
                        GameData.mGameSetting.mFee = 2;
                        break;
                    case "3Pushed":
                        GameData.mGameSetting.mFee = 3;
                        break;
                    case "4Pushed":
                        GameData.mGameSetting.mFee = 4;
                        break;
                    case "5Pushed":
                        GameData.mGameSetting.mFee = 5;
                        break;
                    case "10Pushed":
                        GameData.mGameSetting.mFee = 10;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("feeSetting");
                    aCallback();
                });
            }));
        });
    }
}
