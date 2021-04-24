using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEventTypeSetting : FlowerBoadSetting {
    public override void set(Action aCallback) {
        displayBoard("orbitEventTypeSetting", () => {
            Subject.addObserver(new Observer("orbitEventTypeSetting", (aMessage) => {
                switch (aMessage.name) {
                    case "nonePushed":
                        GameData.mGameSetting.mOrbitEventType = OrbitEventType.none;
                        break;
                    case "alwaysPushed":
                        GameData.mGameSetting.mOrbitEventType = OrbitEventType.always;
                        break;
                    case "firstRidePushed":
                        GameData.mGameSetting.mOrbitEventType = OrbitEventType.firstRide;
                        break;
                    case "firstComePushed":
                        GameData.mGameSetting.mOrbitEventType = OrbitEventType.firstCome;
                        break;
                    case "bottomPushed":
                        GameData.mGameSetting.mOrbitEventType = OrbitEventType.bottom;
                        break;
                    case "bottomHalfPushed":
                        GameData.mGameSetting.mOrbitEventType = OrbitEventType.bottomHalf;
                        break;
                    default:
                        return;
                }
                hadeBoard(() => {
                    Subject.removeObserver("orbitEventTypeSetting");
                    aCallback();
                });
            }));
        });
    }
}
