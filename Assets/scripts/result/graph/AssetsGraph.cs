using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AssetsGraph : MyBehaviour {
    public Action mCallback;
    [SerializeField]
    public List<FaceStamp> mFaceStamps;
    public List<LineGraph> mLineGraphs;
    public float mTop;
    public float mBottom;
    public float mRight;
    public float mLeft;
    public float mLineThickness;
    static public AssetsGraph show(List<ResultTravelerData> aResult, Action aCallback) {
        AssetsGraph tGraph = GameObject.Instantiate(Resources.Load<AssetsGraph>("prefabs/result/assetsGraph"));
        tGraph.mCallback = aCallback;
        tGraph.set(aResult);
        return tGraph;
    }
    public void set(List<ResultTravelerData> aResult) {
        //グラフ以外の準備,総資産の最大値探し
        int tMaxAssets = 0;
        for (int i = 0; i < GameData.mTravelerNumber; i++) {
            ResultTravelerData tData = aResult[i];
            if (tData == null) {
                mFaceStamps[i].gameObject.SetActive(false);
                continue;
            }
            mFaceStamps[i].mImage.sprite = tData.mTraveler.mTravelerData.mTravelerCharaData.getImage();
            foreach (int tNum in tData.mTraveler.mAssetsTransitionList) {
                if (tMaxAssets < tNum)
                    tMaxAssets = tNum;
            }
            if (tMaxAssets < tData.mTotalPoint)
                tMaxAssets = tData.mTotalPoint;
        }
        //グラフ作成
        for (int i = 0; i < GameData.mTravelerNumber; i++) {
            ResultTravelerData tData = aResult[i];
            if (tData == null) continue;
            LineGraph tGrap = mLineGraphs[i];
            List<float> tAssets = new List<float>(tData.mTraveler.mAssetsTransitionList.Select((a) => (float)(a)));
            tAssets.Add(tData.mTotalPoint);
            tGrap.mTop = mTop;
            tGrap.mBottom = mBottom;
            tGrap.mRight = mRight;
            tGrap.mLeft = mLeft;
            tGrap.mMaxNumber = tMaxAssets;
            tGrap.mMinNumber = 0;
            tGrap.mLineThickness = mLineThickness;
            tGrap.mColor = tData.mTraveler.playerColor;
            tGrap.set(tAssets);
            tGrap.positionZ = -0.5f + i * 0.1f;
        }
        setObserver();
    }
    public void graphToFront(int aIndex) {
        LineGraph tFront = mLineGraphs[0];
        foreach (LineGraph tGraph in mLineGraphs) {
            if (tFront.positionZ > tGraph.positionZ)
                tFront = tGraph;
        }
        float tTemp = tFront.positionZ;
        tFront.positionZ = mLineGraphs[aIndex].positionZ;
        mLineGraphs[aIndex].positionZ = tTemp;
    }
    private void setObserver() {
        Subject.addObserver(new Observer("assetsGraph", (aMessage) => {
            switch (aMessage.name) {
                case "stampOfGraph1Pushed":
                    graphToFront(0);
                    return;
                case "stampOfGraph2Pushed":
                    graphToFront(1);
                    return;
                case "stampOfGraph3Pushed":
                    graphToFront(2);
                    return;
                case "stampOfGraph4Pushed":
                    graphToFront(3);
                    return;
                case "stampOfGraph5Pushed":
                    graphToFront(4);
                    return;
                case "stampOfGraph6Pushed":
                    graphToFront(5);
                    return;
                case "graphBackPushed":
                    delete();
                    mCallback();
                    return;
            }
        }));
    }
    private void OnDestroy() {
        Subject.removeObserver("assetsGraph");
    }
}
