using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultTitle : MyBehaviour {
    [SerializeField]
    public MyBehaviour mPaper;
    public TextMesh mTitleMesh;
    public void setTitle(string aTitle,Action aCallback) {
        mPaper.moveBy(new Vector2(0, 2), 0.7f, () => {
            mTitleMesh.text = aTitle;
            mPaper.moveBy(new Vector2(0, -2), 0.7f, () => {
                aCallback();
            });
        });
    }
}
