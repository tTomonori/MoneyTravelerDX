using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {
    void Start() {
        MySceneManager.fadeCallbacks.fadeInFinished = () => {
            Debug.Log("ゲーム準備完了");
        };
        MySceneManager.fadeCallbacks.nextSceneReady();
    }
}
