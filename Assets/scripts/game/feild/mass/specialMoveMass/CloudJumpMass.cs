using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudJumpMass : SpecialMoveMass {
    static public float mJumpHeight = 1.2f;
    static public float mJumpDuration = 0.5f;
    [SerializeField]
    public List<JumpCloud> mClouds;
    public override void effectMove(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        Action<int> tF = null;
        tF = (aIndex) => {
            if (aIndex < mClouds.Count) {
                jumpToCloud(aIndex, aTraveler, () => { tF(aIndex + 1); });
                return;
            }
            jumpToNextMass(aTraveler, aNextMass, aCallback);
        };
        tF(0);
    }
    private void jumpToCloud(int aIndex, TravelerStatus aTraveler, Action aCallback) {
        aTraveler.mComa.sinMove(mClouds[aIndex].worldPosition - aTraveler.mComa.worldPosition, new Vector3(0, mJumpHeight, 0), 0, 1, mJumpDuration, () => {
            mClouds[aIndex].bound();
            aCallback();
        });
    }
    private void jumpToNextMass(TravelerStatus aTraveler, SpecialMoveMass aNextMass, Action aCallback) {
        aTraveler.mComa.sinMove(aNextMass.worldPosition - aTraveler.mComa.worldPosition, new Vector3(0, mJumpHeight, 0), 0, 1, mJumpDuration, () => {
            aCallback();
        });
    }
}
