using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class GameMasterSearcher {
    //プレイ中のトラベラーのリストを返す
    static public List<TravelerStatus> getActiveTravelers(this GameMaster aMaster) {
        List<TravelerStatus> tTravelers = new List<TravelerStatus>();
        foreach(TravelerStatus tTraveler in aMaster.mTurnOrder) {
            if (tTraveler.mIsRetired) continue;
            tTravelers.Add(tTraveler);
        }
        return tTravelers;
    }
    //指定したトラベラーのスタート地点を返す
    static public StartMass getStart(this GameMaster aMaster,TravelerStatus aTraveler) {
        foreach(GameMass tMass in aMaster.mFeild.mMassList) {
            if (!(tMass is StartMass)) continue;
            return (StartMass)tMass;
        }
        throw new Exception();
    }
}
