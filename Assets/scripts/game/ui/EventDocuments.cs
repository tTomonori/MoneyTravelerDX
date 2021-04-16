using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventDocuments : MyBehaviour {
    [SerializeField]
    public MyBehaviour mDocuments;
    public MyBehaviour mEnvelope;
    public MyBehaviour mPaper;
    public TextMesh mText;
    static public EventDocuments create(string aText) {
        EventDocuments tDocuments = GameObject.Instantiate(Resources.Load<EventDocuments>("prefabs/game/event/eventDocuments"));
        tDocuments.mText.text = aText;
        return tDocuments;
    }
    public void animateOpen(Action aCallback) {
        mDocuments.positionY = 6f;
        mDocuments.moveTo(new Vector2(0, 0), 0.8f, () => {
            MyBehaviour.setTimeoutToIns(0.3f, () => {
                mDocuments.rotateZBy(-30, 0.3f);
                mPaper.moveTo(new Vector2(0, 1),0.7f);
                mEnvelope.moveTo(new Vector2(0, -4), 0.7f);
                MyBehaviour.setTimeoutToIns(0.8f, () => {
                    mPaper.rotateZBy(30, 0.4f);
                    mPaper.moveTo(new Vector2(0, 0),0.4f);
                    mEnvelope.moveBy(Quaternion.Euler(0, 0, 30) * new Vector2(0, -12), 0.8f, () => {
                        aCallback();
                    });
                });
            });
        });
    }
}
