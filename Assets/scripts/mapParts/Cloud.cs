using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cloud : MyBehaviour {
    [SerializeField]
    public float mRotateSpeed;
    public float mShakeHeight;
    public float mShakeSpeed;
    public float mScaleXSize;
    public float mScaleXSpeed;
    public float mScaleYSize;
    public float mScaleYSpeed;
    private void Start() {
        if (mRotateSpeed != 0)
            actRotate(UnityEngine.Random.Range(0, 100) < 50 ? 1 : -1);
        if (mShakeHeight != 0)
            actShake();
        if (mScaleXSize != 0)
            actScaleX();
        if (mScaleYSize != 0)
            actScaleY();
    }
    public void actRotate(int aDirection) {
        this.rotateZByWithSpeed(360 * aDirection, mRotateSpeed, () => { actRotate(aDirection); });
    }
    public void actShake() {
        Action tMove = null;
        float tPi = UnityEngine.Random.Range(0, 359);
        this.positionY += mShakeHeight * Mathf.Sign(tPi / 360f * Mathf.PI);
        tMove = () => {
            this.sinMoveWithSpeed(Vector3.zero, new Vector3(0, mShakeHeight, 0), tPi / 360f, tPi / 360f + 2, mShakeSpeed, tMove);
        };
        tMove();
    }
    public void actScaleX() {
        this.scaleByWithSpeed(new Vector3(mScaleXSize, 0, 0), mScaleXSpeed, () => {
            this.scaleByWithSpeed(new Vector3(-mScaleXSize * 2, 0, 0), mScaleXSpeed, () => {
                this.scaleByWithSpeed(new Vector3(mScaleXSize, 0, 0), mScaleXSpeed, actScaleX);
            });
        });
    }
    public void actScaleY() {
        this.scaleByWithSpeed(new Vector3(0, mScaleYSize, 0), mScaleYSpeed, () => {
            this.scaleByWithSpeed(new Vector3(0, -mScaleYSize * 2, 0), mScaleYSpeed, () => {
                this.scaleByWithSpeed(new Vector3(0, mScaleYSize, 0), mScaleYSpeed, actScaleY);
            });
        });
    }
}
