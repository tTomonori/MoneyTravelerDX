using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameData {
    static public readonly int mTravelerNumber = 6;
    static public GameSetting mGameSetting;
    static public StageData mStageData;
    static GameData() {
        mGameSetting = new GameSetting();
        mGameSetting.mInitialMoney = 1;
        mGameSetting.mFee = 1;
        mGameSetting.mBonus = 1;
        mGameSetting.mAcquisition = 2;
        mGameSetting.mAcquisitionCondition = AcquisitionCondition.soldOut;
        mGameSetting.mDisasterDamage = 1;
        mGameSetting.mSecondHandFee = true;
        mGameSetting.mSecondHandPrice = SecondHandPrice.currentValue;
        mGameSetting.mOrbitEventType = OrbitEventType.always;
        mGameSetting.mStageSceneName = "standard";
        mGameSetting.mStageName = "スタンダード";
        mGameSetting.mBattleMethod = new BottomConfirmedMethod();

        mGameSetting.mTravelerData = new List<TravelerData>();
        TravelerData tCharaData;
        tCharaData = new TravelerData();
        tCharaData.mTravelerCharaData = TravelerCharaData.marie;
        tCharaData.mAiPattern = TravelerAiPattern.player;
        mGameSetting.mTravelerData.Add(tCharaData);

        tCharaData = new TravelerData();
        tCharaData.mTravelerCharaData = TravelerCharaData.rear;
        tCharaData.mAiPattern = TravelerAiPattern.carefully;
        mGameSetting.mTravelerData.Add(tCharaData);

        tCharaData = new TravelerData();
        tCharaData.mTravelerCharaData = TravelerCharaData.maru;
        tCharaData.mAiPattern = TravelerAiPattern.solid;
        mGameSetting.mTravelerData.Add(tCharaData);

        tCharaData = new TravelerData();
        tCharaData.mTravelerCharaData = TravelerCharaData.chiara;
        tCharaData.mAiPattern = TravelerAiPattern.impulse;
        mGameSetting.mTravelerData.Add(tCharaData);

        tCharaData = new TravelerData();
        tCharaData.mTravelerCharaData = TravelerCharaData.hana;
        tCharaData.mAiPattern = TravelerAiPattern.increaser;
        mGameSetting.mTravelerData.Add(tCharaData);

        tCharaData = new TravelerData();
        tCharaData.mTravelerCharaData = TravelerCharaData.donna;
        tCharaData.mAiPattern = TravelerAiPattern.buyer;
        mGameSetting.mTravelerData.Add(tCharaData);
    }
}
