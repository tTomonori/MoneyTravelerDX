using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainFadeMain : MonoBehaviour {
    [SerializeField]
    public MyBehaviour mCurtainUp;
    public MyBehaviour mCurtainLeft;
    public MyBehaviour mCurtainRight;
    private void Start() {
        fadeOut();
    }
    public void fadeOut() {
        MySoundPlayer.playSe("curtain");
        mCurtainUp.moveTo(new Vector2(0, 4), 0.3f);
        mCurtainLeft.moveTo(new Vector2(0, -5), 0.5f);
        mCurtainRight.moveTo(new Vector2(0, -5), 0.5f);
        MyBehaviour.setTimeoutToIns(1, () => {
            MySceneManager.fadeCallbacks.nextSceneReady = fadeIn;
            MySceneManager.fadeCallbacks.fadeOutFinished();
        });
    }
    public void fadeIn() {
        MySoundPlayer.playSe("curtain");
        MyBehaviour.setTimeoutToIns(0.2f, () => {
            mCurtainUp.moveTo(new Vector2(0, 6), 0.3f);
        });
        mCurtainLeft.moveTo(new Vector2(-8, -5), 0.5f);
        mCurtainRight.moveTo(new Vector2(8, -5), 0.5f);
        MyBehaviour.setTimeoutToIns(0.6f, () => {
            MySceneManager.fadeCallbacks.fadeInFinished();
        });
    }
}
