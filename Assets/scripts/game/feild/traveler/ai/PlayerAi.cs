using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAi : TravelerAi {
    //ダイス
    public override void rollDice(DiceManager aDiceManager) {
        aDiceManager.displayMapButton();
        setDiceObserver(aDiceManager);
    }
    private void setDiceObserver(DiceManager aDiceManager) {
        aDiceManager.show();
        Subject.addObserver(new Observer("playerAiRollDice", (aMessage) => {
            switch (aMessage.name) {
                case "mapButtonPushed":
                    Subject.removeObserver("playerAiRollDice");
                    setMapInDiceObserver(aDiceManager);
                    return;
                case "diceBackPushed":
                    aDiceManager.open();
                    return;
                case "dice1Pushed":
                    aDiceManager.open1();
                    return;
                case "dice2Pushed":
                    aDiceManager.open2();
                    return;
                case "dice3Pushed":
                    aDiceManager.open3();
                    return;
            }
        }));
    }
    private void setMapInDiceObserver(DiceManager aDiceManager) {
        MonoBehaviour tCameraTarget = GameData.mStageData.mCamera.mTarget;
        GameData.mStageData.mCamera.mTarget = null;
        aDiceManager.hide();
        Subject.addObserver(new Observer("playerAiRollDice", (aMessage) => {
            switch (aMessage.name) {
                case "mapButtonPushed":
                    Subject.removeObserver("playerAiRollDice");
                    GameData.mStageData.mCamera.mTarget = tCameraTarget;
                    setDiceObserver(aDiceManager);
                    return;
                case "gamePadDragged":
                    Vector2 tVec = aMessage.getParameter<Vector2>("vector") / -15;
                    GameData.mStageData.mCamera.move(tVec);
                    return;
                case "gamePadClicked":
                    return;
            }
        }));
    }
    public override void endRollDice() {
        Subject.removeObserver("playerAiRollDice");
    }
    //土地の購入
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        MassStatusDisplay tDisplay = null;
        List<MassStatusUiButtonData> tButtonDataList = new List<MassStatusUiButtonData>() {
            {new MassStatusUiButtonData("購入する",aMyStatus.playerColor,()=>{tDisplay.close(); aCallback(true); }) },
            null,null,
            {new MassStatusUiButtonData("マップを\n見る",new Color(0.8f,0.8f,0.8f),()=>{ }) },
            {new MassStatusUiButtonData("やめる",new Color(0.8f,0.8f,0.8f),()=>{tDisplay.close(); aCallback(false); }) }
        };
        tDisplay = mMaster.mUiMain.displayMassStatus(aLand, tButtonDataList);
    }
    //土地の増資
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        MassStatusDisplay tDisplay = null;
        List<MassStatusUiButtonData> tButtonDataList = new List<MassStatusUiButtonData>() {
            {new MassStatusUiButtonData("増資する",aMyStatus.playerColor,()=>{tDisplay.close(); aCallback(true); }) },
            null,null,
            {new MassStatusUiButtonData("マップを\n見る",new Color(0.8f,0.8f,0.8f),()=>{ }) },
            {new MassStatusUiButtonData("やめる",new Color(0.8f,0.8f,0.8f),()=>{tDisplay.close(); aCallback(false); }) }
        };
        tDisplay = mMaster.mUiMain.displayMassStatus(aLand, tButtonDataList);
    }
    //土地の買収
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster mMaster, Action<bool> aCallback) {
        MassStatusDisplay tDisplay = null;
        List<MassStatusUiButtonData> tButtonDataList = new List<MassStatusUiButtonData>() {
            {new MassStatusUiButtonData("買収する",aMyStatus.playerColor,()=>{tDisplay.close(); aCallback(true); }) },
            null,null,
            {new MassStatusUiButtonData("マップを\n見る",new Color(0.8f,0.8f,0.8f),()=>{ }) },
            {new MassStatusUiButtonData("やめる",new Color(0.8f,0.8f,0.8f),()=>{tDisplay.close(); aCallback(false); }) }
        };
        tDisplay = mMaster.mUiMain.displayMassStatus(aLand, tButtonDataList);
    }
}