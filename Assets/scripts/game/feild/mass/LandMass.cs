using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;

public class LandMass : GameMass {
    static public int mMaxIncreaseLevel = 3;
    public TravelerStatus mOwner = null;
    [NonSerialized] public int mIncreaseLevel = 0;
    [NonSerialized] public bool mSecondHand = false;
    [SerializeField]
    public string mName;
    public int mBaseValue;
    public List<LandAttribute> mAttributes;
    public float mFeeRate;

    public SpriteRenderer mMassRenderer;
    public TextMesh mNameMesh;
    public MyBehaviour mSellingValues;
    public MyBehaviour mSoldValues;
    public TextMesh mPurchaseMesh;
    public TextMesh mSecondHandFeeMesh;
    public TextMesh mIncreaseMesh;
    public TextMesh mFeeMesh;
    public SpriteRenderer mAttribute1Renderer;
    public SpriteRenderer mAttribute2Renderer;
    public MyBehaviour mBuildings;
    public List<MyBehaviour> mBuildingRenderers;

    public int getIncreaseCost(int aIncraseLevel) {
        return mBaseValue / 2 * (int)Mathf.Pow(3.5f - 0.5f * aIncraseLevel, aIncraseLevel);
    }
    public int getTotalValue(int aIncreaseLevel) {
        int tTotal = mBaseValue;
        for (int i = 0; i < aIncreaseLevel; i++) {
            tTotal += getIncreaseCost(i);
        }
        return tTotal;
    }
    public int mPurchaseCost {
        get {
            switch (GameData.mGameSetting.mSecondHandPrice) {
                case SecondHandPrice.initialValue:
                    return mBaseValue;
                case SecondHandPrice.currentValue:
                    return mTotalValue;
                case SecondHandPrice.acquisitionPrice:
                    return mSecondHand ? mAcquisitionCost : mBaseValue;
                case SecondHandPrice.cannotPurchase:
                    return mSecondHand ? -1 : mBaseValue;
            }
            throw new Exception();
        }
    }
    public int mFreePurchaseCost { get { return mBaseValue; } }
    public int mFeeCost { get { return (int)(mFeeRate * GameData.mGameSetting.mFee * mBaseValue / 5 * Mathf.Pow(3.25f - 0.25f * mIncreaseLevel, mIncreaseLevel)); } }
    public int mIncreaseCost { get { return getIncreaseCost(mIncreaseLevel); } }
    public int mAcquisitionCost { get { return (int)(mTotalValue * GameData.mGameSetting.mAcquisition); } }
    public int mAcquisitionTakeCost { get { return mTotalValue; } }
    public int mSellCost { get { return (int)(mTotalValue * 0.8f); } }
    public int mTotalValue { get { return getTotalValue(mIncreaseLevel); } }
    public Color mSecondHandColor { get { return new Color(0.8f, 0.8f, 0.8f, 1); } }
    //購入増資等の欄更新
    public void updateValueDisplay() {
        if (mOwner == null) {
            mSellingValues.gameObject.SetActive(true);
            mSoldValues.gameObject.SetActive(false);
            if (GameData.mGameSetting.mSecondHandPrice == SecondHandPrice.cannotPurchase && mSecondHand)
                mPurchaseMesh.text = "-";
            else
                mPurchaseMesh.text = mPurchaseCost.ToString();

            if (!GameData.mGameSetting.mSecondHandFee || !mSecondHand)
                mSecondHandFeeMesh.text = "-";
            else
                mSecondHandFeeMesh.text = mFeeCost.ToString();
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
    public void changeOrner(TravelerStatus aTraveler, Action aCallback) {
        mSecondHand = true;
        if (aTraveler == null) {
            mOwner = null;
            mMassRenderer.color = mSecondHandColor;
            updateValueDisplay();
            GameEffector.generateAura(this.worldPosition + new Vector3(0, 0.1f, 0), mSecondHandColor, aCallback);
            return;
        }
        mOwner = aTraveler;
        mMassRenderer.color = aTraveler.playerColor;
        updateValueDisplay();
        GameEffector.generateAura(this.worldPosition + new Vector3(0, 0.1f, 0), aTraveler.playerColor, aCallback);
    }
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
        mSecondHandFeeMesh.text = (mBaseValue == 0) ? "-" : mFeeCost.ToString();
        mBuildingRenderers[0].gameObject.SetActive(true);
        mAttribute1Renderer.sprite = mAttributes[0].getSprite();
        mAttribute2Renderer.sprite = mAttributes[1].getSprite();
    }
    public override void initialize() {
        updateValueDisplay();
        foreach (MyBehaviour tRenderer in mBuildingRenderers)
            tRenderer.gameObject.SetActive(false);
    }
}
//[CustomEditor(typeof(LandMass))]
//public class LandMassEditor : Editor {

//    public override void OnInspectorGUI() {
//        // 元のインスペクター部分を表示
//        base.OnInspectorGUI();
//        LandMass tLand = target as LandMass;

//        if (GUILayout.Button("changeBuilding")) {
//            string tBuildingName = "ship";
//            for (int i = 0; i < 4; i++) {
//                tLand.mBuildingRenderers[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/feild/mass/building/" + tBuildingName + i.ToString());
//            }
//        }
//    }
//}