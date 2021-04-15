using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMass : GameMass {
    [SerializeField]
    public int mBonus;
    public int mExactlyBonus;
    public int mStopBonus { get { return (int)GameData.mGameSetting.mBonus * (mBonus + mExactlyBonus); } }
    public int mPassBonus { get { return (int)GameData.mGameSetting.mBonus * mBonus; } }
}
