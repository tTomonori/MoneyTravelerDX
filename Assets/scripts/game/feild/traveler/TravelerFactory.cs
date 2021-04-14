using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class TravelerFactory {
    static public List<TravelerStatus> create(GameFeild aFeild) {
        List<TravelerStatus> tTravelers = new List<TravelerStatus>();
        //スタートを探す
        StartMass tStartMass;
        int tStartMassNumber;
        for(int i=0; ; i++) {
            GameMass tMass = aFeild.mMassList[i];
            if (!(tMass is StartMass)) continue;
            tStartMass = (StartMass)tMass;
            tStartMassNumber = i;
            break;
        }
        for(int i = 0; i < 4; i++) {
            TravelerStatus tTraveler = new TravelerStatus();
            tTraveler.mTravelerData = GameData.mGameSetting.mTravelerData[i];
            tTraveler.mAi = tTraveler.mTravelerData.mAiPattern.getAi();
            tTraveler.mOrbit = 1;
            tTraveler.mRanking = 1;
            tTraveler.mPlayerNumber = i + 1;
            tTraveler.mCurrentMassNumber = tStartMassNumber;
            tTraveler.mComa = tTraveler.mTravelerData.mTravelerCharaData.createComa();
            tTraveler.mComa.transform.SetParent(aFeild.mComaContainer.transform);
            tTraveler.mComa.position = tStartMass.worldPosition;
            tTravelers.Add(tTraveler);
        }
        return tTravelers;
    }
}