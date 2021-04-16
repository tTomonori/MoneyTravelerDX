using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
static public partial class EventMassEventManager {
    static public void runDisasterEvent(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        List<(Action<TravelerStatus, GameMaster, Action>, float)> tEventList = new List<(Action<TravelerStatus, GameMaster, Action>, float)>();
        tEventList.Add((hugeEarthquake, 3));
        tEventList.Add((randomDisaster, 5));
        tEventList.Add((randomCatastrophe, 2));
        pickEvent(tEventList)(aTraveler, aMaster, aCallback);
    }
    //指定したトラベラーの指定した属性の土地の価値の合計を返す
    static public int getLandValue(TravelerStatus aTraveler, GameFeild aFeild, LandAttribute aAttribute) {
        int tTotal = 0;
        foreach (LandMass tLand in aFeild.getOwnedLand(aTraveler)) {
            if (aAttribute == LandAttribute.none || tLand.mAttributes.Contains(aAttribute))
                tTotal += tLand.mTotalValue;
        }
        return tTotal;
    }
    //指定したトラベラーに指定した額の災害被害を与える
    static public void addDisasterDamage(TravelerStatus aTraveler, int aMoney, GameMaster aMaster, Action aCallback) {
        GameData.mStageData.mCamera.mTarget = aTraveler.mComa;
        GameEffector.lostCoin(aTraveler.mComa.worldPosition, "-" + aMoney.ToString(), () => {
            aTraveler.disasterDamage(aMoney);
            aMaster.updateStatusDisplay();
            BankruptcyEventManager.checkRankruptcy(aTraveler, aMaster, aCallback);
        });
    }
    //それぞれのトラベラーにそれぞれ指定した額の災害被害を与える
    static public void continuousDisasterDamage(List<(TravelerStatus, int)> aDamageList, GameMaster aMaster, Action aCallback) {
        int tLength = aDamageList.Count;
        Action<int> tFunction = null;
        tFunction = (aIndex) => {
            addDisasterDamage(aDamageList[aIndex].Item1, aDamageList[aIndex].Item2, aMaster, () => {
                if (aIndex + 1 < tLength) {
                    tFunction(aIndex + 1);
                    return;
                }
                aCallback();
            });
        };
        tFunction(0);
    }
    //指定した属性の土地に指定した%の被害
    static public void disaster(GameMaster aMaster, LandAttribute aAttribute, float tRate, Action aCallback) {
        List<(TravelerStatus, int)> tDamageList = new List<(TravelerStatus, int)>();
        foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
            if (tTraveler.mIsRetired) continue;
            tDamageList.Add((tTraveler, (int)(getLandValue(tTraveler, aMaster.mFeild, aAttribute) * tRate / 100f)));
        }
        continuousDisasterDamage(tDamageList, aMaster, aCallback);
    }
    //巨大地震
    static public void hugeEarthquake(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tRate = UnityEngine.Random.Range(5, 7);
        aMaster.mUiMain.displayEventDescription("巨大地震が全世界を襲う!\n物件の" + tRate.ToString() + "%の被害", () => {
            disaster(aMaster, LandAttribute.none, tRate, aCallback);
        });
    }
    //ランダムな属性の土地に災害
    static public void randomDisaster(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tRate = UnityEngine.Random.Range(5, 7);
        List<LandAttribute> tAttributes = aMaster.mFeild.getAttributesIncludingCover();
        LandAttribute tAttribute = tAttributes[UnityEngine.Random.Range(0, tAttributes.Count)];
        string tText = "";
        switch (tAttribute) {
            case LandAttribute.north:
            case LandAttribute.east:
            case LandAttribute.south:
            case LandAttribute.west:
            case LandAttribute.center:
                tText = tAttribute.getName() + "で地震が発生!";
                break;
            case LandAttribute.waterside:
                tText = tAttribute.getName() + "で渦潮が発生!";
                break;
            case LandAttribute.woods:
                tText = tAttribute.getName() + "で大雨が発生!";
                break;
        }
        aMaster.mUiMain.displayEventDescription(tText + "\n物件の" + tRate.ToString() + "%の被害", () => {
            disaster(aMaster, tAttribute, tRate, aCallback);
        });
    }
    //ランダムな属性の土地に大きな災害
    static public void randomCatastrophe(TravelerStatus aTraveler, GameMaster aMaster, Action aCallback) {
        int tRate = UnityEngine.Random.Range(7, 12);
        List<LandAttribute> tAttributes = aMaster.mFeild.getAttributesIncludingCover();
        LandAttribute tAttribute = tAttributes[UnityEngine.Random.Range(0, tAttributes.Count)];
        string tText = "";
        switch (tAttribute) {
            case LandAttribute.north:
            case LandAttribute.east:
            case LandAttribute.south:
            case LandAttribute.west:
            case LandAttribute.center:
                tText = tAttribute.getName() + "を竜巻が襲う!!";
                break;
            case LandAttribute.waterside:
                tText = tAttribute.getName() + "で津波が発生!!";
                break;
            case LandAttribute.woods:
                tText = tAttribute.getName() + "に酸性雨が降り注ぐ!!";
                break;
        }
        aMaster.mUiMain.displayEventDescription(tText + "\n物件の" + tRate.ToString() + "%の被害", () => {
            disaster(aMaster, tAttribute, tRate, aCallback);
        });
    }
}
