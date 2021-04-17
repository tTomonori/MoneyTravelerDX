using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMain : MyBehaviour {
    private Arg mArg;
    public (List<ResultTravelerData>, List<(string, Color)>) mResult;
    [SerializeField]
    public ResultPerformer mPerformer;
    public MyButton mBackButton;
    public GameObject mCover;
    private void Start() {
        mArg = MySceneManager.getArg("result");
        if (MySceneManager.fadeCallbacks == null) {
            mArg = new Arg(new Dictionary<string, object>() { { "travelers", createTravelerSampleResult() } });
        }
        mPerformer.prepare(mArg.get<List<TravelerStatus>>("travelers"));
        mBackButton.gameObject.SetActive(false);
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
            MySceneManager.changeSceneWithFade("title", "curtainFade");
        };
    }
    private List<TravelerStatus> createTravelerSampleResult() {
        List<TravelerStatus> tList = new List<TravelerStatus>();
        TravelerStatus tTraveler = null;

        tList.Add(null);

        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.carefully;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.rear;
        tTraveler.mMoney = 500;
        tTraveler.mProperty = 753;
        tTraveler.mOrbit = 120;
        tTraveler.mRanking = 1;
        tTraveler.mPlayerNumber = 2;
        tTraveler.mCurrentMassNumber = 8;
        tTraveler.mIsRetired = false;
        tTraveler.mMaxAssets = 83990;
        tTraveler.mMoveDistance = 120;
        tTraveler.mLandNumber = 12;
        tTraveler.mFeeAmount = 982;
        tTraveler.mDisasterDamageAmount = 1393;
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
        tTraveler.mMaxAssets = 12380;
        tTraveler.mMoveDistance = 120;
        tTraveler.mLandNumber = 13;
        tTraveler.mFeeAmount = 3983;
        tTraveler.mDisasterDamageAmount = 786;
        tList.Add(tTraveler);


        tTraveler = new TravelerStatus();
        tTraveler.mTravelerData = new TravelerData();
        tTraveler.mTravelerData.mAiPattern = TravelerAiPattern.impulse;
        tTraveler.mTravelerData.mTravelerCharaData = TravelerCharaData.chiara;
        tTraveler.mMoney = -421;
        tTraveler.mProperty = 0;
        tTraveler.mOrbit = 103;
        tTraveler.mRanking = 3;
        tTraveler.mPlayerNumber = 4;
        tTraveler.mCurrentMassNumber = 23;
        tTraveler.mIsRetired = true;
        tTraveler.mMaxAssets = 2133;
        tTraveler.mMoveDistance = 120;
        tTraveler.mLandNumber = 0;
        tTraveler.mFeeAmount = 7834;
        tTraveler.mDisasterDamageAmount = 1432;
        tList.Add(tTraveler);

        LapGoalMethod tMethod = new LapGoalMethod();
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
