using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerStatusDisplay : MyBehaviour {
    [SerializeField]
    public SpriteRenderer mBackRenderer;
    public SpriteRenderer mCharaRenderer;
    public MyBehaviour mRanking;
    public SpriteRenderer mRankingRenderer;
    public int mDisplayedRanking;
    public TextMesh mNameMesh;
    public TextMesh mOrbitMesh;
    public TextMesh mMoneyMesh;
    public TextMesh mPropertyMesh;
    public TextMesh mLandNumberMesh;
    public TextMesh mAssetsMesh;
    public MyBehaviour mBancruptcy;
    public MyBehaviour mSanOfBancruptcy;
    public MyBehaviour mRetire;

    public void initialize(TravelerStatus aTraveler) {
        mBackRenderer.color = aTraveler.playerColor;
        mCharaRenderer.sprite = aTraveler.mTravelerData.mTravelerCharaData.getImage();
        mNameMesh.text = aTraveler.mTravelerData.mTravelerCharaData.mName;
        updateStatus(aTraveler);
    }
    public void updateStatus(TravelerStatus aTraveler) {
        updateRanking(aTraveler);
        mOrbitMesh.text = aTraveler.mOrbit.ToString();
        mMoneyMesh.text = aTraveler.mMoney.ToString();
        mPropertyMesh.text = aTraveler.mProperty.ToString();
        mLandNumberMesh.text = aTraveler.mLandNumber.ToString();
        mAssetsMesh.text = aTraveler.mAssets.ToString();
        if (aTraveler.mMoney >= 0) {
            mBancruptcy.gameObject.SetActive(false);
        } else if (!mBancruptcy.gameObject.activeSelf) {
            animateBancruptcy();
        }
        if (aTraveler.mIsRetired && !mRetire.gameObject.activeSelf)
            animateRetire();
    }
    public void updateRanking(TravelerStatus aTraveler) {
        if (mDisplayedRanking == aTraveler.mRanking) {
            return;
        }
        mDisplayedRanking = aTraveler.mRanking;
        mRankingRenderer.sprite = Resources.Load<Sprite>("sprites/number/ranking/" + mDisplayedRanking.ToString());
        //アニメーション
        mRanking.scale2D -= new Vector2(1, 1);
        mRanking.scaleBy(new Vector2(0.4f, 1.3f), 0.15f, () => {
            mRanking.scaleBy(new Vector2(0.8f, -0.5f), 0.15f, () => {
                mRanking.scaleBy(new Vector2(-0.2f, 0.2f), 0.15f);
            });
        });
    }
    public void animateBancruptcy() {
        mBancruptcy.gameObject.SetActive(true);
        mSanOfBancruptcy.positionY = 0;
        mSanOfBancruptcy.rotateZ = 0;
        mBancruptcy.position2D = new Vector2(0, 0.5f);
        mBancruptcy.moveTo(new Vector2(0, 0), 0.2f, () => {
            MyBehaviour.setTimeoutToIns(0.5f, () => {
                mSanOfBancruptcy.moveBy(new Vector2(0, -0.2f),0.2f);
                mSanOfBancruptcy.rotateZBy(-10, 0.2f);
            });
        });
    }
    public void animateRetire() {
        mRetire.gameObject.SetActive(true);
        mRetire.position2D = new Vector2(0, 1);
        mRetire.opacityBy(-0.1f, 0, () => {
            mRetire.moveTo(new Vector2(0, 0), 1);
            mRetire.opacityBy(1f, 1);
        });
    }
}
