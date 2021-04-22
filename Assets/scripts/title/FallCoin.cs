using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCoin : MyBehaviour {
    public float mSpeed = 5f;
    public float mMovedDistance = 0;
    public float mMaxMovedDistance = 10;
    public Vector3 mRotateDirection;
    public Vector3 mRotation;
    private void Start() {
        mRotateDirection = new Vector3(UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360));
    }
    private void Update() {
        if (mMovedDistance >= mMaxMovedDistance) {
            delete();
            return;
        }
        this.positionY -= Time.deltaTime * mSpeed;
        mMovedDistance += Time.deltaTime * mSpeed;

        mRotation = mRotation + mRotateDirection * Time.deltaTime;
        rotate = mRotation;
    }
}
