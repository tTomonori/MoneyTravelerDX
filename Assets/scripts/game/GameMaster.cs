using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameMaster {
    public GameFeild mFeild;
    public GameCamera mCamera;
    public GameUiMain mUiMain;
    public List<TravelerStatus> mTravelers;
    public List<TravelerStatus> mTurnOrder;
    public GameMaster(GameFeild aFeild, GameCamera aCamera) {
        mFeild = aFeild;
        mCamera = aCamera;
    }
    public void prepare(Action aCallback) {
        foreach (GameMass tMass in mFeild.mMassList)
            tMass.initialize();
        mTravelers = TravelerFactory.create(mFeild);
        aCallback();
    }
    public void gameStart() {
        //uiのシーンを開く
        MySceneManager.openScene("gameUi", null, (aScene) => {
            mUiMain = GameObject.Find("gameUiMain").GetComponent<GameUiMain>();
            //ターンの順番を決める
            mUiMain.decideTurnOrder(mTravelers, (aTurnOrder) => {
                mTurnOrder = aTurnOrder;
                distributeInitialMoney();
                //ステータス表示
                mUiMain.displayStatus(mTurnOrder, () => {

                });
            });
        });
    }
    //初期所持金を配布
    private void distributeInitialMoney() {
        foreach (TravelerStatus tTraveler in mTurnOrder) {
            tTraveler.mMoney = (int)(GameData.mStageData.mInitialMoney * GameData.mGameSetting.mInitialMoney);
            tTraveler.mAssets = tTraveler.mMoney;
        }
    }
}
