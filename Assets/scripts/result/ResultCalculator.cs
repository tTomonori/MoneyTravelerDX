using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class ResultCalculator {
    static public (List<ResultTravelerData>, List<(string, Color)>) calcurate(List<TravelerStatus> aTravelers) {
        List<ResultTravelerData> tResultTravelerDataList = new List<ResultTravelerData>();
        List<(string, Color)> tResultNameList = new List<(string, Color)>();
        foreach (TravelerStatus tTraveler in aTravelers) {
            if (tTraveler == null) {
                tResultTravelerDataList.Add(null);
                continue;
            }
            tResultTravelerDataList.Add(new ResultTravelerData(tTraveler));
        }

        //所持金
        tResultNameList.Add(("所持金", new Color(1, 1, 0)));
        foreach (ResultTravelerData tData in tResultTravelerDataList) {
            if (tData == null) continue;
            tData.mResultNumberAndPoint.Add((tData.mTraveler.mMoney, tData.mTraveler.mMoney));
            tData.mTotalPoint += tData.mTraveler.mMoney;
        }
        //物件
        tResultNameList.Add(("物件", new Color(0, 1, 0)));
        foreach (ResultTravelerData tData in tResultTravelerDataList) {
            if (tData == null) continue;
            tData.mResultNumberAndPoint.Add((tData.mTraveler.mProperty, tData.mTraveler.mProperty));
            tData.mTotalPoint += tData.mTraveler.mProperty;
        }
        if (GameData.mGameSetting.mBattleMethod is WithResultBonusMethod) {

            WithResultBonusMethod tBattleMethod = (WithResultBonusMethod)GameData.mGameSetting.mBattleMethod;
            List<bool> tTravelerGetBonus = null;

            //最大総資産
            if (tBattleMethod.mMaxAssets) {
                tResultNameList.Add(("最大総資産", new Color(0, 0, 1)));
                tTravelerGetBonus = askTravelerGetBonus(aTravelers, (a) => { return a.mMaxAssets; });
                for (int i = 0; i < tResultTravelerDataList.Count; i++) {
                    ResultTravelerData tData = tResultTravelerDataList[i];
                    if (tData == null) continue;
                    if (tTravelerGetBonus[i]) {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mMaxAssets, tBattleMethod.mResultBonus));
                        tData.mTotalPoint += tBattleMethod.mResultBonus;
                    } else {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mMaxAssets, 0));
                    }
                }
            }

            //移動マス数
            if (tBattleMethod.mMoveDistance) {
                tResultNameList.Add(("移動マス数", new Color(0, 1, 1)));
                tTravelerGetBonus = askTravelerGetBonus(aTravelers, (a) => { return a.mMoveDistance; });
                for (int i = 0; i < tResultTravelerDataList.Count; i++) {
                    ResultTravelerData tData = tResultTravelerDataList[i];
                    if (tData == null) continue;
                    if (tTravelerGetBonus[i]) {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mMoveDistance, tBattleMethod.mResultBonus));
                        tData.mTotalPoint += tBattleMethod.mResultBonus;
                    } else {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mMoveDistance, 0));
                    }
                }
            }

            //所有地の数
            if (tBattleMethod.mLandNumber) {
                tResultNameList.Add(("所有地の数", new Color(1, 0.5f, 0)));
                tTravelerGetBonus = askTravelerGetBonus(aTravelers, (a) => { return a.mLandNumber; });
                for (int i = 0; i < tResultTravelerDataList.Count; i++) {
                    ResultTravelerData tData = tResultTravelerDataList[i];
                    if (tData == null) continue;
                    if (tTravelerGetBonus[i]) {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mLandNumber, tBattleMethod.mResultBonus));
                        tData.mTotalPoint += tBattleMethod.mResultBonus;
                    } else {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mLandNumber, 0));
                    }
                }
            }

            //支払い料金
            if (tBattleMethod.mFeeAmount) {
                tResultNameList.Add(("支払い料金", new Color(1, 0, 0)));
                tTravelerGetBonus = askTravelerGetBonus(aTravelers, (a) => { return a.mFeeAmount; });
                for (int i = 0; i < tResultTravelerDataList.Count; i++) {
                    ResultTravelerData tData = tResultTravelerDataList[i];
                    if (tData == null) continue;
                    if (tTravelerGetBonus[i]) {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mFeeAmount, tBattleMethod.mResultBonus));
                        tData.mTotalPoint += tBattleMethod.mResultBonus;
                    } else {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mFeeAmount, 0));
                    }
                }
            }

            //災害被害額
            if (tBattleMethod.mDisasterDamageAmount) {
                tResultNameList.Add(("災害被害額", new Color(0.5f, 1, 0.7f)));
                tTravelerGetBonus = askTravelerGetBonus(aTravelers, (a) => { return a.mDisasterDamageAmount; });
                for (int i = 0; i < tResultTravelerDataList.Count; i++) {
                    ResultTravelerData tData = tResultTravelerDataList[i];
                    if (tData == null) continue;
                    if (tTravelerGetBonus[i]) {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mDisasterDamageAmount, tBattleMethod.mResultBonus));
                        tData.mTotalPoint += tBattleMethod.mResultBonus;
                    } else {
                        tData.mResultNumberAndPoint.Add((tData.mTraveler.mDisasterDamageAmount, 0));
                    }
                }
            }
        }
        //順位
        foreach (ResultTravelerData tData in tResultTravelerDataList) {
            if (tData == null) continue;
            int tRanling = 1;
            foreach (ResultTravelerData tComparison in tResultTravelerDataList) {
                if (tComparison == null) continue;
                if (tData.mTotalPoint >= tComparison.mTotalPoint) continue;
                tRanling++;
            }
            tData.mRanking = tRanling;
        }

        return (tResultTravelerDataList, tResultNameList);
    }
    //ボーナスを獲得できるトラベラーを決める
    static private List<bool> askTravelerGetBonus(List<TravelerStatus> aTravelers, Func<TravelerStatus, int> aFunction) {
        List<bool> tList = new List<bool>();
        int tMaxResult = -999999;
        foreach (TravelerStatus tTraveler in aTravelers) {
            if (tTraveler == null) {
                tList.Add(false);
                continue;
            }
            int tResult = aFunction(tTraveler);
            if (tMaxResult == tResult) {
                tList.Add(true);
                continue;
            }
            if (tMaxResult > tResult) {
                tList.Add(false);
                continue;
            }
            for (int i = 0; i < tList.Count; i++)
                tList[i] = false;
            tMaxResult = tResult;
            tList.Add(true);
        }
        return tList;
    }
}