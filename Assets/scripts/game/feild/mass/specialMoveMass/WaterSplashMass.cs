using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplashMass : SpecialMoveMass {
    static public readonly float mSplashSpeed = 2.5f;
    static public readonly float mJumpDuration = 0.5f;
    static public readonly float mJumpHeight = 1f;
    [SerializeField]
    public WaterSplash mSplash;
    public override void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        if (this.positionY < aNextMass.positionY)
            splashUp(aTraveler, aNextMass, aCallback);
        else
            splashDown(aTraveler, aNextMass, aCallback);
    }
    //移動先の方が高い場合
    public void splashUp(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        float tPreHeight = mSplash.mHeight;
        mSplash.changeHeightWithSpeed(aNextMass.positionY - mSplash.worldPosition.y, mSplashSpeed, () => {
            MyBehaviour.setTimeoutToIns(mJumpDuration, () => {
                mSplash.changeHeightWithSpeed(tPreHeight, mSplashSpeed, () => { });
            });
        });
        MyBehaviour.setTimeoutToIns((this.positionY - mSplash.worldPosition.y - tPreHeight) / mSplashSpeed - mJumpDuration, () => {
            aTraveler.mComa.sinMove(new Vector3(mSplash.worldPosition.x - this.positionX, 0, mSplash.worldPosition.z - this.positionZ), new Vector3(0, mJumpHeight, 0), 0, 1, mJumpDuration, () => {
                aTraveler.mComa.moveToWithSpeed(new Vector3(mSplash.worldPosition.x, aNextMass.positionY, mSplash.worldPosition.z), mSplashSpeed, () => {
                    aTraveler.mComa.sinMove(new Vector3(aNextMass.positionX - mSplash.worldPosition.x, 0, aNextMass.positionZ - mSplash.worldPosition.z), new Vector3(0, mJumpHeight, 0), 0, 1, mJumpDuration, () => {
                        aCallback();
                    });
                });
            });
        });
    }
    //移動先の方が低い場合
    public void splashDown(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        float tPreHeight = mSplash.mHeight;
        mSplash.changeHeightWithSpeed(this.positionY - mSplash.worldPosition.y, mSplashSpeed, () => {
            MyBehaviour.setTimeoutToIns(mJumpDuration, () => {
                mSplash.changeHeightWithSpeed(tPreHeight, mSplashSpeed, () => { });
            });
        });
        MyBehaviour.setTimeoutToIns((this.positionY - mSplash.worldPosition.y - tPreHeight) / mSplashSpeed, () => {
            aTraveler.mComa.sinMove(new Vector3(mSplash.worldPosition.x - this.positionX, 0, mSplash.worldPosition.z - this.positionZ), new Vector3(0, mJumpHeight, 0), 0, 1, mJumpDuration, () => {
                aTraveler.mComa.moveToWithSpeed(new Vector3(mSplash.worldPosition.x, aNextMass.positionY, mSplash.worldPosition.z), mSplashSpeed, () => {
                    aTraveler.mComa.sinMove(new Vector3(aNextMass.positionX - mSplash.worldPosition.x, 0, aNextMass.positionZ - mSplash.worldPosition.z), new Vector3(0, mJumpHeight, 0), 0, 1, mJumpDuration, () => {
                        aCallback();
                    });
                });
            });
        });
    }
}
