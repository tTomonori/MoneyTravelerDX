using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class FlowerBoadSetting {
    public MyBehaviour mBoard;
    static public int t;
    //ボードをロードして表示
    public void displayBoard(string aBoardName, Action aCallback, Action aInstantiated = null) {
        mBoard = GameObject.Instantiate<MyBehaviour>(Resources.Load<MyBehaviour>("prefabs/setting/flowerBoard/" + aBoardName));
        mBoard.transform.SetParent(GameObject.Find("settingBoard").transform, false);
        mBoard.positionY = 8;
        if (aInstantiated != null) aInstantiated();
        mBoard.moveTo(Vector2.zero, 0.3f, aCallback);
        MySoundPlayer.playSe("boardDown");
    }
    //ボードを消す
    public void hadeBoard(Action aCallback) {
        mBoard.moveTo(new Vector2(0, 8), 0.3f, () => {
            mBoard.delete();
            aCallback();
        });
        MySoundPlayer.playSe("boardUp");
    }

    abstract public void set(Action aCallback);
}
