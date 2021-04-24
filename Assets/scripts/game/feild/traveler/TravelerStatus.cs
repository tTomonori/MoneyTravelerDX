using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerStatus {
    static public int mRetiredCounter;
    public TravelerAi mAi;
    public TravelerData mTravelerData;
    public int mMoney;
    public int mProperty;
    public int mAssets { get { return mMoney + mProperty; } }
    public int mOrbit;
    public int mRanking;
    public int mPlayerNumber;
    public int mCurrentMassNumber;
    public TravelerComa mComa;
    public bool mIsRetired;
    public int mRetiredNumber;

    public int mMaxAssets;
    public int mMoveDistance;
    public int mLandNumber;
    public int mFeeAmount;
    public int mDisasterDamageAmount;
    public List<int> mAssetsTransitionList;

    public Color playerColor {
        get {
            switch (mPlayerNumber) {
                case 1: return new Color(1, 0.1f, 0.1f, 1);
                case 2: return new Color(0.2f, 0.4f, 1, 1);
                case 3: return new Color(1, 1, 0, 1);
                case 4: return new Color(0, 1, 0, 1);
                case 5: return new Color(1, 0.2f, 1, 1);
                case 6: return new Color(1, 0.6f, 0.2f, 1);
                default: return new Color(1, 1, 1, 1);
            }
        }
    }
}
