﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LandMass : GameMass {
    static public int mMaxIncreaseLevel = 3;
    [NonSerialized]public int mOwner = 0;
    [NonSerialized]public int mIncreaseLevel = 0;
    [SerializeField]
    public string mName;
    public int mBaseValue;
    public List<LandAttribute> mAttributes;
    public int mFeeRate;

    public SpriteRenderer mMassMesh;
    public TextMesh mNameMesh;
    public MyBehaviour mSellingValues;
    public MyBehaviour mSoldValues;
    public TextMesh mPurchaseMesh;
    public TextMesh mIncreaseMesh;
    public TextMesh mFeeMesh;
    public SpriteRenderer mAttribute1Renderer;
    public SpriteRenderer mAttribute2Renderer;
    public MyBehaviour mBuildings;
    public List<MyBehaviour> mBuildingRenderers;

    public int getIncreaseCost(int aIncraseLevel) {
        return mBaseValue / 2 * (int)Mathf.Pow(2, aIncraseLevel);
    }
    public int mPurchaseCost { get { return mBaseValue; } }
    public int mFeeCost { get { return (int)(GameData.mGameSetting.mFee * mBaseValue / 5 * Mathf.Pow(3, mIncreaseLevel)); } }
    public int mIncreaseCost { get { return getIncreaseCost(mIncreaseLevel); } }
    public int mAcquisitionCost { get { return (int)(mTotalValue * GameData.mGameSetting.mAcquisition); } }
    public int mSellCost { get { return (int)(mTotalValue * 0.8f); } }
    public int mTotalValue {
        get {
            int tTotal = mBaseValue;
            for (int i = 0; i < mIncreaseLevel; i++) {
                tTotal += getIncreaseCost(i);
            }
            return tTotal;
        }
    }
    //購入増資等の欄更新
    public void updateValueDisplay() {
        if (mOwner <= 0) {
            mSellingValues.gameObject.SetActive(true);
            mSoldValues.gameObject.SetActive(false);
            mPurchaseMesh.text = mPurchaseCost.ToString();
        } else {
            mSellingValues.gameObject.SetActive(false);
            mSoldValues.gameObject.SetActive(true);
            mIncreaseMesh.text = mIncreaseCost.ToString();
            mFeeMesh.text = mFeeCost.ToString();
            if (mIncreaseLevel == mMaxIncreaseLevel) {
                mIncreaseMesh.text = "MAX";
            }
        }
    }
    //オーナー変更
    //public void changeOrner(PlayerStatus aStatus, Action aCallback) {
    //    if (aStatus == null) {
    //        mOwner = 0;
    //        mMass.color = new Color(1, 1, 1, 1);
    //        updateValueDisplay();
    //        aCallback();
    //        return;
    //    }
    //    mOwner = aStatus.mPlayerNumber;
    //    mMass.color = aStatus.playerColor;
    //    updateValueDisplay();
    //    aCallback();
    //}
    //増資レベル変更
    public void changeIncreaseLevel(int aLevel, Action aCallback) {
        mBuildingRenderers[mIncreaseLevel].gameObject.SetActive(false);
        mBuildingRenderers[aLevel].gameObject.SetActive(true);
        MySoundPlayer.playSe("increase", false);
        mIncreaseLevel = aLevel;
        updateValueDisplay();
        mBuildings.scale2D = new Vector2(0, 0);
        mBuildings.scaleTo(new Vector2(0.2f, 1.3f), 0.2f, () => {
            mBuildings.scaleTo(new Vector2(1.3f, 0.6f), 0.2f, () => {
                mBuildings.scaleTo(new Vector2(1, 1), 0.2f, () => {
                    aCallback();
                });
            });
        });
    }
    private void OnValidate() {
        setTest();
    }
    public void setTest() {
        mNameMesh.text = (mName == "") ? "地名" : mName;
        mPurchaseMesh.text = (mBaseValue == 0) ? "9999" : mPurchaseCost.ToString();
        mBuildingRenderers[0].gameObject.SetActive(true);
        mAttribute1Renderer.sprite = mAttributes[0].getSprite();
        mAttribute2Renderer.sprite = mAttributes[1].getSprite();
    }
    public void initialize() {
        updateValueDisplay();
        foreach (MyBehaviour tRenderer in mBuildingRenderers)
            tRenderer.gameObject.SetActive(false);
    }
}