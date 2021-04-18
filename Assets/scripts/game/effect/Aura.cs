using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Aura : MyBehaviour {
    public Color mColor;
    [SerializeField] public MeshRenderer mMesh;
    public void animate(Action aCallback) {
        mMesh.material.SetColor("_TintColor", mColor);
        this.scale = new Vector3(0, 0, 0.4f);
        StartCoroutine(delta(new Vector3(150, 150, 0), 1, -1, 0.6f, aCallback));
    }
    private IEnumerator delta(Vector3 aScale, float aInitialOpacty, float aAlpha, float aDuration, Action aCallback) {
        float tElapsedTime = 0;
        float tCurrentAlpha = aInitialOpacty;
        while (true) {
            if (tElapsedTime + Time.deltaTime >= aDuration) {//完了
                scale += aScale * (aDuration - tElapsedTime) / aDuration;
                mMesh.material.SetColor("_TintColor", new Color(mColor.r, mColor.g, mColor.b, aInitialOpacty + aAlpha));
                if (aCallback != null) aCallback();
                yield break;
            }
            tElapsedTime += Time.deltaTime;
            tCurrentAlpha += aAlpha * Time.deltaTime / aDuration;
            mMesh.material.SetColor("_TintColor", new Color(mColor.r, mColor.g, mColor.b, tCurrentAlpha));
            scale += aScale * Time.deltaTime / aDuration;
            yield return null;
        }
    }
}
