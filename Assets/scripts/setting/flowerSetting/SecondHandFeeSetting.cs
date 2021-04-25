using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecondHandFeeSetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("secondHandFeeSetting", () => {
            Subject.addObserver(new Observer("secondHandFeeSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "nonePushed":
                        GameData.mGameSetting.mSecondHandFee = false;
                        break;
                    case "yesPushed":
                        GameData.mGameSetting.mSecondHandFee = true;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("secondHandFeeSetting");
                    aCallback();
                });
            }));
        });
    }
}
