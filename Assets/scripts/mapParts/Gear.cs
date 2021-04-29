using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gear : MyBehaviour {
    [SerializeField]
    public int mToothNumber;
    public float mRotateThoothParSecond;
    public float mRotateDuration;
    private void Start() {
        if (mRotateThoothParSecond == 0) return;
        Action tF = null;
        tF = () => {
            rotateGear(mRotateThoothParSecond, mRotateDuration, () => {
                MyBehaviour.setTimeoutToIns(1f, tF);
            });
        };
        tF();
    }
    public void rotateGear(float aRotateThoothParSecond, float aDuration, Action aCallback) {
        float tDirection = Mathf.Abs(aRotateThoothParSecond) / aRotateThoothParSecond;
        this.rotateZBy(360f / mToothNumber * aRotateThoothParSecond * aDuration, aDuration, () => {
            this.rotateZBy(360f / mToothNumber / 10f * tDirection, 0.1f, () => {
                this.rotateZBy(360f / mToothNumber / 10f * -tDirection, 0.1f, () => {
                    aCallback();
                });
            });
        });
    }
}
