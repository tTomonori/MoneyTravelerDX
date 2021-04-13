using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisasterDamageSetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("disasterDamageSetting", () => {
            Subject.addObserver(new Observer("disasterDamageSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "1Pushed":
                        GameData.mGameSetting.mDisasterDamage = 1;
                        break;
                    case "2Pushed":
                        GameData.mGameSetting.mDisasterDamage = 2;
                        break;
                    case "3Pushed":
                        GameData.mGameSetting.mDisasterDamage = 3;
                        break;
                    case "4Pushed":
                        GameData.mGameSetting.mDisasterDamage = 4;
                        break;
                    case "5Pushed":
                        GameData.mGameSetting.mDisasterDamage = 5;
                        break;
                    case "10Pushed":
                        GameData.mGameSetting.mDisasterDamage = 10;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("disasterDamageSetting");
                    aCallback();
                });
            }));
        });
    }
}
