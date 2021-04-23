using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TravelerResult : MyBehaviour {
    [SerializeField]
    public FaceStamp mFaceStamp;
    public ResultGauge mGauge;
    public ResultNumber mResultNumber;
    public MyBehaviour mRanking;
    public SpriteRenderer mRankingRenderer;
    public MyBehaviour mRetire;
}
