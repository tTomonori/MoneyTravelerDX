﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAi : TravelerAi {
    public void moveCamera(Vector2 aDragVector) {
        Vector2 tVec = aDragVector / -15f;
        GameData.mStageData.mCamera.move(tVec);
    }
    //マップを見る
    private void viewMap(GameMaster aMaster, Action aCallback) {
        MonoBehaviour tTarget = GameData.mStageData.mCamera.mTarget;
        GameData.mStageData.mCamera.mTarget = null;
        HangingBoard tBoard = aMaster.mUiMain.displayHangingBoard(HangingBoard.BoardImage.arrow);
        tBoard.open();
        Subject.addObserver(new Observer("playerAiViewMap", (aMessage) => {
            switch (aMessage.name) {
                case "hangingBoardPushed":
                    tBoard.close();
                    Subject.removeObserver("playerAiViewMap");
                    GameData.mStageData.mCamera.mTarget = tTarget;
                    aCallback();
                    return;
                case "gamePadDragged":
                    moveCamera(aMessage.getParameter<Vector2>("vector"));
                    return;
                case "gamePadClicked":
                    Subject.removeObserver("playerAiViewMap");
                    tBoard.close();
                    MassStatusDisplay tDisplay = null;
                    List<MassStatusUiButtonData> tButtonData = new List<MassStatusUiButtonData>() {
                        null,null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tTarget;
                            viewMap(aMaster,aCallback);
                        })
                    };
                    tDisplay = aMaster.mUiMain.displayMassStatus(aMessage.getParameter<GameMass>("mass"), tButtonData);
                    return;
            }
        }));
    }
    //ダイス
    public override void rollDice(DiceManager aDiceManager, GameMaster aMaster) {
        HangingBoard tBoard = aMaster.mUiMain.displayHangingBoard(HangingBoard.BoardImage.map);
        aDiceManager.show();
        tBoard.open();
        Subject.addObserver(new Observer("playerAiRollDice", (aMessage) => {
            switch (aMessage.name) {
                case "hangingBoardPushed":
                    tBoard.close();
                    aDiceManager.hide();
                    Subject.removeObserver("playerAiRollDice");
                    viewMap(aMaster, () => { rollDice(aDiceManager, aMaster); });
                    return;
                case "diceBackPushed":
                    aDiceManager.open();
                    break;
                case "dice1Pushed":
                    aDiceManager.open1();
                    break;
                case "dice2Pushed":
                    aDiceManager.open2();
                    break;
                case "dice3Pushed":
                    aDiceManager.open3();
                    break;
            }
            if (aDiceManager.mIsAllOpened)
                tBoard.close();
        }));
    }
    public override void endRollDice() {
        Subject.removeObserver("playerAiRollDice");
    }
    //土地の購入
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        MassStatusDisplay tDisplay = null;
        List<MassStatusUiButtonData> tButtonDataList = new List<MassStatusUiButtonData>() {
            {new MassStatusUiButtonData("購入する",aMyStatus.playerColor,()=>{tDisplay.close(); aCallback(true); }) },
            null,null,
            {new MassStatusUiButtonData("マップを\n見る",new Color(0.8f,0.8f,0.8f),()=>{
                tDisplay.close();
                viewMap(aMaster,()=>{ purchaseLand(aMyStatus,aLand,aMaster,aCallback); });
            }) },
            {new MassStatusUiButtonData("やめる",new Color(0.8f,0.8f,0.8f),()=>{tDisplay.close(); aCallback(false); }) }
        };
        tDisplay = aMaster.mUiMain.displayMassStatus(aLand, tButtonDataList);
    }
    //土地の増資
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        MassStatusDisplay tDisplay = null;
        List<MassStatusUiButtonData> tButtonDataList = new List<MassStatusUiButtonData>() {
            {new MassStatusUiButtonData("増資する",aMyStatus.playerColor,()=>{tDisplay.close(); aCallback(true); }) },
            null,null,
            {new MassStatusUiButtonData("マップを\n見る",new Color(0.8f,0.8f,0.8f),()=>{
                tDisplay.close();
                viewMap(aMaster,()=>{ purchaseLand(aMyStatus,aLand,aMaster,aCallback); });
            }) },
            {new MassStatusUiButtonData("やめる",new Color(0.8f,0.8f,0.8f),()=>{tDisplay.close(); aCallback(false); }) }
        };
        tDisplay = aMaster.mUiMain.displayMassStatus(aLand, tButtonDataList);
    }
    //土地の買収
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        MassStatusDisplay tDisplay = null;
        List<MassStatusUiButtonData> tButtonDataList = new List<MassStatusUiButtonData>() {
            {new MassStatusUiButtonData("買収する",aMyStatus.playerColor,()=>{tDisplay.close(); aCallback(true); }) },
            null,null,
            {new MassStatusUiButtonData("マップを\n見る",new Color(0.8f,0.8f,0.8f),()=>{
                tDisplay.close();
                viewMap(aMaster,()=>{ purchaseLand(aMyStatus,aLand,aMaster,aCallback); });
            }) },
            {new MassStatusUiButtonData("やめる",new Color(0.8f,0.8f,0.8f),()=>{tDisplay.close(); aCallback(false); }) }
        };
        tDisplay = aMaster.mUiMain.displayMassStatus(aLand, tButtonDataList);
    }
    //土地の売却
    public override void sellLand(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        MonoBehaviour tTarget = GameData.mStageData.mCamera.mTarget;
        GameData.mStageData.mCamera.mTarget = null;
        Subject.addObserver(new Observer("playerAiSellLand", (aMessage) => {
            switch (aMessage.name) {
                case "gamePadDragged":
                    moveCamera(aMessage.getParameter<Vector2>("vector"));
                    return;
                case "gamePadClicked":
                    Subject.removeObserver("playerAiSellLand");
                    MassStatusDisplay tDisplay = null;
                    List<MassStatusUiButtonData> tButtonData = new List<MassStatusUiButtonData>() {
                        null,null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tTarget;
                            sellLand(aMyStatus,aMaster,aCallback);
                        })
                    };
                    GameMass tMass = aMessage.getParameter<GameMass>("mass");
                    if (tMass is LandMass && ((LandMass)tMass).mOwner == aMyStatus) {
                        //自分の土地の場合は売却ボタン追加
                        tButtonData[0] = new MassStatusUiButtonData("売却する", aMyStatus.playerColor, () => {
                            tDisplay.close();
                            GameData.mStageData.mCamera.mTarget = tTarget;
                            aCallback((LandMass)tMass);
                        });
                    }
                    tDisplay = aMaster.mUiMain.displayMassStatus(tMass, tButtonData);
                    return;
            }
        }));
    }
}