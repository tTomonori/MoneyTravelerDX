using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum OrbitEventType {
    none, always, firstRide, firstCome, bottom, bottomHalf
}

static public class OrbitEventTypeMethods {
    static public string getName(this OrbitEventType e) {
        switch (e) {
            case OrbitEventType.none:
                return "なし";
            case OrbitEventType.always:
                return "あり";
            case OrbitEventType.firstRide:
                return "一番乗り";
            case OrbitEventType.firstCome:
                return "先着";
            case OrbitEventType.bottom:
                return "ビリのみ";
            case OrbitEventType.bottomHalf:
                return "下位のみ";
        }
        throw new Exception();
    }
    static public bool canRunEvent(this OrbitEventType e, TravelerStatus aTraveler, GameMaster aMaster) {
        switch (e) {
            case OrbitEventType.none:
                return false;
            case OrbitEventType.always:
                return true;
            case OrbitEventType.firstRide:
                int tFirstRide = 0;
                foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                    if (aTraveler.mOrbit <= tTraveler.mOrbit)
                        tFirstRide++;
                }
                return tFirstRide <= 1;
            case OrbitEventType.firstCome:
                int tFirstCome = 0;
                foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                    if (aTraveler.mOrbit <= tTraveler.mOrbit)
                        tFirstCome++;
                }
                return tFirstCome <= aMaster.mTurnOrder.Count / 2f;
            case OrbitEventType.bottom:
                foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
                    if (aTraveler.mRanking < tTraveler.mRanking)
                        return false;
                }
                return true;
            case OrbitEventType.bottomHalf:
                return aTraveler.mRanking > Mathf.Ceil(aMaster.mTurnOrder.Count / 2f);
        }
        throw new Exception();
    }
}