using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultNumber : MyBehaviour {
    [SerializeField] public TextMesh mNumberMesh;
    [NonSerialized] public int mNumber;
    private void Awake() {
        mNumber = 0;
        mNumberMesh.text = "0";
    }
    public int getNumber() {
        return int.Parse(mNumberMesh.text);
    }
    public void plus(int aNumber, float aDuration, Action aCallback) {
        StartCoroutine(plusDelta(aNumber, aDuration, aCallback));
    }
    private IEnumerator plusDelta(int aNumber, float aDuration, Action aCallback) {
        int tPreNumber = mNumber;
        float tElapsedTime = 0;
        while (true) {
            if (tElapsedTime + Time.deltaTime >= aDuration) {//完了
                mNumber = tPreNumber + aNumber;
                mNumberMesh.text = mNumber.ToString();
                if (aCallback != null) aCallback();
                yield break;
            }
            tElapsedTime += Time.deltaTime;
            mNumberMesh.text = ((int)(mNumber + aNumber * tElapsedTime / aDuration)).ToString();
            yield return null;
        }
    }
}
