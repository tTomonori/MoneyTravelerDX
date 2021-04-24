using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMain : MyBehaviour {
    private Arg mArg;
    public (List<ResultTravelerData>, List<(string, Color)>) mResult;
    [SerializeField]
    public ResultPerformer mPerformer;
    public MyButton mBackButton;
    public MyButton mGraphButton;
    public GameObject mCover;
    public MyBehaviour mGraphContaier;
    private void Start() {
        mArg = MySceneManager.getArg("result");
        if (MySceneManager.fadeCallbacks == null) {
            mArg = new Arg(new Dictionary<string, object>() { { "travelers", createTravelerSampleResult() } });
        }
        mPerformer.prepare(mArg.get<List<TravelerStatus>>("travelers"));
        mBackButton.gameObject.SetActive(false);
        mGraphButton.gameObject.SetActive(false);
        mResult = ResultCalculator.calcurate(mArg.get<List<TravelerStatus>>("travelers"));

        if (MySceneManager.fadeCallbacks == null) {
            result();
            return;
        }
        MySceneManager.fadeCallbacks.fadeInFinished = () => {
            result();
        };
        MySceneManager.fadeCallbacks.nextSceneReady();
    }
    private void result() {
        MySoundPlayer.playBgm("勝利のマーチ", MySoundPlayer.LoopType.normalConnect, 0.6f);
        MyBehaviour.setTimeoutToIns(3, () => {
            mPerformer.result(mResult, () => {
                MyBehaviour.setTimeoutToIns(2, endResult);
            });
        });
    }
    private void endResult() {
        ConfettiSystem tSystem = this.createChild<ConfettiSystem>("confettiSystem");
        tSystem.mTop = 6;
        tSystem.mBottom = -6;
        tSystem.mLeft = -8.5f;
        tSystem.mRight = 8.5f;
        tSystem.mCreateParSecond = 10;
        tSystem.mIsCreating = true;
        tSystem.positionZ = -5;

        mBackButton.gameObject.SetActive(true);
        mBackButton.mPushedFunction = () => {
            mCover.SetActive(true);
            MySoundPlayer.fadeBgm(1.5f, 0, () => {
                MySoundPlayer.stopBgm();
            });
            MySceneManager.changeSceneWithFade("title", "curtainFade");
        };
        mGraphButton.gameObject.SetActive(true);
        mGraphButton.mPushedFunction = () => {
            mCover.SetActive(true);
            AssetsGraph tGraph = AssetsGraph.show(mResult.Item1, () => {
                mCover.SetActive(false);
            });
            tGraph.transform.SetParent(mGraphContaier.transform, false);
        };
    }
    private List<TravelerStatus> createTravelerSampleResult() {
        List<TravelerStatus> tList = new List<TravelerStatus>();
        TravelerStatus tTraveler = null;

        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.carefully;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.marie;
        tTraveler.mMoney = -600;
        tTraveler.mProperty = 0;
        tTraveler.mOrbit = 120;
        tTraveler.mRanking = 6;
        tTraveler.mPlayerNumber = 1;
        tTraveler.mCurrentMassNumber = 8;
        tTraveler.mIsRetired = false;
        tTraveler.mMaxAssets = 13990;
        tTraveler.mMoveDistance = 134;
        tTraveler.mLandNumber = 0;
        tTraveler.mFeeAmount = 982;
        tTraveler.mDisasterDamageAmount = 1393;
        tTraveler.mAssetsTransitionList = new List<int>() { 1000, 400, 200, -600, -600, -600 };
        tTraveler.mIsRetired = true;
        tTraveler.mRetiredNumber = 1;
        tList.Add(tTraveler);

        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.carefully;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.rear;
        tTraveler.mMoney = 2000;
        tTraveler.mProperty = 753;
        tTraveler.mOrbit = 120;
        tTraveler.mRanking = 1;
        tTraveler.mPlayerNumber = 2;
        tTraveler.mCurrentMassNumber = 8;
        tTraveler.mIsRetired = false;
        tTraveler.mMaxAssets = 3990;
        tTraveler.mMoveDistance = 120;
        tTraveler.mLandNumber = 12;
        tTraveler.mFeeAmount = 982;
        tTraveler.mDisasterDamageAmount = 1393;
        tTraveler.mAssetsTransitionList = new List<int>() { 1000, 2000, 3000, 4000, 3000, 2000 };
        tList.Add(tTraveler);

        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.solid;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.maru;
        tTraveler.mMoney = 203;
        tTraveler.mProperty = 436;
        tTraveler.mOrbit = 118;
        tTraveler.mRanking = 2;
        tTraveler.mPlayerNumber = 3;
        tTraveler.mCurrentMassNumber = 5;
        tTraveler.mIsRetired = false;
        tTraveler.mMaxAssets = 4380;
        tTraveler.mMoveDistance = 120;
        tTraveler.mLandNumber = 13;
        tTraveler.mFeeAmount = 3983;
        tTraveler.mDisasterDamageAmount = 786;
        tTraveler.mAssetsTransitionList = new List<int>() { 1000, 2300, 2311, 1000, 876, 203 };
        tList.Add(tTraveler);


        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.impulse;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.chiara;
        tTraveler.mMoney = -421;
        tTraveler.mProperty = 0;
        tTraveler.mOrbit = 103;
        tTraveler.mRanking = 5;
        tTraveler.mPlayerNumber = 4;
        tTraveler.mCurrentMassNumber = 23;
        tTraveler.mIsRetired = true;
        tTraveler.mMaxAssets = 2133;
        tTraveler.mMoveDistance = 120;
        tTraveler.mLandNumber = 0;
        tTraveler.mFeeAmount = 7834;
        tTraveler.mDisasterDamageAmount = 1432;
        tTraveler.mAssetsTransitionList = new List<int>() { 1000, 1000, 500, 300, -100, -100 };
        tTraveler.mIsRetired = true;
        tTraveler.mRetiredNumber = 2;
        tList.Add(tTraveler);

        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.buyer;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.hana;
        tTraveler.mMoney = 239;
        tTraveler.mProperty = 392;
        tTraveler.mOrbit = 113;
        tTraveler.mRanking = 3;
        tTraveler.mPlayerNumber = 5;
        tTraveler.mCurrentMassNumber = 38;
        tTraveler.mIsRetired = true;
        tTraveler.mMaxAssets = 2133;
        tTraveler.mMoveDistance = 120;
        tTraveler.mLandNumber = 3;
        tTraveler.mFeeAmount = 2397;
        tTraveler.mDisasterDamageAmount = 1983;
        tTraveler.mAssetsTransitionList = new List<int>() { 1000, 1200, 304, 532, 234, 239 };
        tTraveler.mIsRetired = false;
        tTraveler.mRetiredNumber = 0;
        tList.Add(tTraveler);

        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.buyer;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.donna;
        tTraveler.mMoney = 1340;
        tTraveler.mProperty = 4977;
        tTraveler.mOrbit = 113;
        tTraveler.mRanking = 2;
        tTraveler.mPlayerNumber = 6;
        tTraveler.mCurrentMassNumber = 38;
        tTraveler.mIsRetired = true;
        tTraveler.mMaxAssets = 3421;
        tTraveler.mMoveDistance = 118;
        tTraveler.mLandNumber = 3;
        tTraveler.mFeeAmount = 6839;
        tTraveler.mDisasterDamageAmount = 997;
        tTraveler.mAssetsTransitionList = new List<int>() { 1000, 1200, 348, 893, 1184, 1340 };
        tTraveler.mIsRetired = false;
        tTraveler.mRetiredNumber = 0;
        tList.Add(tTraveler);

        LapGoalMethod tMethod = new LapGoalMethod();
        tMethod.mResultBonus = 1000;
        tMethod.mGoalLap = 120;
        tMethod.mMaxAssets = true;
        tMethod.mMoveDistance = true;
        tMethod.mLandNumber = true;
        tMethod.mFeeAmount = true;
        tMethod.mDisasterDamageAmount = true;
        GameData.mGameSetting.mBattleMethod = tMethod;

        return tList;
    }
}
