using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultPerformer : MyBehaviour {
    static private float mGaugeMaxLength = 5.5f;
    public (List<ResultTravelerData>, List<(string, Color)>) mResult;
    [NonSerialized] public int mMaxPoint;
    [SerializeField]
    public List<TravelerResult> mTravelerResults;
    public List<ResultMiniPaper> mMiniPapers;
    public ResultTitle mResultTitle;
    public MyBehaviour mMiniPapersContainer;
    public void prepare(List<TravelerStatus> aTravelers) {
        for (int i = 0; i < GameData.mTravelerNumber; i++) {
            TravelerStatus tTraveler = aTravelers[i];
            if (tTraveler == null) {
                mTravelerResults[i].gameObject.SetActive(false);
                mMiniPapers[i].gameObject.SetActive(false);
                continue;
            }
            mTravelerResults[i].mFaceStamp.mImage.sprite = tTraveler.mTravelerData.mTravelerCharaData.getImage();
            mTravelerResults[i].mRanking.scale2D = Vector2.zero;
            if (!tTraveler.mIsRetired)
                mTravelerResults[i].mRetire.gameObject.SetActive(false);
        }
        mMiniPapersContainer.positionY = 6;
    }
    public void result((List<ResultTravelerData>, List<(string, Color)>) aResult, Action aCallback) {
        mMaxPoint = -999999;
        foreach (ResultTravelerData tData in aResult.Item1) {
            if (tData == null) continue;
            if (mMaxPoint < tData.mTotalPoint)
                mMaxPoint = tData.mTotalPoint;
        }
        Action<int> tF = null;
        tF = (aIndex) => {
            resultOne(aIndex, aResult.Item1, aResult.Item2[aIndex].Item1, aResult.Item2[aIndex].Item2, () => {
                if (aIndex + 1 < aResult.Item2.Count) {
                    tF(aIndex + 1);
                    return;
                }
                mResultTitle.setTitle("最終結果", () => {
                    MyBehaviour.setTimeoutToIns(2, () => {
                        resultRanking(aResult.Item1, aCallback);
                    });
                });
            });
        };
        tF(0);
    }
    public void resultOne(int aResultIndex, List<ResultTravelerData> aTraveler, string aResultName, Color tResultColor, Action aCallback) {
        //タイトル変更
        mResultTitle.setTitle(aResultName, () => {
            //MiniPaperの数字セット
            for (int i = 0; i < GameData.mTravelerNumber; i++) {
                if (aTraveler[i] == null) continue;
                mMiniPapers[i].set(aTraveler[i].mResultNumberAndPoint[aResultIndex].Item1);
            }
            //MiniPaper表示
            MySoundPlayer.playSe("turn", false);
            mMiniPapersContainer.moveTo(new Vector2(0, 2.5f), 0.5f, () => {
                MyBehaviour.setTimeoutToIns(2, () => {
                    CallbackSystem tSystem = new CallbackSystem();
                    for (int i = 0; i < GameData.mTravelerNumber; i++) {
                        if (aTraveler[i] == null) continue;
                        mMiniPapers[i].change(aTraveler[i].mResultNumberAndPoint[aResultIndex].Item2, tSystem.getCounter());
                    }
                    //MiniPaperの数字変更後
                    tSystem.then(() => {
                        MyBehaviour.setTimeoutToIns(2, () => {
                            tSystem = new CallbackSystem();
                            //MiniPaperのhide
                            mMiniPapersContainer.moveTo(new Vector2(0, 6), 0.5f, tSystem.getCounter());
                            //MiniPaperの数字drop
                            for (int i = 0; i < GameData.mTravelerNumber; i++) {
                                if (aTraveler[i] == null) continue;
                                mMiniPapers[i].drop(tSystem.getCounter());
                            }
                            //Gaugeを伸ばす,数字を加算する
                            MySoundPlayer.playSe("increase", false);
                            int tGaugeSpeed = 1;
                            for (int i = 0; i < GameData.mTravelerNumber; i++) {
                                ResultTravelerData tData = aTraveler[i];
                                if (tData == null) continue;
                                int tLengthNumber = tData.mResultNumberAndPoint[aResultIndex].Item2;
                                mTravelerResults[i].mResultNumber.plus(tLengthNumber, Mathf.Abs(tLengthNumber) * mGaugeMaxLength / mMaxPoint / tGaugeSpeed, tSystem.getCounter());
                                if (mTravelerResults[i].mResultNumber.getNumber() < 0) {
                                    tLengthNumber += mTravelerResults[i].mResultNumber.getNumber();
                                }
                                mTravelerResults[i].mGauge.extend(tLengthNumber * mGaugeMaxLength / mMaxPoint, tResultColor, tGaugeSpeed, tSystem.getCounter());
                            }
                            tSystem.then(aCallback);
                        });
                    });
                });
            });
        });
    }
    public void resultRanking(List<ResultTravelerData> aResultData, Action aCallback) {
        CallbackSystem tSystem = new CallbackSystem();
        for (int i = 0; i < GameData.mTravelerNumber; i++) {
            if (aResultData[i] == null) continue;
            mTravelerResults[i].mRankingRenderer.sprite = Resources.Load<Sprite>("sprites/number/ranking/" + aResultData[i].mRanking.ToString());
            mTravelerResults[i].mRanking.scaleTo(new Vector2(1, 1), 1f, tSystem.getCounter());
        }
        tSystem.then(aCallback);
    }
}
