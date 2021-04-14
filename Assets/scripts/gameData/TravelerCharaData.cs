using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerCharaData {
    public readonly string mName;
    public readonly string mEName;
    public readonly string mFile;
    private TravelerCharaData(string aName,string aEName,string aFile) {
        mName = aName;
        mEName = aEName;
        mFile = aFile;
    }

    public Sprite getImage() {
        return Resources.Load<Sprite>("sprites/chara/" + mFile + "/" + mFile);
    }
    public TravelerComa createComa() {
        TravelerComa tComa = GameObject.Instantiate(Resources.Load<TravelerComa>("prefabs/game/coma/travelerComa"));
        tComa.mImage.sprite = getImage();
        return tComa;
    }

    static public readonly TravelerCharaData none = new TravelerCharaData("なし", "none", "none");
    static public readonly TravelerCharaData marie = new TravelerCharaData("マリー", "Marie", "marie");
    static public readonly TravelerCharaData rear = new TravelerCharaData("リア", "Rear", "rear");
    static public readonly TravelerCharaData maru = new TravelerCharaData("マル", "Maru", "maru");
    static public readonly TravelerCharaData chiara = new TravelerCharaData("キアラ", "Chiara", "chiara");
}
