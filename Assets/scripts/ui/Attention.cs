using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attention : MyBehaviour {
    static public List<string> mWaiting = new List<string>();
    static private Attention mAttention;
    [SerializeField] private TextMesh mText;
    public enum OverlapType {
        truncate, waite
    }
    static public void attention(string aMessage, OverlapType aOverlapType) {
        if (mAttention == null) {
            create();
            mAttention.mText.text = aMessage;
            MySoundPlayer.playSe("attention", false);
            mAttention.showMessage();
            return;
        }
        switch (aOverlapType) {
            case OverlapType.truncate:
                return;
            case OverlapType.waite:
                mWaiting.Add(aMessage);
                return;
        }
    }
    static private void create() {
        mAttention = GameObject.Instantiate<Attention>(Resources.Load<Attention>("prefabs/ui/attention"));
        mAttention.position2D = new Vector2(3.5f, 6f);
    }
    //メッセージを表示
    private void showMessage() {
        mAttention.moveTo(new Vector2(3.5f, 4.25f), 0.5f, () => {
            MyBehaviour.setTimeoutToIns(2f, () => {
                mAttention.moveTo(new Vector2(3.5f, 6f), 0.5f, () => {
                    showedMessage();
                });
            });
        });
    }
    //メッセージの表示が終了した
    static private void showedMessage() {
        if (mWaiting.Count == 0) {
            mAttention.delete();
            return;
        }
        mAttention.mText.text = mWaiting[0];
        mWaiting.RemoveAt(0);
        mAttention.showMessage();
    }
}
