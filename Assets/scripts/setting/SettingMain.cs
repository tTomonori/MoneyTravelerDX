using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SettingMain : MonoBehaviour {
    [SerializeField] public MyBehaviour mCover;
    void Start() {
        updteDisplay();
        Subject.addObserver(new Observer("settingMain", (aMessage) => {
            switch (aMessage.name) {
                //キャラ
                case "chara1ButtonPushed":
                    TravelerCharaSetting.mSettingPlayerNumber = 1;
                    displayFlowerBoard<TravelerCharaSetting>();
                    break;
                case "chara2ButtonPushed":
                    TravelerCharaSetting.mSettingPlayerNumber = 2;
                    displayFlowerBoard<TravelerCharaSetting>();
                    break;
                case "chara3ButtonPushed":
                    TravelerCharaSetting.mSettingPlayerNumber = 3;
                    displayFlowerBoard<TravelerCharaSetting>();
                    break;
                case "chara4ButtonPushed":
                    TravelerCharaSetting.mSettingPlayerNumber = 4;
                    displayFlowerBoard<TravelerCharaSetting>();
                    break;
                case "ai1ButtonPushed":
                    TravelerAiSetting.mSettingPlayerNumber = 1;
                    displayFlowerBoard<TravelerAiSetting>();
                    break;
                case "ai2ButtonPushed":
                    TravelerAiSetting.mSettingPlayerNumber = 2;
                    displayFlowerBoard<TravelerAiSetting>();
                    break;
                case "ai3ButtonPushed":
                    TravelerAiSetting.mSettingPlayerNumber = 3;
                    displayFlowerBoard<TravelerAiSetting>();
                    break;
                case "ai4ButtonPushed":
                    TravelerAiSetting.mSettingPlayerNumber = 4;
                    displayFlowerBoard<TravelerAiSetting>();
                    break;
                //その他
                case "initialMoneyButtonPushed":
                    displayFlowerBoard<InitialMoneySetting>();
                    break;
                case "feeButtonPushed":
                    displayFlowerBoard<FeeSetting>();
                    break;
                case "bonusButtonPushed":
                    displayFlowerBoard<BonusSetting>();
                    break;
                case "acquisitionButtonPushed":
                    displayFlowerBoard<AcquisitionSetting>();
                    break;
                case "acquisitionConditionButtonPushed":
                    displayFlowerBoard<AcquisitionConditionSetting>();
                    break;
                case "disasterDamageButtonPushed":
                    displayFlowerBoard<DisasterDamageSetting>();
                    break;
                case "battleMethodButtonPushed":
                    displayFlowerBoard<BattleMethodSetting>();
                    break;
                //開始
                case "startButtonPushed":
                    gameStart();
                    break;
            }
        }));
    }
    //設定に応じて表示更新
    public void updteDisplay() {
        //キャラ
        for(int i = 1; i <= 4; i++) {
            MyBehaviour tContainer = GameObject.Find("chara" + i.ToString()).GetComponent<MyBehaviour>();
            tContainer.findChild<SpriteRenderer>("charaImg").sprite = GameData.mGameSetting.mTravelerData[i - 1].mTravelerCharaData.getImage();
            tContainer.findChild<TextMesh>("nameMesh").text = GameData.mGameSetting.mTravelerData[i - 1].mTravelerCharaData.mName;
            tContainer.findChild<TextMesh>("aiMesh").text = GameData.mGameSetting.mTravelerData[i - 1].mAiPattern.getName();
        }
        //初期金
        GameObject.Find("initialMoneyMesh").GetComponent<TextMesh>().text = "x" + GameData.mGameSetting.mInitialMoney.ToString();
        //料金
        GameObject.Find("feeMesh").GetComponent<TextMesh>().text = "x" + GameData.mGameSetting.mFee.ToString();
        //ボーナス
        GameObject.Find("bonusMesh").GetComponent<TextMesh>().text = "x" + GameData.mGameSetting.mBonus.ToString();
        //買収
        GameObject.Find("acquisitionMesh").GetComponent<TextMesh>().text = "x" + GameData.mGameSetting.mAcquisition.ToString();
        //買収条件
        GameObject.Find("acquisitionConditionMesh").GetComponent<TextMesh>().text = GameData.mGameSetting.mAcquisitionCondition.getName();
        //災害被害
        GameObject.Find("disasterDamageMesh").GetComponent<TextMesh>().text = "x" + GameData.mGameSetting.mDisasterDamage.ToString();
        //ステージ
        //対戦方式
        GameObject.Find("battleMethodMesh").GetComponent<TextMesh>().text = GameData.mGameSetting.mBattleMethod.mName;
    }
    //設定用のボードを表示
    public void displayFlowerBoard<T>() where T : FlowerBoadSetting {
        mCover.gameObject.SetActive(true);
        T tBoard = Activator.CreateInstance<T>();
        tBoard.set(() => {
            this.updteDisplay();
            mCover.gameObject.SetActive(false);
        });
    }
    //開始
    public void gameStart() {
        //開始可能な設定になっているか確認
        //トラベラーが2人以上か
        int tTravelerNum = 0;
        foreach(TravelerData tData in GameData.mGameSetting.mTravelerData) {
            if (tData.mTravelerCharaData != TravelerCharaData.none) tTravelerNum++;
        }
        if (tTravelerNum < 2) {
            Attention.attention("トラベラーを2人以上\n設定してください", Attention.OverlapType.truncate);
            return;
        }
        //同じキャラを複数選んでいないか
        for(int i = 0; i < 4; i++) {
            if (GameData.mGameSetting.mTravelerData[i].mTravelerCharaData == TravelerCharaData.none) continue;
            for(int j = i + 1; j < 4; j++) {
                if(GameData.mGameSetting.mTravelerData[i].mTravelerCharaData== GameData.mGameSetting.mTravelerData[j].mTravelerCharaData) {
                    Attention.attention("同じキャラを\n複数選択できません", Attention.OverlapType.truncate);
                    return;
                }
            }
        }
        mCover.gameObject.SetActive(true);
        MySceneManager.changeSceneWithFade("standard", "curtainFade");
    }
    private void OnDestroy() {
        Subject.removeObserver("settingMain");
    }
}
