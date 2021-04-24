using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TravelerAiSetting : FlowerBoadSetting {
    static public int mSettingPlayerNumber;
    public override void set(Action aCallback) {
        displayBoard("travelerAiSetting", () => {
            Subject.addObserver(new Observer("travelerAiSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "playerPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mAiPattern = TravelerAiPattern.player;
                        break;
                    case "solidPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mAiPattern = TravelerAiPattern.solid;
                        break;
                    case "carefullyPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mAiPattern = TravelerAiPattern.carefully;
                        break;
                    case "impulsePushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mAiPattern = TravelerAiPattern.impulse;
                        break;
                    case "increaserPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mAiPattern = TravelerAiPattern.increaser;
                        break;
                    case "buyerPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mAiPattern = TravelerAiPattern.buyer;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("travelerAiSetting");
                    aCallback();
                });
            }));
        });
    }
}
