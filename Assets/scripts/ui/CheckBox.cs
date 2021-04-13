using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MyBehaviour {
    [SerializeField]
    public bool mIsCheck;
    public MyBehaviour mCheck;
    public string mMessage;
    private void Awake() {
        if (mIsCheck) mCheck.scale2D = new Vector2(1, 1);
        else mCheck.scale2D = Vector2.zero;
    }
    //チェックする
    public void check() {
        if (mIsCheck == true) return;
        mIsCheck = true;
        mCheck.scaleTo(new Vector2(1, 1), 0.1f);
        MySoundPlayer.playSe("drow", false);
    }
    //チェックを外す
    public void uncheck() {
        if (mIsCheck == false) return;
        mIsCheck = false;
        mCheck.scaleTo(Vector2.zero, 0.1f);
        MySoundPlayer.playSe("drow", false);
    }
    //チェックを反転させる
    public void invertCheck() {
        if (mIsCheck) uncheck();
        else check();
    }
    //アニメーションせずにcheck適用
    public void set(bool mCheckBool) {
        mIsCheck = mCheckBool;
        if (mIsCheck) mCheck.scale2D = new Vector2(1, 1);
        else mCheck.scale2D = Vector2.zero;
    }
    private void OnMouseUpAsButton() {
        if (mMessage == "") return;
        Subject.sendMessage(new Message(mMessage, new Arg(new Dictionary<string, object>() { { "object", this } })));
    }
}
