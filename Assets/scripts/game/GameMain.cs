using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {
    private GameMaster mMaster;
    [SerializeField]
    public int mInitialMoney;
    public GameFeild mFeild;
    public GameCamera mCamera;
    public AudioClip mBGM;
    void Start() {
        GameData.mStageData = new StageData();
        GameData.mStageData.mInitialMoney = mInitialMoney;
        GameData.mStageData.mNorth = mFeild.mNorth;
        GameData.mStageData.mEast = mFeild.mEast;
        GameData.mStageData.mSouth = mFeild.mSouth;
        GameData.mStageData.mWest = mFeild.mWest;
        GameData.mStageData.mFloor = mFeild.mFloor;
        GameData.mStageData.mCeiling = mFeild.mCeiling;
        GameData.mStageData.mCamera = mCamera;

        Arg tArg = MySceneManager.getArg(this.gameObject.scene.name);
        if (tArg.ContainsKey("game") && !tArg.get<bool>("game")) {
            setPreview();
            return;
        }
        prepare();
    }
    private void prepare() {
        mMaster = new GameMaster(mFeild, mCamera);
        mMaster.prepare(() => {
            //準備完了
            if (MySceneManager.fadeCallbacks == null) {
                gameStart();
                return;
            }
            MySceneManager.fadeCallbacks.fadeInFinished = () => {
                gameStart();
            };
            MySceneManager.fadeCallbacks.nextSceneReady();
        });
    }
    private void gameStart() {
        MySoundPlayer.playBgm(mBGM.name, MySoundPlayer.LoopType.normalConnect, 0.6f);
        mMaster.gameStart(gameEnd);
    }
    private void gameEnd() {
        MySoundPlayer.fadeBgm(1, 0, () => {
            MySoundPlayer.stopBgm();
        });
        MySceneManager.changeSceneWithFade("result", "curtainFade", new Arg(new Dictionary<string, object>() { { "travelers", mMaster.mTravelers } }));
    }
    private void setPreview() {
        Subject.addObserver(new Observer("gameMain", (aMessage) => {
            switch (aMessage.name) {
                case "gamePadDragged":
                    moveCamera(aMessage.getParameter<Vector2>("vector"));
                    return;
            }
        }));
    }
    public void moveCamera(Vector2 aDragVector) {
        Vector2 tVec = aDragVector / -15f;
        mCamera.move(tVec);
    }
    private void OnDestroy() {
        Subject.removeObserver("gameMain");
    }
}
