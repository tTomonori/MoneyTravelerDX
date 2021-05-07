using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : MonoBehaviour {
    [SerializeField] public MyButton mButton;
    void Start() {
        if (MySceneManager.fadeCallbacks != null) {
            MySceneManager.fadeCallbacks.fadeInFinished = setButton;
            MySceneManager.fadeCallbacks.nextSceneReady();
        } else {
            setButton();
        }
    }
    public void setButton() {
        mButton.mPushedFunction = () => {
            mButton.gameObject.SetActive(false);
            MySceneManager.changeScene("setting");
        };
    }
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad() {
        Screen.SetResolution(2250, 1500, false, 60);
    }
}
