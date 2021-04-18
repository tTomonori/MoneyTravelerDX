using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class PaperBoardSetting : MyBehaviour {
    static public PaperBoardSetting mBoard;
    static public Action mCallback;
    //ボードをロードして表示
    static public void display(string aBoardName, Action aCallback) {
        mBoard = GameObject.Instantiate<PaperBoardSetting>(Resources.Load<PaperBoardSetting>("prefabs/setting/paperBoard/" + aBoardName));
        mCallback = aCallback;
        mBoard.transform.SetParent(GameObject.Find("settingPaper").transform, false);
        mBoard.positionY = 8;
        mBoard.changeLayer(10);
        mBoard.moveTo(Vector2.zero, 0.3f, mBoard.displayed);
        MySoundPlayer.playSe("turn");
    }
    //ボードを消す
    static public void hadeBoard() {
        mBoard.moveTo(new Vector2(0, 8), 0.3f, () => {
            mBoard.delete();
            mCallback();
        });
        MySoundPlayer.playSe("turn");
    }

    abstract public void displayed();
}
