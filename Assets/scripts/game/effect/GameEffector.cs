using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class GameEffector {
    static public void lostCoin(Vector3 aPosition, string aLabel, Action aCallback) {
        MyBehaviour tContainer = MyBehaviour.create<MyBehaviour>();
        tContainer.name = "lostCoin";
        tContainer.position = aPosition;

        MySoundPlayer.playSe("lost", false);

        CallbackSystem tSystem = new CallbackSystem();
        KinCoin tPrefab = Resources.Load<KinCoin>("models/kinCoin/kinCoin");
        for (int i = 0; i < 10; i++) {
            Action tCounter = tSystem.getCounter();
            MyBehaviour.setTimeoutToIns(0.1f * i, () => {
                KinCoin tCoin = GameObject.Instantiate(tPrefab);
                tCoin.transform.SetParent(tContainer.transform, false);
                tCoin.lost(tCounter);
            });
        }
        displayText(aPosition, aLabel + " ", new Color(1, 0, 0, 1), tSystem.getCounter());
        tSystem.then(()=> {
            tContainer.delete();
            aCallback();
        });
    }
    static public void getCoin(Vector3 aPosition, string aLabel, Action aCallback) {
        MyBehaviour tContainer = MyBehaviour.create<MyBehaviour>();
        tContainer.name = "getCoin";
        tContainer.position = aPosition;

        MySoundPlayer.playSe("get", false);

        CallbackSystem tSystem = new CallbackSystem();
        KinCoin tPrefab = Resources.Load<KinCoin>("models/kinCoin/kinCoin");
        for (int i = 0; i < 7; i++) {
            Action tCounter = tSystem.getCounter();
            MyBehaviour.setTimeoutToIns(0.1f * i, () => {
                KinCoin tCoin = GameObject.Instantiate(tPrefab);
                tCoin.transform.SetParent(tContainer.transform, false);
                tCoin.get(tCounter);
            });
        }
        displayText(aPosition, aLabel + " ", new Color(0, 0, 1, 1), tSystem.getCounter());
        tSystem.then(()=> {
            tContainer.delete();
            aCallback();
        });
    }
    static public void generateAura(Vector3 aPosition, Color aColor, Action aCallback) {
        Aura tAura = GameObject.Instantiate(Resources.Load<Aura>("models/aura/aura"));
        tAura.position = aPosition;
        tAura.mColor = aColor;
        MySoundPlayer.playSe("aura", false);
        tAura.animate(() => {
            tAura.delete();
            aCallback();
        });
    }
    static public void displayText(Vector3 aPosition, string aText, Color aColor, Action aCallback) {
        CallbackSystem tSystem = new CallbackSystem();
        for (int i = 0; i < aText.Length; i++) {
            int tIndex = i;
            Action tCounter = tSystem.getCounter();
            //TextMesh tMesh = GameObject.Instantiate(Resources.Load<TextMesh>("prefabs/text/3DTextMesh"));
            TextMesh tMesh = MyBehaviour.create<TextMesh>();
            tMesh.characterSize = 0.1f;
            tMesh.anchor = TextAnchor.MiddleCenter;
            tMesh.fontStyle = FontStyle.Bold;
            MyBehaviour tBehaviour = tMesh.gameObject.AddComponent<MyBehaviour>();
            tMesh.text = aText.Substring(i, 1);
            tMesh.fontSize = 100;
            tMesh.color = aColor;
            tBehaviour.position = aPosition + new Vector3(0, 0.5f, 0);
            tBehaviour.scale2D = Vector2.zero;
            MyBehaviour.setTimeoutToIns(i * 0.07f, () => {
                tBehaviour.scaleTo(new Vector2(1, 1), 0.4f);
                tBehaviour.moveBy(new Vector3(0.6f * (-aText.Length / 2f + 0.5f + tIndex), 1, -1), 0.4f);
                tBehaviour.StartCoroutine(sinMove(0.5f, 0.4f, tBehaviour, () => { }));
            });
            MyBehaviour.setTimeoutToIns(aText.Length * 0.07f + 0.4f + 0.3f, () => {
                tBehaviour.opacityBy(-1, 0.5f);
                tBehaviour.moveBy(new Vector2(0, 1), 0.55f, () => {
                    tBehaviour.delete();
                    tCounter();
                });
            });
        }
        tSystem.then(aCallback);
    }
    static private IEnumerator sinMove(float aHeight, float aDuration, MyBehaviour aBehaviour, Action aCallback) {
        float tElapsedTime = 0;
        float tCurrentHeight = 0;
        float tHeight = 0;
        while (true) {
            if (tElapsedTime + Time.deltaTime >= aDuration) {//完了
                aBehaviour.positionY -= tCurrentHeight;
                if (aCallback != null) aCallback();
                yield break;
            }
            tElapsedTime += Time.deltaTime;
            tHeight = Mathf.Sin(tElapsedTime / aDuration * Mathf.PI);
            aBehaviour.positionY += tHeight - tCurrentHeight;
            tCurrentHeight = tHeight;
            yield return null;
        }
    }
}
