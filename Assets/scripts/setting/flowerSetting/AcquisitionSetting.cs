using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AcquisitionSetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("acquisitionSetting", () => {
            Subject.addObserver(new Observer("acquisitionSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "1Pushed":
                        GameData.mGameSetting.mAcquisition = 1;
                        break;
                    case "2Pushed":
                        GameData.mGameSetting.mAcquisition = 2;
                        break;
                    case "3Pushed":
                        GameData.mGameSetting.mAcquisition = 3;
                        break;
                    case "4Pushed":
                        GameData.mGameSetting.mAcquisition = 4;
                        break;
                    case "5Pushed":
                        GameData.mGameSetting.mAcquisition = 5;
                        break;
                    case "10Pushed":
                        GameData.mGameSetting.mAcquisition = 10;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("acquisitionSetting");
                    aCallback();
                });
            }));
        });
    }
}
