using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting {
    public List<TravelerData> mTravelerData;
    public float mInitialMoney;
    public float mFee;
    public float mBonus;
    public float mAcquisition;
    public AcquisitionCondition mAcquisitionCondition;
    public float mDisasterDamage;
    public bool mSecondHandFee;
    public SecondHandPrice mSecondHandPrice;
    public OrbitEventType mOrbitEventType;
    public string mStageSceneName;
    public string mStageName;
    public BattleMethod mBattleMethod;
}