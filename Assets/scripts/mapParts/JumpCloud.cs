using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JumpCloud : MyBehaviour {
    static public float mShakeHeight = 0.1f;
    [SerializeField]
    public MyBehaviour mCloud;

    public void bound() {
        MySoundPlayer.playSe("bound", false);
        mCloud.scaleTo(new Vector3(1.2f, 0.8f, 0), 0.2f, () => {
            mCloud.scaleTo(new Vector3(0.9f, 1.1f, 0), 0.2f, () => {
                mCloud.scaleTo(new Vector3(1f, 1f, 0), 0.2f, () => {

                });
            });
        });
    }
    private void Start() {
        Action tMove = null;
        float aDuration = UnityEngine.Random.Range(2.5f, 4f);
        float tPi = UnityEngine.Random.Range(0, 359);
        mCloud.positionY += mShakeHeight * Mathf.Sign(tPi / 360f * Mathf.PI);
        tMove = () => {
            mCloud.sinMove(Vector3.zero, new Vector3(0, mShakeHeight, 0), tPi / 360f, tPi / 360f + 2, aDuration, tMove);
        };
        tMove();
    }
}
