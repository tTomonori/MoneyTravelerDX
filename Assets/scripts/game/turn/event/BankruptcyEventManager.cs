using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class BankruptcyEventManager {
    static public void checkRankruptcy(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        if (aTraveler.mMoney >= 0) {
            aCallback();
            return;
        }
        MySoundPlayer.playSe("bancruptcy");
        run(aTraveler, aMaster, aCallback);
    }
    static public void run(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        if (aMaster.mFeild.getOwnedLand(aTraveler).Count == 0) {
            //リタイア
            aMaster.updateStatusDisplay();
            runRetire(aTraveler, aCallback);
            return;
        }
        aTraveler.mAi.sellLand(aTraveler, aMaster, (aLand) => {
            //売却
            LandMassEventManager.sellLand(aTraveler, aLand, aMaster, () => {
                if (aTraveler.mMoney < 0) {
                    run(aTraveler, aMaster, aCallback);
                    return;
                }
                aCallback();
            });
        });
    }
    static public void runRetire(TravelerStatus aTraveler, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        aTraveler.mIsRetired = true;
        MySoundPlayer.playSe("siren", false);
        MyBehaviour tPoliceCar = MyBehaviour.create<MyBehaviour>("policeCar");
        tPoliceCar.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/game/bankruptcy/policeCar");

        Vector3 tInPosition = aTraveler.mComa.worldPosition;
        tInPosition.x = GameData.mStageData.mEast + 10;
        tInPosition.z -= 3;
        Vector3 tStopPosition = aTraveler.mComa.worldPosition;
        tStopPosition.z -= 3;
        Vector3 tOutPosition = aTraveler.mComa.worldPosition;
        tOutPosition.x = GameData.mStageData.mWest - 10;
        tOutPosition.z -= 3;

        tPoliceCar.worldPosition = tInPosition;
        tPoliceCar.moveToWithSpeed(tStopPosition, 30, () => {
            aTraveler.mComa.gameObject.SetActive(false);
            MyBehaviour.setTimeoutToIns(0.7f, () => {
                tPoliceCar.moveToWithSpeed(tOutPosition, 30, () => {
                    tPoliceCar.delete();
                    aCallback();
                });
            });
        });
    }
}
