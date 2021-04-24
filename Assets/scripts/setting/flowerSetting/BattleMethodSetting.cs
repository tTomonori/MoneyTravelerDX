using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleMethodSetting : FlowerBoadSetting {
    public List<MyBehaviour> mBattleMethodCheckContainers;
    public List<MyBehaviour> mMethodMiniSettingContainers;
    public TextMesh mGoalAmountMesh;
    public TextMesh mTurnLimitsMesh;
    public TextMesh mGoalLapMesh;
    public MyBehaviour mResultBonusSettingContainer;
    public TextMesh mBonusAmountMesh;
    public List<MyBehaviour> mBonusCheckContainers;
    public override void set(Action aCallback) {
        displayBoard("battleMethodSetting", () => {
            Subject.addObserver(new Observer("battleMethodSetting", (aMessage) => {
                switch (aMessage.name) {
                    //対戦方式欄
                    case "losing":
                        if (GameData.mGameSetting.mBattleMethod is LosingMethod) return;
                        GameData.mGameSetting.mBattleMethod = new LosingMethod();
                        changeBattleMethod(true);
                        return;
                    case "assetsGoal":
                        if (GameData.mGameSetting.mBattleMethod is AssetsGoalMethod) return;
                        GameData.mGameSetting.mBattleMethod = new AssetsGoalMethod();
                        changeBattleMethod(true);
                        return;
                    case "turnLimits":
                        if (GameData.mGameSetting.mBattleMethod is TurnLimitsMethod) return;
                        GameData.mGameSetting.mBattleMethod = new TurnLimitsMethod();
                        changeBattleMethod(true);
                        return;
                    case "lapGoal":
                        if (GameData.mGameSetting.mBattleMethod is LapGoalMethod) return;
                        GameData.mGameSetting.mBattleMethod = new LapGoalMethod();
                        changeBattleMethod(true);
                        return;
                    case "bottomConfirmed":
                        if (GameData.mGameSetting.mBattleMethod is BottomConfirmedMethod) return;
                        GameData.mGameSetting.mBattleMethod = new BottomConfirmedMethod();
                        changeBattleMethod(true);
                        return;
                    //対戦方式別設定欄
                    case "settingUpArrowPushed":
                        MySoundPlayer.playSe("hit",false);
                        switch (GameData.mGameSetting.mBattleMethod) {
                            case AssetsGoalMethod tAssetsGoal:
                                int tGoalAmountUp = int.Parse(mGoalAmountMesh.text);
                                if (tGoalAmountUp >= 50000) return;
                                tGoalAmountUp += 1000;
                                mGoalAmountMesh.text = tGoalAmountUp.ToString();
                                tAssetsGoal.mGoalAmount = tGoalAmountUp;
                                return;
                            case TurnLimitsMethod tTurnLimits:
                                int tTurnLimitsUp = int.Parse(mTurnLimitsMesh.text);
                                if (tTurnLimitsUp >= 500) return;
                                tTurnLimitsUp += 5;
                                mTurnLimitsMesh.text = tTurnLimitsUp.ToString();
                                tTurnLimits.mLimitsTurn = tTurnLimitsUp;
                                return;
                            case LapGoalMethod tLapGoal:
                                int tGoalLapUp = int.Parse(mGoalLapMesh.text);
                                if (tGoalLapUp >= 100) return;
                                tGoalLapUp += 1;
                                mGoalLapMesh.text = tGoalLapUp.ToString();
                                tLapGoal.mGoalLap = tGoalLapUp;
                                return;
                        }
                        return;
                    case "settingDownArrowPushed":
                        MySoundPlayer.playSe("hit",false);
                        switch (GameData.mGameSetting.mBattleMethod) {
                            case AssetsGoalMethod tAssetsGoal:
                                int tGoalAmountDown = int.Parse(mGoalAmountMesh.text);
                                if (tGoalAmountDown <= 1000) return;
                                tGoalAmountDown -= 1000;
                                mGoalAmountMesh.text = tGoalAmountDown.ToString();
                                tAssetsGoal.mGoalAmount = tGoalAmountDown;
                                return;
                            case TurnLimitsMethod tTurnLimits:
                                int tTurnLimitsDown = int.Parse(mTurnLimitsMesh.text);
                                if (tTurnLimitsDown <= 5) return;
                                tTurnLimitsDown -= 5;
                                mTurnLimitsMesh.text = tTurnLimitsDown.ToString();
                                tTurnLimits.mLimitsTurn = tTurnLimitsDown;
                                return;
                            case LapGoalMethod tLapGoal:
                                int tGoalLapDown = int.Parse(mGoalLapMesh.text);
                                if (tGoalLapDown <= 1) return;
                                tGoalLapDown -= 1;
                                mGoalLapMesh.text = tGoalLapDown.ToString();
                                tLapGoal.mGoalLap = tGoalLapDown;
                                return;
                        }
                        return;
                    //ボーナス設定欄
                    case "bonusUpArrowPushed":
                        MySoundPlayer.playSe("hit",false);
                        int tBonusAmountUp = int.Parse(mBonusAmountMesh.text);
                        if (tBonusAmountUp >= 5000) return;
                        tBonusAmountUp += 100;
                        mBonusAmountMesh.text = tBonusAmountUp.ToString();
                        ((WithResultBonusMethod)GameData.mGameSetting.mBattleMethod).mResultBonus = tBonusAmountUp;
                        return;
                    case "bonusDownArrowPushed":
                        MySoundPlayer.playSe("hit",false);
                        int tBonusAmountDown = int.Parse(mBonusAmountMesh.text);
                        if (tBonusAmountDown <= 100) return;
                        tBonusAmountDown -= 100;
                        mBonusAmountMesh.text = tBonusAmountDown.ToString();
                        ((WithResultBonusMethod)GameData.mGameSetting.mBattleMethod).mResultBonus = tBonusAmountDown;
                        return;
                    case "maxAssets":
                        aMessage.getParameter<CheckBox>("object").invertCheck();
                        ((WithResultBonusMethod)GameData.mGameSetting.mBattleMethod).mMaxAssets = aMessage.getParameter<CheckBox>("object").mIsCheck;
                        return;
                    case "moveDistance":
                        aMessage.getParameter<CheckBox>("object").invertCheck();
                        ((WithResultBonusMethod)GameData.mGameSetting.mBattleMethod).mMoveDistance = aMessage.getParameter<CheckBox>("object").mIsCheck;
                        return;
                    case "landNumber":
                        aMessage.getParameter<CheckBox>("object").invertCheck();
                        ((WithResultBonusMethod)GameData.mGameSetting.mBattleMethod).mLandNumber = aMessage.getParameter<CheckBox>("object").mIsCheck;
                        return;
                    case "feeAmount":
                        aMessage.getParameter<CheckBox>("object").invertCheck();
                        ((WithResultBonusMethod)GameData.mGameSetting.mBattleMethod).mFeeAmount = aMessage.getParameter<CheckBox>("object").mIsCheck;
                        return;
                    case "disasterDamageAmount":
                        aMessage.getParameter<CheckBox>("object").invertCheck();
                        ((WithResultBonusMethod)GameData.mGameSetting.mBattleMethod).mDisasterDamageAmount = aMessage.getParameter<CheckBox>("object").mIsCheck;
                        return;
                    //決定
                    case "okButtonPushed":
                        hadeBoard(() => {
                            Subject.removeObserver("battleMethodSetting");
                            aCallback();
                        });
                        return;
                    default:
                        return;
                }
            }));
        }, () => {
            findObject();
            changeBattleMethod(false);
        });
    }
    public void changeBattleMethod(bool aAnimate) {
        string tCheckContainerName = "";
        switch (GameData.mGameSetting.mBattleMethod) {
            case LosingMethod tLosing:
                tCheckContainerName = "losing";
                break;
            case AssetsGoalMethod tAssetsGoal:
                tCheckContainerName = "assetsGoal";
                break;
            case TurnLimitsMethod tTurnLimits:
                tCheckContainerName = "turnLimits";
                break;
            case LapGoalMethod tLapGoal:
                tCheckContainerName = "lapGoal";
                break;
            case BottomConfirmedMethod tBottomConfirmed:
                tCheckContainerName = "bottomConfirmed";
                break;
        }
        foreach (MyBehaviour tContainer in mBattleMethodCheckContainers) {
            if (tContainer.name == tCheckContainerName) {
                if (aAnimate)
                    tContainer.GetComponentInChildren<CheckBox>().check();
                else
                    tContainer.GetComponentInChildren<CheckBox>().set(true);
            } else {
                if (aAnimate)
                    tContainer.GetComponentInChildren<CheckBox>().uncheck();
                else
                    tContainer.GetComponentInChildren<CheckBox>().set(false);
            }
        }
        setMiniSetting();
        setResultBonusSetting();
    }
    public void setMiniSetting() {
        string tDisplayMiniSettingName = "";
        switch (GameData.mGameSetting.mBattleMethod) {
            case LosingMethod tLosing:
                tDisplayMiniSettingName = "";
                break;
            case AssetsGoalMethod tAssetsGoal:
                tDisplayMiniSettingName = "assetsGoalSetting";
                mGoalAmountMesh.text = tAssetsGoal.mGoalAmount.ToString();
                break;
            case TurnLimitsMethod tTurnLimits:
                tDisplayMiniSettingName = "turnLimitsSetting";
                mTurnLimitsMesh.text = tTurnLimits.mLimitsTurn.ToString();
                break;
            case LapGoalMethod tLapGoal:
                tDisplayMiniSettingName = "lapGoalSetting";
                mGoalLapMesh.text = tLapGoal.mGoalLap.ToString();
                break;
            case BottomConfirmedMethod tBottomConfirmed:
                tDisplayMiniSettingName = "";
                break;
        }
        foreach (MyBehaviour tContainer in mMethodMiniSettingContainers) {
            if (tContainer.name == tDisplayMiniSettingName)
                tContainer.gameObject.SetActive(true);
            else
                tContainer.gameObject.SetActive(false);
        }
    }
    public void setResultBonusSetting() {
        if (!(GameData.mGameSetting.mBattleMethod is WithResultBonusMethod)) {
            mResultBonusSettingContainer.gameObject.SetActive(false);
            return;
        }
        mResultBonusSettingContainer.gameObject.SetActive(true);
        WithResultBonusMethod tBonusMethod = (WithResultBonusMethod)GameData.mGameSetting.mBattleMethod;
        mBonusAmountMesh.text = tBonusMethod.mResultBonus.ToString();
        foreach (MyBehaviour tContainer in mBonusCheckContainers) {
            switch (tContainer.name) {
                case "maxAssets":
                    tContainer.GetComponentInChildren<CheckBox>().set(tBonusMethod.mMaxAssets);
                    break;
                case "moveDistance":
                    tContainer.GetComponentInChildren<CheckBox>().set(tBonusMethod.mMoveDistance);
                    break;
                case "landNumber":
                    tContainer.GetComponentInChildren<CheckBox>().set(tBonusMethod.mLandNumber);
                    break;
                case "feeAmount":
                    tContainer.GetComponentInChildren<CheckBox>().set(tBonusMethod.mFeeAmount);
                    break;
                case "disasterDamageAmount":
                    tContainer.GetComponentInChildren<CheckBox>().set(tBonusMethod.mDisasterDamageAmount);
                    break;
            }
        }
    }
    public void findObject() {
        //対戦方式欄
        mBattleMethodCheckContainers = new List<MyBehaviour>();
        foreach (string tContainerName in new List<string>() { "losing", "assetsGoal", "turnLimits", "lapGoal", "bottomConfirmed" }) {
            mBattleMethodCheckContainers.Add(mBoard.findChild<MyBehaviour>(tContainerName));
        }
        //対戦方式別設定欄
        mMethodMiniSettingContainers = new List<MyBehaviour>();
        foreach (string tContainerName in new List<string>() { "assetsGoalSetting", "turnLimitsSetting", "lapGoalSetting" }) {
            mMethodMiniSettingContainers.Add(mBoard.findChild<MyBehaviour>(tContainerName));
        }
        mGoalAmountMesh = mBoard.findChild<TextMesh>("goalAmountMesh");
        mTurnLimitsMesh = mBoard.findChild<TextMesh>("turnLimitsMesh");
        mGoalLapMesh = mBoard.findChild<TextMesh>("lapGoalMesh");
        //ボーナス設定欄
        mResultBonusSettingContainer = mBoard.findChild<MyBehaviour>("resultBonusSetting");
        mBonusAmountMesh = mBoard.findChild<TextMesh>("bonusAmountMesh");
        mBonusCheckContainers = new List<MyBehaviour>();
        foreach (string tContainerName in new List<string>() { "maxAssets", "moveDistance", "landNumber", "feeAmount", "disasterDamageAmount" }) {
            mBonusCheckContainers.Add(mBoard.findChild<MyBehaviour>(tContainerName));
        }
    }
}
