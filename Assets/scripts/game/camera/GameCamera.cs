using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GameCamera : MyBehaviour {
    [SerializeField]
    public MyBehaviour mCamera;
    public MyBehaviour mCameraContainer;
    [System.NonSerialized] public MonoBehaviour mTarget;
    [System.NonSerialized] public float mMoveSpeed = 100;
    private void Update() {
        if (mTarget == null) return;
        if (isShooting()) return;
        moveToTarget();
    }
    //撮影対象の場所へ移動する
    abstract public void moveToTarget();
    //撮影対象をベストポジションで撮影している
    abstract public bool isShooting();
    //指定した対象の位置を撮影する
    abstract public void shoot(MonoBehaviour aTarget);
    //カメラを移動させる
    abstract public void move(Vector2 aVec);
    //カメラがフィールドの範囲外にある場合は調整する
    abstract public void orthodonticsPosition();
    //ズームインorズームアウト
    abstract public void zoom(float aScale);
}
