using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : MonoBehaviour {
    void Start() {
        GameObject.Find("button").GetComponent<MyButton>().mPushedFunction = () => {
            MySceneManager.changeScene("setting");
        };

        if (MySceneManager.fadeCallbacks != null) {
            MySceneManager.fadeCallbacks.nextSceneReady();
        }
    }
}
