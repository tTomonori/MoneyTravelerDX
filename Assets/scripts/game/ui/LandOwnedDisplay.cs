using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandOwnedDisplay : MyBehaviour {
    [SerializeField]
    public SpriteRenderer mBack;
    public FaceStamp mFaceStamp;
    public TextMesh mNameMesh;
    public LandListDisplay mLandListDisplay;
    public MassStatusUiButtons mButtons;
    static public LandOwnedDisplay create(TravelerStatus aTraveler, List<LandMass> aLans, Action<LandMass> aDetailButtonPushed, List<MassStatusUiButtonData> aUiButtondata) {
        LandOwnedDisplay tDisplay = GameObject.Instantiate(Resources.Load<LandOwnedDisplay>("prefabs/game/ui/landOwnedDisplay"));
        tDisplay.set(aTraveler, aLans, aDetailButtonPushed, aUiButtondata);
        return tDisplay;
    }
    public void set(TravelerStatus aTraveler, List<LandMass> aLans, Action<LandMass> aDetailButtonPushed, List<MassStatusUiButtonData> aUiButtondata) {
        mBack.color = aTraveler.playerColor;
        mBack.color = new Color(mBack.color.r, mBack.color.g, mBack.color.b, 0.3f);
        mFaceStamp.mImage.sprite = aTraveler.mTravelerData.mTravelerCharaData.getImage();
        mNameMesh.text = aTraveler.mTravelerData.mTravelerCharaData.mName + "の所有地";
        mLandListDisplay.set(aLans, aDetailButtonPushed);
        mButtons.setButtons(aUiButtondata);
    }
    public void close() {
        delete();
    }
}
