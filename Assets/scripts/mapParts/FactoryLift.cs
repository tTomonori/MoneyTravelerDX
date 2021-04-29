using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FactoryLift : MyBehaviour {
    static public readonly float mSpeed = 8f;
    static public readonly float mGearRotateThoothParSecond = 2f;
    [SerializeField]
    public List<Gear> mGears;
    public void moveLift(float aPositionY, Action aCallback) {
        if (aPositionY == this.positionY) {
            aCallback();
            return;
        }
        float tDistance = aPositionY - this.positionY;
        float tDirection = Mathf.Abs(tDistance) / tDistance;
        float tDuration = Mathf.Abs(tDistance / mSpeed);
        this.moveBy(new Vector3(0, tDistance, 0), tDuration, () => {
            aCallback();
        });
        rotateGears(tDirection, tDuration);
    }
    public void rotateGears(float aDirection, float aDuration) {
        for (int i = 0; i < mGears.Count; i++) {
            if (i % 2 == 0)
                mGears[i].rotateGear(mGearRotateThoothParSecond * aDirection, aDuration, () => { });
            else
                mGears[i].rotateGear(mGearRotateThoothParSecond * -aDirection, aDuration, () => { });
        }
    }
}
