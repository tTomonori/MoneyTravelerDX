using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultMiniPaper : MyBehaviour {
    [SerializeField]
    public TextMesh mNumberMesh1;
    public TextMesh mNumberMesh2;
    public MyBehaviour mNumber1;
    public MyBehaviour mNumber2;
    public void set(int aNumber) {
        mNumberMesh1.text = aNumber.ToString();
        mNumber2.gameObject.SetActive(false);
    }
    public void change(int aNumber, Action aCallback) {
        mNumber1.scaleTo(new Vector2(0, 0), 0.5f, () => {
            mNumberMesh1.text = (aNumber >= 0) ? "+" + aNumber.ToString() : aNumber.ToString();
            mNumber1.scaleTo(new Vector2(1, 1), 0.5f, () => {
                aCallback();
            });
        });
    }
    public void drop(Action aCallback) {
        mNumberMesh2.text = mNumberMesh1.text;
        mNumber2.gameObject.SetActive(true);
        mNumber2.positionY = 0;
        mNumber2.opacityBy(1, 0);
        mNumber2.opacityBy(-1, 0.5f);
        mNumber2.moveBy(new Vector2(0, -10), 0.5f, () => {
            aCallback();
        });
    }
}
