using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallCoinSystem : MyBehaviour {
    [NonSerialized] private float mElapsedTime = 0;
    [SerializeField]
    public float mTop;
    public float mBottom;
    public float mRight;
    public float mLeft;
    public float mCreateParSecond;
    public float mSpeed;
    private void Update() {
        mElapsedTime += Time.deltaTime;
        if (mElapsedTime < 1f / mCreateParSecond) return;
        for (int i = 0; i < mElapsedTime / mCreateParSecond; i++) {
            createCoin();
        }
        mElapsedTime = mElapsedTime % (1f / mCreateParSecond);
    }
    public FallCoin createCoin() {
        FallCoin tCoin = GameObject.Instantiate(Resources.Load<FallCoin>("models/kinCoin/TitleKinCoin"));
        if (mSpeed >= 0)
            tCoin.mSpeed = mSpeed;
        tCoin.mMaxMovedDistance = mTop - mBottom;
        tCoin.position2D = new Vector2(UnityEngine.Random.Range(mLeft, mRight), mTop);
        return tCoin;
    }
}
