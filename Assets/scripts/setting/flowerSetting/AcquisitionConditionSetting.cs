using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquisitionConditionSetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("acquisitionConditionSetting", () => {
            Subject.addObserver(new Observer("acquisitionConditionSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "nonePushed":
                        GameData.mGameSetting.mAcquisitionCondition = AcquisitionCondition.none;
                        break;
                    case "alwaysPushed":
                        GameData.mGameSetting.mAcquisitionCondition = AcquisitionCondition.always;
                        break;
                    case "soldOutPushed":
                        GameData.mGameSetting.mAcquisitionCondition = AcquisitionCondition.soldOut;
                        break;
                    case "increaseMaxPushed":
                        GameData.mGameSetting.mAcquisitionCondition = AcquisitionCondition.increaseMax;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("acquisitionConditionSetting");
                    aCallback();
                });
            }));
        });
    }
}
