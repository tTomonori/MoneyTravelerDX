﻿using System.Collections;
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
    public TextMesh mIncreaseMesh;
    public TextMesh mAcquisitionMesh;
    public TextMesh mPurchaseMesh;
    public TextMesh mSellMesh;
    public TextMesh mIncreaseLevelMesh;
    public override void setStatus(GameMass aMass) {
        LandMass tLand = (LandMass)aMass;
        if (tLand.mOwner == null) {
            mOwnerStamp.gameObject.SetActive(false);
            mOwnerNameMesh.text = "";
        } else {
            mOwnerStamp.mImage.sprite = tLand.mOwner.mTravelerData.mTravelerCharaData.getImage();
            mOwnerNameMesh.text = tLand.mOwner.mTravelerData.mTravelerCharaData.mName;
        }
        mNameMesh.text = tLand.mName;
        mTotalValueMesh.text = tLand.mTotalValue.ToString();
        mFeeMesh.text = tLand.mFeeCost.ToString();
        mIncreaseMesh.text = tLand.mIncreaseCost.ToString();
        mAcquisitionMesh.text = tLand.mAcquisitionCost.ToString();
        mPurchaseMesh.text = tLand.mPurchaseCost.ToString();
        mSellMesh.text = tLand.mSellCost.ToString();
        mIncreaseLevelMesh.text = tLand.mIncreaseLevel.ToString();
    }
}
