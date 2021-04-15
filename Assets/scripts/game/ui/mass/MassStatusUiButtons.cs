using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MassStatusUiButtons : MyBehaviour {
    [SerializeField]
    public List<MyButton> mButtons;
    public List<TextMesh> mButtonTexts;
    public List<SpriteRenderer> mPaints;
    public List<MassStatusUiButtonData> mDataList;
    public void setButtons(List<MassStatusUiButtonData> aDataList) {
        for (int i = 0; i < aDataList.Count; i++) {
            if (aDataList[i] == null) {
                mButtons[i].gameObject.SetActive(false);
                continue;
            }
            mButtonTexts[i].text = aDataList[i].text;
            mPaints[i].color = aDataList[i].color;
        }
        mDataList = aDataList;
        Subject.addObserver(new Observer("massStatusUiButtons", (aMessage) => {
            switch (aMessage.name) {
                case "massStatusUiButton1Pushed":
                    mDataList[0].callback();
                    return;
                case "massStatusUiButton2Pushed":
                    mDataList[1].callback();
                    return;
                case "massStatusUiButton3Pushed":
                    mDataList[2].callback();
                    return;
                case "massStatusUiButton4Pushed":
                    mDataList[3].callback();
                    return;
                case "massStatusUiButton5Pushed":
                    mDataList[4].callback();
                    return;
            }
        }));
    }
    private void OnDestroy() {
        Subject.removeObserver("massStatusUiButtons");
    }
}
public class MassStatusUiButtonData {
    public string text;
    public Color color;
    public Action callback;
    public MassStatusUiButtonData(string aText,Color aColor,Action aCallback) {
        text = aText;
        color = aColor;
        callback = aCallback;
    }
}