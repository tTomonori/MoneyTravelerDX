using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InitialMoneySetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("initialMoneySetting", () => {
            Subject.addObserver(new Observer("initialMoneySetting", (aMessage) => {
                switch (aMessage.name) {
                    case "0.5Pushed":
                        GameData.mGameSetting.mInitialMoney = 0.5f;
                        break;
                    case "1Pushed":
                        GameData.mGameSetting.mInitialMoney = 1;
                        break;
                    case "2Pushed":
                        GameData.mGameSetting.mInitialMoney = 2;
                        break;
                    case "3Pushed":
                        GameData.mGameSetting.mInitialMoney = 3;
                        break;
                    case "4Pushed":
                        GameData.mGameSetting.mInitialMoney = 4;
                        break;
                    case "5Pushed":
                        GameData.mGameSetting.mInitialMoney = 5;
                        break;
                    case "10Pushed":
                        GameData.mGameSetting.mInitialMoney = 10;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("initialMoneySetting");
                    aCallback();
                });
            }));
        });
    }
}
