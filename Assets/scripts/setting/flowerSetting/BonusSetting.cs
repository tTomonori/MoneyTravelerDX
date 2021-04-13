using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonusSetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("bonusSetting", () => {
            Subject.addObserver(new Observer("bonusSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "0Pushed":
                        GameData.mGameSetting.mBonus = 0;
                        break;
                    case "0.5Pushed":
                        GameData.mGameSetting.mBonus = 0.5f;
                        break;
                    case "1Pushed":
                        GameData.mGameSetting.mBonus = 1;
                        break;
                    case "2Pushed":
                        GameData.mGameSetting.mBonus = 2;
                        break;
                    case "3Pushed":
                        GameData.mGameSetting.mBonus = 3;
                        break;
                    case "4Pushed":
                        GameData.mGameSetting.mBonus = 4;
                        break;
                    case "5Pushed":
                        GameData.mGameSetting.mBonus = 5;
                        break;
                    case "10Pushed":
                        GameData.mGameSetting.mBonus = 10;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("bonusSetting");
                    aCallback();
                });
            }));
        });
    }
}
