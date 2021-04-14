using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MyBehaviour {
    [SerializeField]
    public MyBehaviour mCamera;
    [System.NonSerialized] public MonoBehaviour mTarget;
    [System.NonSerialized] public float mMoveSpeed = 100;
    private void Update() {
        if (mTarget == null) return;
        moveToTarget();
    }
    //撮影対象の場所へ移動する
    public void moveToTarget() {
        if (isShooting()) return;
        float tDistance = Vector3.Distance(mTarget.transform.position, this.worldPosition);
        if (tDistance < mMoveSpeed * Time.deltaTime) {
            this.worldPosition = mTarget.transform.position;
            return;
        }
        Vector3 tVec = mTarget.transform.position - this.worldPosition;
        this.worldPosition += tVec.normalized * (mMoveSpeed * Time.deltaTime);
    }
    //撮影対象をベストポジションで撮影している
    public bool isShooting() {
        return Vector3.Distance(mTarget.transform.position, this.worldPosition) < 0.05f;
    }
    //指定した対象の位置を撮影する
    public void shoot(MonoBehaviour aTarget) {
        this.worldPosition = aTarget.transform.position;
    }
    public void move(Vector2 aVec) {
        this.position += new Vector3(aVec.x, 0, aVec.y);
    }
}
