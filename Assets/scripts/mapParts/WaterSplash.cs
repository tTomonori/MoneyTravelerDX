using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterSplash : MyBehaviour {
    static public readonly float mTopMargin = 0.9f;
    [SerializeField]
    public MyBehaviour mTopSplash;
    public MyBehaviour mCenterSplash;
    public MyBehaviour mBottomSplash;
    public float mHeight;
    public Vector2 mScale;
    private void OnValidate() {
        mTopSplash.scale2D = mScale;
        mCenterSplash.scale2D = mScale;
        mBottomSplash.scale2D = mScale;
        mBottomSplash.positionY = 0.5f * mScale.y;
        setHeight(mHeight);
    }
    public void setHeight(float aHeight) {
        mHeight = aHeight;
        mTopSplash.positionY = mHeight + (-0.75f + 1.5f * mTopMargin) * mScale.y;
        mCenterSplash.positionY = ((1f * mScale.y) + (mHeight + (-0.75f + 1.5f * mTopMargin - 0.75f) * mScale.y)) / 2f;
        mCenterSplash.scaleY = ((mHeight + (-0.75f + 1.5f * mTopMargin - 0.75f) * mScale.y) - (1f * mScale.y));
    }
    //高さを変える
    public void changeHeight(float aHeight, float aDuration, Action aCallback) {
        if (aHeight == mHeight) {
            aCallback();
            return;
        }
        MySoundPlayer.playSe("splash", false);
        StartCoroutine(changeHeightDelta(aHeight - mHeight, aDuration, aCallback));
    }
    //高さを変える
    public void changeHeightWithSpeed(float aHeight, float aSpeed, Action aCallback) {
        if (aHeight == mHeight) {
            aCallback();
            return;
        }
        MySoundPlayer.playSe("splash", false);
        StartCoroutine(changeHeightDelta(aHeight - mHeight, Mathf.Abs((aHeight - mHeight) / aSpeed), aCallback));
    }
    private IEnumerator changeHeightDelta(float aHeightDelta, float aDuration, Action aCallback) {
        float tElapsedTime = 0;
        while (true) {
            if (tElapsedTime + Time.deltaTime >= aDuration) {//完了
                setHeight(mHeight + aHeightDelta * (aDuration - tElapsedTime) / aDuration);
                if (aCallback != null) aCallback();
                yield break;
            }
            tElapsedTime += Time.deltaTime;
            setHeight(mHeight + aHeightDelta * Time.deltaTime / aDuration);
            yield return null;
        }
    }
}
