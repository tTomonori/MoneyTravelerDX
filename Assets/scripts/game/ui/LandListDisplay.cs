using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandListDisplay : MyBehaviour {
    [NonSerialized] public List<LandMass> mLands;
    [NonSerialized] public int mPage;
    public Action<LandMass> mDetailButtonPushed;
    [SerializeField]
    public List<TextMesh> mLandNameMeshs;
    public List<TextMesh> mValueMeshs;
    public List<TextMesh> mFeeMeshs;
    public List<MyButton> mDetailButtons;
    public MyButton mNextArrowButton;
    public MyButton mPreviouseArrowButton;
    public MyButton mValueButton;
    public MyButton mFeeButton;

    public void set(List<LandMass> aLands, Action<LandMass> aDetailButtonPushed) {
        mLands = aLands;
        mPage = 0;
        mDetailButtonPushed = aDetailButtonPushed;
        setButtonCallback();
        updatePage();
    }
    public void nextPage() {
        mPage = (mPage + 1) % (int)Mathf.Ceil((mLands.Count / (float)mLandNameMeshs.Count));
        updatePage();
    }
    public void previousePage() {
        mPage = (mPage + (int)Mathf.Ceil((mLands.Count / (float)mLandNameMeshs.Count)) - 1) % (int)Mathf.Ceil((mLands.Count / (float)mLandNameMeshs.Count));
        updatePage();
    }
    public void sortByValue() {
        mLands.Sort((a, b) => a.mTotalValue - b.mTotalValue);
        mPage = 0;
        updatePage();
    }
    public void sortByFee() {
        mLands.Sort((a, b) => a.mFeeCost - b.mFeeCost);
        mPage = 0;
        updatePage();
    }
    //ページの表示更新
    public void updatePage() {
        for (int i = 0; i < mLandNameMeshs.Count; i++) {
            int tIndex = mPage * mLandNameMeshs.Count + i;
            if (mLands.Count <= tIndex) {
                mLandNameMeshs[i].text = "";
                mValueMeshs[i].text = "";
                mFeeMeshs[i].text = "";
                mDetailButtons[i].gameObject.SetActive(false);
                continue;
            }
            LandMass tLand = mLands[tIndex];
            mLandNameMeshs[i].text = tLand.mName;
            mValueMeshs[i].text = tLand.mTotalValue.ToString();
            mFeeMeshs[i].text = tLand.mFeeCost.ToString();
            mDetailButtons[i].gameObject.SetActive(true);
            mDetailButtons[i].mPushedFunction = () => {
                mDetailButtonPushed(tLand);
            };
        }
    }
    //ボタンにコールバック設定
    public void setButtonCallback() {
        mNextArrowButton.mPushedFunction = nextPage;
        mPreviouseArrowButton.mPushedFunction = previousePage;
        mValueButton.mPushedFunction = sortByValue;
        mFeeButton.mPushedFunction = sortByFee;
    }
}
