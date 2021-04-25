using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandMassStatusDisplay : MassStatusDisplay {
    [SerializeField]
    public SpriteRenderer mBack;
    public FaceStamp mOwnerStamp;
    public TextMesh mOwnerNameMesh;
    public TextMesh mNameMesh;
    public TextMesh mTotalValueMesh;
    public TextMesh mFeeMesh;
    public TextMesh mFeeRateMesh;
    public TextMesh mIncreaseMesh;
    public TextMesh mAcquisitionMesh;
    public TextMesh mPurchaseMesh;
    public TextMesh mSellMesh;
    public TextMesh mIncreaseLevelMesh;
    public SpriteRenderer mAttributeRenderer1;
    public SpriteRenderer mAttributeRenderer2;
    public TextMesh mAttributeNameMesh1;
    public TextMesh mAttributeNameMesh2;
    public override void setStatus(GameMass aMass) {
        LandMass tLand = (LandMass)aMass;
        if (tLand.mOwner == null) {
            mOwnerStamp.gameObject.SetActive(false);
            mOwnerNameMesh.text = "";
        } else {
            mBack.color = tLand.mOwner.playerColor;
            mBack.color = new Color(mBack.color.r, mBack.color.g, mBack.color.b, 0.3f);
            mOwnerStamp.mImage.sprite = tLand.mOwner.mTravelerData.mTravelerCharaData.getImage();
            mOwnerNameMesh.text = tLand.mOwner.mTravelerData.mTravelerCharaData.mName;
        }
        mNameMesh.text = tLand.mName;
        mTotalValueMesh.text = tLand.mTotalValue.ToString();
        mFeeMesh.text = tLand.mFeeCost.ToString();
        mFeeRateMesh.text = tLand.mFeeRate.ToString();
        if (tLand.mIncreaseLevel >= LandMass.mMaxIncreaseLevel)
            mIncreaseMesh.text = "MAX";
        else
            mIncreaseMesh.text = tLand.mIncreaseCost.ToString();
        mAcquisitionMesh.text = tLand.mAcquisitionCost.ToString();
        mPurchaseMesh.text = tLand.mPurchaseCost.ToString();
        mSellMesh.text = tLand.mSellCost.ToString();
        mIncreaseLevelMesh.text = tLand.mIncreaseLevel.ToString();
        mAttributeRenderer1.sprite = tLand.mAttributes[0].getSprite();
        mAttributeRenderer2.sprite = tLand.mAttributes[1].getSprite();
        mAttributeNameMesh1.text = tLand.mAttributes[0].getName();
        mAttributeNameMesh2.text = tLand.mAttributes[1].getName();
    }
}
