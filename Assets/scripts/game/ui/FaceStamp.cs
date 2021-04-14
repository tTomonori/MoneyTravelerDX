using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceStamp : MyBehaviour {
    public float cutRotation {
        get { return findChild<MyBehaviour>("stamp").rotateZ; }
        set { findChild<MyBehaviour>("stamp").rotateZ = value; }
    }
    static public FaceStamp create(TravelerCharaData aData,int aLayer = 0) {
        FaceStamp tStamp = GameObject.Instantiate(Resources.Load<FaceStamp>("prefabs/ui/faceStamp"));
        tStamp.findChild<SpriteRenderer>("img").sprite = aData.getImage();
        tStamp.changeLayer(aLayer, true);
        return tStamp;
    }
}
