using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KinCoin : MyBehaviour {
    public void lost(Action aCallback) {
        StartCoroutine(lostDelta(aCallback));
    }
    private IEnumerator lostDelta(Action aCallback) {
        float tLoopTime = 1;//180度分移動するまでの時間
        float tEndTime = 0.8f;
        float tPassTime = 0;
        float tDirection = UnityEngine.Random.Range(0, 359);
        float tXSize = 2;
        float tYSize = 2;
        while (true) {
            tPassTime += Time.deltaTime;
            if (tEndTime <= tPassTime) {
                delete();
                aCallback();
                yield break;
            }
            float tX = tPassTime / tLoopTime;
            float tY = Mathf.Sin(tPassTime / tLoopTime * Mathf.PI);
            Vector3 tPosition = new Vector3(tX * tXSize, tY * tYSize, 0);
            tPosition = Quaternion.Euler(0, tDirection, 0) * tPosition;
            this.position = tPosition;
            yield return null;
        }
    }
    public void get(Action aCallback) {
        this.positionY = 3.5f;
        this.moveBy(new Vector2(0, -1.5f), 0.3f, () => {
            delete();
            aCallback();
        });
    }
}
