using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph : MyBehaviour {
    public float mTop;
    public float mBottom;
    public float mRight;
    public float mLeft;
    public float mMaxNumber;
    public float mMinNumber;
    public float mLineThickness;
    public Color mColor;
    public int mDataNum;
    public void set(List<float> aData) {
        mDataNum = aData.Count;
        for (int i = 0; i < mDataNum - 1; i++) {
            float tNumber = aData[i];
            if (tNumber < mMinNumber) tNumber = mMinNumber;
            Vector2 tLeftPosition = new Vector2();
            tLeftPosition.x = mLeft + i * (mRight - mLeft) / (mDataNum - 1);
            tLeftPosition.y = mBottom + ((tNumber - mMinNumber) / (mMaxNumber - mMinNumber)) * (mTop - mBottom);

            tNumber = aData[i + 1];
            if (tNumber < mMinNumber) tNumber = mMinNumber;
            Vector2 tRightPosition = new Vector2();
            tRightPosition.x = mLeft + (i + 1) * (mRight - mLeft) / (mDataNum - 1);
            tRightPosition.y = mBottom + ((tNumber - mMinNumber) / (mMaxNumber - mMinNumber)) * (mTop - mBottom);
            createAndSetLine(tLeftPosition, tRightPosition);
        }
    }
    public void createAndSetLine(Vector2 aLeftPosition, Vector2 aRightPosition) {
        MyBehaviour tLine = createLine(Vector2.Distance(aLeftPosition, aRightPosition));
        tLine.position2D = (aLeftPosition + aRightPosition) / 2f;
        tLine.rotateZ = VectorCalculator.corner(new Vector2(1, 0), aRightPosition - aLeftPosition);
    }
    public MyBehaviour createLine(float aLength) {
        MyBehaviour tLine = createChild<MyBehaviour>("line");
        MyBehaviour tContents;
        SpriteRenderer tRenderer;
        //line
        tContents = tLine.createChild<MyBehaviour>("bar");
        tRenderer = tContents.gameObject.AddComponent<SpriteRenderer>();
        tRenderer.sprite = Resources.Load<Sprite>("sprites/squareMask");
        tRenderer.color = mColor;
        tContents.scale2D = new Vector2(aLength, mLineThickness);
        //pointLeft
        tContents = tLine.createChild<MyBehaviour>("pointLeft");
        tRenderer = tContents.gameObject.AddComponent<SpriteRenderer>();
        tRenderer.sprite = Resources.Load<Sprite>("sprites/circleMask");
        tRenderer.color = mColor;
        tContents.scale2D = new Vector2(mLineThickness, mLineThickness);
        tContents.positionX = -aLength / 2f;
        //pointRight
        tContents = tLine.createChild<MyBehaviour>("pointRight");
        tRenderer = tContents.gameObject.AddComponent<SpriteRenderer>();
        tRenderer.sprite = Resources.Load<Sprite>("sprites/circleMask");
        tRenderer.color = mColor;
        tContents.scale2D = new Vector2(mLineThickness, mLineThickness);
        tContents.positionX = aLength / 2f;

        return tLine;
    }
}
