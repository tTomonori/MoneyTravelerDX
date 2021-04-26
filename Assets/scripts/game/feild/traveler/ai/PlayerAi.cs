using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAi : TravelerAi {
    public void moveCamera(Vector2 aDragVector) {
        Vector2 tVec = aDragVector / -15f;
        GameData.mStageData.mCamera.move(tVec);
    }
    //マップを見る
    private void viewMap(GameMaster aMaster, HangingBoard.BoardImage aHangingBoardImage, Action aHangingBoardPushed, Action<GameMass> aMassClicked) {
        HangingBoard tBoard = null;
        if (aHangingBoardPushed != null) {
            //hangingBoardのcallbackがnull出なければhangingBoardを表示
            tBoard = aMaster.mUiMain.displayHangingBoard(aHangingBoardImage, () => {
                tBoard.close();
                GameCameraController.invalidate();
                aHangingBoardPushed();
            });
        }
        tBoard.open();
        GameCameraController.activate((aMass) => {
            if (tBoard != null)
                tBoard.close();
            GameCameraController.invalidate();
            if (aMassClicked != null) {
                aMassClicked(aMass);
                return;
            }
            //massClickのcallbackがnullならボタンが戻るだけのマスのステータスを表示
            MonoBehaviour tPreCameraTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = aMass;
            MassStatusDisplay tDisplay = null;
            List<MassStatusUiButtonData> tButtonData = new List<MassStatusUiButtonData>() {
                        null,null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tPreCameraTarget;
                            viewMap(aMaster,aHangingBoardImage,aHangingBoardPushed,aMassClicked);
                        })
                    };
            tDisplay = aMaster.mUiMain.displayMassStatus(aMass, tButtonData);
        });
    }
    //ダイス
    public override void rollDice(DiceManager aDiceManager, GameMaster aMaster) {
        HangingBoard tBoard = null;
        tBoard = aMaster.mUiMain.displayHangingBoard(HangingBoard.BoardImage.map, () => {
            tBoard.close();
            aDiceManager.hide();
            Subject.removeObserver("playerAiRollDice");
            viewMap(aMaster, HangingBoard.BoardImage.arrow, () => { rollDice(aDiceManager, aMaster); }, null);
        });
        aDiceManager.show();
        tBoard.open();
        Subject.addObserver(new Observer("playerAiRollDice", (aMessage) => {
            switch (aMessage.name) {
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
                viewMap(aMaster, HangingBoard.BoardImage.arrow,()=>{ purchaseLand(aMyStatus,aLand,aMaster,aCallback); },null);
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
                viewMap(aMaster,HangingBoard.BoardImage.arrow,()=>{ purchaseLand(aMyStatus,aLand,aMaster,aCallback); },null);
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
                viewMap(aMaster,HangingBoard.BoardImage.arrow,()=>{ purchaseLand(aMyStatus,aLand,aMaster,aCallback); },null);
            }) },
            {new MassStatusUiButtonData("やめる",new Color(0.8f,0.8f,0.8f),()=>{tDisplay.close(); aCallback(false); }) }
        };
        tDisplay = aMaster.mUiMain.displayMassStatus(aLand, tButtonDataList);
    }
    //土地の売却
    public override void sellLand(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        MonoBehaviour tCameraTarget;
        viewMap(aMaster, HangingBoard.BoardImage.paper, () => {
            tCameraTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = null;
            LandOwnedDisplay tOwnedDisplay = null;
            List<MassStatusUiButtonData> tUiButtonData = new List<MassStatusUiButtonData>() {
                        null,null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tOwnedDisplay.close();
                            GameData.mStageData.mCamera.mTarget = tCameraTarget;
                            sellLand(aMyStatus,aMaster,aCallback);
                        })
                    };
            Action<LandMass> tF = (aLand) => {
                tOwnedDisplay.close();
                GameData.mStageData.mCamera.mTarget = aLand;
                LandMassStatusDisplay tLandDisplay = null;
                List<MassStatusUiButtonData> tLandUiButtonData = new List<MassStatusUiButtonData>() {
                        new MassStatusUiButtonData("売却する", aMyStatus.playerColor, () => {
                            tLandDisplay.close();
                            GameData.mStageData.mCamera.mTarget = tCameraTarget;
                            aCallback(aLand);
                        }),null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tLandDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            sellLand(aMyStatus,aMaster,aCallback);
                        })};
                tLandDisplay = (LandMassStatusDisplay)aMaster.mUiMain.displayMassStatus(aLand, tLandUiButtonData);
            };
            tOwnedDisplay = aMaster.mUiMain.displayLandOwnedDisplay(aMyStatus, aMaster.mFeild.getOwnedLand(aMyStatus), tF, tUiButtonData);
        }, (aMass) => {
            tCameraTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = aMass;
            MassStatusDisplay tDisplay = null;
            List<MassStatusUiButtonData> tButtonData = new List<MassStatusUiButtonData>() {
                        null,null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            sellLand(aMyStatus,aMaster,aCallback);
                        })
                    };
            if (aMass is LandMass && ((LandMass)aMass).mOwner == aMyStatus) {
                //自分の土地の場合は売却ボタン追加
                tButtonData[0] = new MassStatusUiButtonData("売却する", aMyStatus.playerColor, () => {
                    tDisplay.close();
                    GameData.mStageData.mCamera.mTarget = tCameraTarget;
                    aCallback((LandMass)aMass);
                });
            }
            tDisplay = aMaster.mUiMain.displayMassStatus(aMass, tButtonData);
        });
    }
    //選択式購入
    public override void selectPurchase(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        MonoBehaviour tCameraTarget;
        viewMap(aMaster, HangingBoard.BoardImage.paper, () => {
            tCameraTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = null;
            LandOwnedDisplay tOwnedDisplay = null;
            List<MassStatusUiButtonData> tUiButtonData = new List<MassStatusUiButtonData>() {
                        new MassStatusUiButtonData("購入\nしない",new Color(0.8f, 0.8f, 0.8f), () => {
                            tOwnedDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            aCallback(null);
                        }),null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tOwnedDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            selectPurchase(aMyStatus,aMaster,aCallback);
                        })
                    };
            Action<LandMass> tF = (aLand) => {
                tOwnedDisplay.close();
                GameData.mStageData.mCamera.mTarget = aLand;
                LandMassStatusDisplay tLandDisplay = null;
                List<MassStatusUiButtonData> tLandUiButtonData = new List<MassStatusUiButtonData>() {
                            null,null,null,null,
                            new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                                tLandDisplay.close();
                                GameData.mStageData.mCamera.mTarget=tCameraTarget;
                                selectPurchase(aMyStatus,aMaster,aCallback);
                        })};
                tLandDisplay = (LandMassStatusDisplay)aMaster.mUiMain.displayMassStatus(aLand, tLandUiButtonData);
            };
            tOwnedDisplay = aMaster.mUiMain.displayLandOwnedDisplay(aMyStatus, aMaster.mFeild.getOwnedLand(aMyStatus), tF, tUiButtonData);
        }, (aMass) => {
            tCameraTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = aMass;
            MassStatusDisplay tDisplay = null;
            List<MassStatusUiButtonData> tButtonData = new List<MassStatusUiButtonData>() {
                        null,null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            selectPurchase(aMyStatus,aMaster,aCallback);
                        })
                    };
            if (aMass is LandMass && aMyStatus.canPurchase((LandMass)aMass)) {
                //購入可能な場合は購入ボタン追加
                tButtonData[0] = new MassStatusUiButtonData("購入する", aMyStatus.playerColor, () => {
                    tDisplay.close();
                    GameData.mStageData.mCamera.mTarget = tCameraTarget;
                    aCallback((LandMass)aMass);
                });
            }
            tDisplay = aMaster.mUiMain.displayMassStatus(aMass, tButtonData);
        });
    }
    //選択式増資
    public override void selectIncrease(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        MonoBehaviour tCameraTarget;
        viewMap(aMaster, HangingBoard.BoardImage.paper, () => {
            tCameraTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = null;
            LandOwnedDisplay tOwnedDisplay = null;
            List<MassStatusUiButtonData> tUiButtonData = new List<MassStatusUiButtonData>() {
                        new MassStatusUiButtonData("増資\nしない",new Color(0.8f, 0.8f, 0.8f), () => {
                            tOwnedDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            aCallback(null);
                        }),null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tOwnedDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            selectIncrease(aMyStatus,aMaster,aCallback);
                        })
                    };
            Action<LandMass> tF = (aLand) => {
                tOwnedDisplay.close();
                GameData.mStageData.mCamera.mTarget = aLand;
                LandMassStatusDisplay tLandDisplay = null;
                List<MassStatusUiButtonData> tLandUiButtonData = new List<MassStatusUiButtonData>() {
                            null,null,null,null,
                            new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                                tLandDisplay.close();
                                GameData.mStageData.mCamera.mTarget=tCameraTarget;
                                selectIncrease(aMyStatus,aMaster,aCallback);
                        })};
                //増資可能な土地なら増資ボタン追加
                if (aMyStatus.canIncrease(aLand)) {
                    tLandUiButtonData[0] = new MassStatusUiButtonData("増資する", aMyStatus.playerColor, () => {
                        tLandDisplay.close();
                        GameData.mStageData.mCamera.mTarget = tCameraTarget;
                        aCallback(aLand);
                    });
                }
                tLandDisplay = (LandMassStatusDisplay)aMaster.mUiMain.displayMassStatus(aLand, tLandUiButtonData);
            };
            tOwnedDisplay = aMaster.mUiMain.displayLandOwnedDisplay(aMyStatus, aMaster.mFeild.getOwnedLand(aMyStatus), tF, tUiButtonData);
        }, (aMass) => {
            tCameraTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = aMass;
            MassStatusDisplay tDisplay = null;
            List<MassStatusUiButtonData> tButtonData = new List<MassStatusUiButtonData>() {
                        null,null,null,null,
                        new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                            tDisplay.close();
                            GameData.mStageData.mCamera.mTarget=tCameraTarget;
                            selectIncrease(aMyStatus,aMaster,aCallback);
                        })
                    };
            if (aMass is LandMass && aMyStatus.canIncrease((LandMass)aMass)) {
                //増資可能な場合は増資ボタン追加
                tButtonData[0] = new MassStatusUiButtonData("増資する", aMyStatus.playerColor, () => {
                    tDisplay.close();
                    GameData.mStageData.mCamera.mTarget = tCameraTarget;
                    aCallback((LandMass)aMass);
                });
            }
            tDisplay = aMaster.mUiMain.displayMassStatus(aMass, tButtonData);
        });
    }
    public override void moveToStart(TravelerStatus aMyStatus, GameMaster aMaster, Action<bool> aCallback) {
        LandOwnedDisplay tDisplay = null;
        List<MassStatusUiButtonData> tUiButtonData = new List<MassStatusUiButtonData>() {
            new MassStatusUiButtonData("進む",new Color(0.8f, 0.8f, 0.8f), () => {
                tDisplay.close();
                aCallback(true);
            }),
            new MassStatusUiButtonData("進まない",new Color(0.8f, 0.8f, 0.8f), () => {
                tDisplay.close();
                aCallback(false);
            }),null,null,
            new MassStatusUiButtonData("マップを\nみる",new Color(0.8f, 0.8f, 0.8f), () => {
                tDisplay.close();
                viewMap(aMaster,HangingBoard.BoardImage.arrow,()=>{ moveToStart(aMyStatus,aMaster,aCallback); },null);
            }),
        };
        tDisplay = aMaster.mUiMain.displayLandOwnedDisplay(aMyStatus, aMaster.mFeild.getOwnedLand(aMyStatus), (aLand) => {
            tDisplay.close();
            MonoBehaviour tTarget = GameData.mStageData.mCamera.mTarget;
            GameData.mStageData.mCamera.mTarget = aLand;
            LandMassStatusDisplay tLandDisplay = null;
            List<MassStatusUiButtonData> tLandUiButtonData = new List<MassStatusUiButtonData>() {
                null,null,null,null,
                new MassStatusUiButtonData("もどる",new Color(0.8f, 0.8f, 0.8f), () => {
                    tLandDisplay.close();
                    GameData.mStageData.mCamera.mTarget=tTarget;
                    moveToStart(aMyStatus,aMaster,aCallback);
             })};
            tLandDisplay = (LandMassStatusDisplay)aMaster.mUiMain.displayMassStatus(aLand, tLandUiButtonData);
        }, tUiButtonData);
    }
}