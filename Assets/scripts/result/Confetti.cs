using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Confetti : MyBehaviour {
    public float mSpeed = 1.5f;
    public float mMovedDistance = 0;
    public float mMaxMovedDistance = 10;
    public Vector3 mRotateDirection;
    public Vector3 mRotation;
    private void Start() {
        SpriteRenderer tRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        tRenderer.sprite = Resources.Load<Sprite>("sprites/squareMask");
        tRenderer.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        this.scale2D = new Vector2(0.2f, 0.2f);
        mRotateDirection = new Vector3(UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360));
    }
    private void Update() {
        if (mMovedDistance >= mMaxMovedDistance) {
            delete();
            return;
        }
        this.positionY -= Time.deltaTime * mSpeed;
        mMovedDistance += Time.deltaTime * mSpeed;

        mRotation = mRotation + mRotateDirection * Time.deltaTime;
        rotate = mRotation;
    }
}
