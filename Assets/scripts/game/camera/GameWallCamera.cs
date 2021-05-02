using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWallCamera : GameCamera {
    [SerializeField]
    public float mWallPosition;
    public float mFloorPosition;
    //撮影対象の場所へ移動する
    public override void moveToTarget() {
        zoom(-0.1f);
        Vector3 tTargetPosition = toTargetPosition(mTarget.transform.position);
        Vector3 tCameraPosition = this.worldPosition;
        float tDistance = Vector2.Distance(tTargetPosition, tCameraPosition);
        if (tDistance < mMoveSpeed * Time.deltaTime) {
            this.worldPosition = tTargetPosition;
            return;
        }
        Vector3 tVec = tTargetPosition - this.worldPosition;
        this.worldPosition += tVec.normalized * (mMoveSpeed * Time.deltaTime);
    }
    //撮影対象をベストポジションで撮影している
    public override bool isShooting() {
        return mTarget.transform.position.x == this.worldPosition.x && mTarget.transform.position.y == this.worldPosition.y && mTarget.transform.position.z == this.worldPosition.z && mCameraContainer.scale.x == 1;
    }
    //指定した対象の位置を撮影する
    public override void shoot(MonoBehaviour aTarget) {
        this.worldPosition = new Vector3(aTarget.transform.position.x, aTarget.transform.position.y, aTarget.transform.position.z);
    }
    public override void move(Vector2 aVec) {
        if (this.position.z >= mWallPosition) {
            this.position += new Vector3(aVec.x, aVec.y, 0);
            if (this.position.y < mFloorPosition) {
                this.positionZ -= mFloorPosition - this.position.y;
                this.positionY = mFloorPosition;
            }
        } else {
            this.position += new Vector3(aVec.x, 0, aVec.y);
            if (this.position.z > mWallPosition) {
                this.positionY += mWallPosition - this.position.z;
                this.positionZ = mWallPosition;
            }
        }
        orthodonticsPosition();
    }
    public override void orthodonticsPosition() {
        if (mCamera.worldPosition.x > GameData.mStageData.mEast - 1) {
            this.positionX -= mCamera.worldPosition.x - (GameData.mStageData.mEast - 1);
        }
        if (mCamera.worldPosition.x < GameData.mStageData.mWest + 1) {
            this.positionX -= mCamera.worldPosition.x - (GameData.mStageData.mWest + 1);
        }
        if (mCamera.worldPosition.y > GameData.mStageData.mCeiling - 7) {
            this.positionY -= mCamera.worldPosition.y - (GameData.mStageData.mCeiling - 7);
        }
        if (mCamera.worldPosition.y < mFloorPosition) {
            this.positionY -= mCamera.worldPosition.z - (mFloorPosition);
        }
        if (mCamera.worldPosition.z > mWallPosition) {
            this.positionZ -= mCamera.worldPosition.z - (mWallPosition);
        }
        if (mCamera.worldPosition.z < GameData.mStageData.mSouth + 1) {
            this.positionZ -= mCamera.worldPosition.z - (GameData.mStageData.mSouth + 1);
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
    public Vector3 toTargetPosition(Vector3 aVector) {
        if (aVector.z < mWallPosition) {
            return new Vector3(aVector.x, mFloorPosition, aVector.z);
        } else {
            return new Vector3(aVector.x, aVector.y, mWallPosition);
        }
    }
}
