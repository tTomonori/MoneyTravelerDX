using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HangingBoard : MyBehaviour {
    static private List<HangingBoard> mBoards = new List<HangingBoard>();
    public enum BoardImage {
        arrow, map, paper
    }
    [SerializeField]
    public MyBehaviour mObjects;
    public MyButton mButton;
    public SpriteRenderer mArrowImage;
    public SpriteRenderer mMapImage;
    public SpriteRenderer mPaperImage;
    private Coroutine mAnimation;
    private bool mIsClosing = false;
    static public HangingBoard create(BoardImage aBoardImage,Action aOnClicked) {
        HangingBoard tBoard = GameObject.Instantiate(Resources.Load<HangingBoard>("prefabs/ui/hangingBoard"));
        if (mBoards.Count != 0)
            tBoard.positionZ = mBoards[mBoards.Count - 1].positionZ - 0.1f;
        mBoards.Add(tBoard);
        tBoard.setImage(aBoardImage);
        tBoard.mButton.mPushedFunction = aOnClicked;
        return tBoard;
    }
    public void open() {
        mAnimation = mObjects.rotateZByWithSpeed(-180, 360, () => {
            mButton.gameObject.SetActive(true);
            mAnimation = null;
        });
    }
    public void close() {
        if (mIsClosing) return;
        mIsClosing = true;
        mButton.gameObject.SetActive(false);
        if (mAnimation != null)
            mObjects.StopCoroutine(mAnimation);
        mAnimation = null;
        float tRotateZ = mObjects.rotateZ;
        tRotateZ = 180 - tRotateZ;
        if (tRotateZ < 0) tRotateZ += 360;
        mObjects.rotateZByWithSpeed(tRotateZ, 360, () => {
            this.delete();
        });
    }
    public void setImage(BoardImage aBoardImage) {
        mArrowImage.gameObject.SetActive(false);
        mMapImage.gameObject.SetActive(false);
        mPaperImage.gameObject.SetActive(false);
        switch (aBoardImage) {
            case BoardImage.arrow:
                mArrowImage.gameObject.SetActive(true);
                return;
            case BoardImage.map:
                mMapImage.gameObject.SetActive(true);
                return;
            case BoardImage.paper:
                mPaperImage.gameObject.SetActive(true);
                return;
        }
    }
    private void OnDestroy() {
        mBoards.Remove(this);
    }
}
