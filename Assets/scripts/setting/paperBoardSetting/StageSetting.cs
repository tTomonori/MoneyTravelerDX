using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageSetting : PaperBoardSetting {
    static public int mChoiceNumberInPage = 10;
    [NonSerialized] private int mPage;
    [NonSerialized] private List<(string, string)> mStageData;
    [NonSerialized] private string mOpeningScene;
    [SerializeField]
    public List<TextMesh> mChoiceMeshList;
    public TextMesh mInitialMoneyMesh;
    public TextMesh mLandNumberMesh;
    private void Awake() {
        mPage = 0;
        mOpeningScene = "";
        mInitialMoneyMesh.text = "";
        mLandNumberMesh.text = "";
        mStageData = new List<(string, string)>();
        mStageData.Add(("スタンダード", "standard"));
        mStageData.Add(("登山道", "trail"));
        mStageData.Add(("双子橋", "twinBridge"));
        mStageData.Add(("世界一周", "worldTour"));
        mStageData.Add(("中央大陸", "centralContinent"));
        mStageData.Add(("魔王城", "demonCastle"));
        mStageData.Add(("水の都", "cityOfWater"));
        mStageData.Add(("天界", "heaven"));
        mStageData.Add(("工場", "factory"));

        updatePage();
    }
    public void choiceButtonPushed(int aNumber) {
        int tChoiceNumber = aNumber + mPage * mChoiceNumberInPage - 1;
        if (mStageData.Count <= tChoiceNumber)
            return;
        setStage(mStageData[tChoiceNumber].Item1, mStageData[tChoiceNumber].Item2);
    }
    public void setStage(string aStageName, string aStageSceneName) {
        if (mOpeningScene != "") {
            MySceneManager.closeScene(mOpeningScene);
        }
        mOpeningScene = aStageSceneName;
        GameData.mGameSetting.mStageName = aStageName;
        GameData.mGameSetting.mStageSceneName = aStageSceneName;
        MySceneManager.openScene(aStageSceneName, new Arg(new Dictionary<string, object>() { { "game", false } }), (aScene) => {
            GameMain tMain = null;
            foreach (GameObject tObject in aScene.GetRootGameObjects()) {
                tMain = tObject.GetComponent<GameMain>();
                if (tMain != null) break;
            }
            mInitialMoneyMesh.text = tMain.mInitialMoney.ToString();
            int tLandNumber = 0;
            foreach (GameMass tMass in tMain.mFeild.mMassList)
                if (tMass is LandMass)
                    tLandNumber++;
            mLandNumberMesh.text = tLandNumber.ToString();
            tMain.mCamera.mCamera.GetComponent<Camera>().rect = new Rect(0.246f, 0.13f, 0.707f, 0.7f);
        });
    }
    public void nextPage() {
        mPage = (mPage + 1) % (int)(Mathf.Ceil(mStageData.Count / (float)mChoiceNumberInPage));
        updatePage();
    }
    public void previousePage() {
        mPage = (mPage + (int)(Mathf.Ceil(mStageData.Count / (float)mChoiceNumberInPage)) - 1) % (int)(Mathf.Ceil(mStageData.Count / (float)mChoiceNumberInPage));
        updatePage();
    }
    public void updatePage() {
        for (int i = 0; i < 10; i++) {
            int tChoiceNumber = i + mPage * mChoiceNumberInPage;
            if (mStageData.Count <= tChoiceNumber) {
                mChoiceMeshList[i].text = "";
                continue;
            }
            mChoiceMeshList[i].text = mStageData[tChoiceNumber].Item1;
        }
    }
    public override void displayed() {
        Subject.addObserver(new Observer("stageSetting", (aMessage) => {
            switch (aMessage.name) {
                case "choice1Pushed":
                    choiceButtonPushed(1);
                    return;
                case "choice2Pushed":
                    choiceButtonPushed(2);
                    return;
                case "choice3Pushed":
                    choiceButtonPushed(3);
                    return;
                case "choice4Pushed":
                    choiceButtonPushed(4);
                    return;
                case "choice5Pushed":
                    choiceButtonPushed(5);
                    return;
                case "choice6Pushed":
                    choiceButtonPushed(6);
                    return;
                case "choice7Pushed":
                    choiceButtonPushed(7);
                    return;
                case "choice8Pushed":
                    choiceButtonPushed(8);
                    return;
                case "choice9Pushed":
                    choiceButtonPushed(9);
                    return;
                case "choice10Pushed":
                    choiceButtonPushed(10);
                    return;
                case "nextArrowPushed":
                    MySoundPlayer.playSe("hit", false);
                    nextPage();
                    return;
                case "previouseArrowPushed":
                    MySoundPlayer.playSe("hit", false);
                    previousePage();
                    return;
                case "okButtonPushed":
                    Subject.removeObserver("stageSetting");
                    if (mOpeningScene != "") {
                        MySceneManager.closeScene(mOpeningScene);
                    }
                    PaperBoardSetting.hadeBoard();
                    return;
            }
        }));
    }
}
