using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TravelerCharaSetting : FlowerBoadSetting {
    static public int mSettingPlayerNumber;
    public override void set(Action aCallback) {
        displayBoard("TravelerCharaSetting", () => {
            Subject.addObserver(new Observer("TravelerCharaSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "nonePushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mTravelerCharaData = TravelerCharaData.none;
                        break;
                    case "mariePushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mTravelerCharaData = TravelerCharaData.marie;
                        break;
                    case "rearPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mTravelerCharaData = TravelerCharaData.rear;
                        break;
                    case "maruPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mTravelerCharaData = TravelerCharaData.maru;
                        break;
                    case "chiaraPushed":
                        GameData.mGameSetting.mTravelerData[mSettingPlayerNumber - 1].mTravelerCharaData = TravelerCharaData.chiara;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("TravelerCharaSetting");
                    aCallback();
                });
            }));
        });
    }
}
