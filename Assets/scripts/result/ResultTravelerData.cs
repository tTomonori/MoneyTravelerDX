using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTravelerData {
    public TravelerStatus mTraveler;
    public List<(int, int)> mResultNumberAndPoint;
    public int mRanking;
    public int mTotalPoint;
    public ResultTravelerData(TravelerStatus aTraveler) {
        mTraveler = aTraveler;
        mResultNumberAndPoint = new List<(int, int)>();
        mRanking = 0;
        mTotalPoint = 0;
    }
}
