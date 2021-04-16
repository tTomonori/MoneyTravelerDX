using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnClock : MyBehaviour {
    [SerializeField]
    public MyBehaviour mClock;
    public MyBehaviour mTexts;
    public MyBehaviour mNumber1;
    public MyBehaviour mNumber2;
    public TextMesh mNumberMesh1;
    public TextMesh mNumberMesh2;
    static public TurnClock create() {
        TurnClock tClock = GameObject.Instantiate(Resources.Load<TurnClock>("prefabs/game/ui/turnClock"));
        tClock.mNumberMesh1.text = "0";
        tClock.mNumberMesh2.text = "0";
        return tClock;
    }
    public void setTurn(string tText) {
        mNumberMesh1.text = mNumberMesh2.text;
        mNumberMesh2.text = tText;
        mNumber1.rotateX = 0;
        mNumber2.rotateX = 90;
        mClock.moveTo(new Vector2(0, 2), 0.5f);
        mClock.rotateZBy(-50, 0.5f);
        mTexts.moveTo(new Vector2(0, 0), 0.5f);
        mTexts.scaleBy(new Vector2(0.4f, 0.4f), 0.5f);
        mClock.scaleBy(new Vector2(0.2f, 0.2f), 0.5f);
        MyBehaviour.setTimeoutToIns(0.7f, () => {
            mNumber1.rotateXBy(-90, 0.5f);
            mNumber2.rotateXBy(-90, 0.5f);
            MyBehaviour.setTimeoutToIns(0.7f, () => {
                mClock.moveTo(new Vector2(-7, 4.5f),0.5f);
                mClock.rotateZBy(50, 0.5f);
                mTexts.moveTo(new Vector2(0, -1), 0.5f);
        mTexts.scaleBy(new Vector2(-0.4f, -0.4f), 0.5f);
                mClock.scaleBy(new Vector2(-0.2f, -0.2f), 0.5f);
            });
        });
    }
}
