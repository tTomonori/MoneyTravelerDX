using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameXZCamera : GameCamera {
    //撮影対象の場所へ移動する
    public override void moveToTarget() {
        zoom(-0.1f);
        Vector2 tTargetPosition = new Vector2(mTarget.transform.position.x, mTarget.transform.position.z);
        Vector2 tCameraPosition = new Vector2(this.worldPosition.x, this.worldPosition.z);
        float tDistance = Vector2.Distance(tTargetPosition, tCameraPosition);
        if (tDistance < mMoveSpeed * Time.deltaTime) {
            this.worldPosition = mTarget.transform.position;
            this.worldPosition = new Vector3(mTarget.transform.position.x, this.worldPosition.y, mTarget.transform.position.z);
            return;
        }
        Vector3 tVec = mTarget.transform.position - this.worldPosition;
        tVec.y = 0;
        this.worldPosition += tVec.normalized * (mMoveSpeed * Time.deltaTime);
    }
    //撮影対象をベストポジションで撮影している
    public override bool isShooting() {
        //Vector2 tTargetPosition = new Vector2(mTarget.transform.position.x, mTarget.transform.position.z);
        //Vector2 tCameraPosition = new Vector2(this.worldPosition.x, this.worldPosition.z);
        //return Vector2.Distance(tTargetPosition, tCameraPosition) < 0.05f;
        return mTarget.transform.position.x == this.worldPosition.x && mTarget.transform.position.z == this.worldPosition.z && mCameraContainer.scale.x == 1;
    }
    //指定した対象の位置を撮影する
    public override void shoot(MonoBehaviour aTarget) {
        this.worldPosition = new Vector3(aTarget.transform.position.x, this.worldPosition.y, aTarget.transform.position.z);
    }
    public override void move(Vector2 aVec) {
        this.position += new Vector3(aVec.x, 0, aVec.y);
        orthodonticsPosition();
    }
    public override void orthodonticsPosition() {
        if (mCamera.worldPosition.z > GameData.mStageData.mNorth - 7) {
            this.positionZ -= mCamera.worldPosition.z - (GameData.mStageData.mNorth - 7);
        }
        if (mCamera.worldPosition.x > GameData.mStageData.mEast - 1) {
            this.positionX -= mCamera.worldPosition.x - (GameData.mStageData.mEast - 1);
        }
        if (mCamera.worldPosition.z < GameData.mStageData.mSouth + 1) {
            this.positionZ -= mCamera.worldPosition.z - (GameData.mStageData.mSouth + 1);
        }
        if (mCamera.worldPosition.x < GameData.mStageData.mWest + 1) {
            this.positionX -= mCamera.worldPosition.x - (GameData.mStageData.mWest + 1);
        }
    }
    public override void zoom(float aScale) {
        mCameraContainer.scale += new Vector3(aScale, aScale, aScale);
        if (mCameraContainer.scale.x < 1)
            mCameraContainer.scale = new Vector3(1, 1, 1);
        if (mCameraContainer.scale.x > 2)
            mCameraContainer.scale = new Vector3(2, 2, 2);
        orthodonticsPosition();
    }
}
