using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class GameCameraController {
    static public Action<GameMass> mOnMassClicked;
    static public MonoBehaviour mPreCameraTarget;
    static public void activate(Action<GameMass> aOnMassClicked) {
        mOnMassClicked = aOnMassClicked;
        mPreCameraTarget = GameData.mStageData.mCamera.mTarget;
        GameData.mStageData.mCamera.mTarget = null;
        Subject.addObserver(new Observer("gameCameraController", (aMessage) => {
            switch (aMessage.name) {
                case "gamePadDragged":
                    GameData.mStageData.mCamera.move(aMessage.getParameter<Vector2>("vector") / -15f);
                    return;
                case "gamePadScrolled":
                    GameData.mStageData.mCamera.zoom(aMessage.getParameter<float>("scroll") * -1);
                    return;
                case "gamePadClicked":
                    if (mOnMassClicked != null)
                        mOnMassClicked(aMessage.getParameter<GameMass>("mass"));
                    return;
            }
        }));
    }
    static public void invalidate() {
        Subject.removeObserver("gameCameraController");
        GameData.mStageData.mCamera.mTarget = mPreCameraTarget;
    }
}
