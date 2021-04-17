using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultGauge : MyBehaviour {
    [NonSerialized] public float mLength = 0;
    //ゲージを伸ばす
    public void extend(float aLength, Color aColor, float aSpeed, Action aCallback) {
        if (aLength <= 0) {
            aCallback();
            return;
        }
        MyBehaviour tBar = this.createChild<MyBehaviour>("bar");
        tBar.scaleY = 0;
        tBar.positionY = mLength;
        SpriteRenderer tRenderer = tBar.gameObject.AddComponent<SpriteRenderer>();
        tRenderer.sprite = Resources.Load<Sprite>("sprites/result/gauge");
        tRenderer.color = aColor;

        mLength += aLength;

        tBar.scaleToWithSpeed(new Vector2(1, aLength), aSpeed, aCallback);
    }
}
