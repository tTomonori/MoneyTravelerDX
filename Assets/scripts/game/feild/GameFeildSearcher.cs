using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GameFeildSearche {
    //landを全て返す
    static public List<LandMass> getAllLands(this GameFeild aFeild) {
        List<LandMass> tLands = new List<LandMass>();
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            tLands.Add((LandMass)tMass);
        }
        return tLands;
    }
    //指定したトラベラーが所有している土地のリストを返す
    static public List<LandMass> getOwnedLand(this GameFeild aFeild, TravelerStatus aTraveler) {
        List<LandMass> tLands = new List<LandMass>();
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOwner != aTraveler) continue;
            tLands.Add(tLand);
        }
        return tLands;
    }
    //指定したトラベラー以外が所有している土地のリストを返す
    static public List<LandMass> getOtherOwnedLand(this GameFeild aFeild, TravelerStatus aTraveler) {
        List<LandMass> tLands = new List<LandMass>();
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOwner == null) continue;
            if (tLand.mOwner == aTraveler) continue;
            tLands.Add(tLand);
        }
        return tLands;
    }
    //指定したトラベラー以外が所有する土地の料金の平均を返す
    static public float calcurateFeeAverage(this GameFeild aFeild, TravelerStatus aTraveler, LandMass aExceptLand = null) {
        float tTotal = 0;
        int tNum = 0;
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand == aExceptLand) continue;
            if (tLand.mOwner == null || tLand.mOwner == aTraveler) continue;
            tTotal += tLand.mFeeCost;
            tNum++;
        }
        if (tNum == 0) return 0;
        return tTotal / tNum;
    }
    //指定したトラベラー以外が所有する土地の中で最も料金が高い土地を返す
    static public LandMass searchExpensivestFeeLand(this GameFeild aFeild, TravelerStatus aTraveler, LandMass aExceptLand = null) {
        LandMass tExpensivestLand = null;
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand == aExceptLand) continue;
            if (tLand.mOwner == null || tLand.mOwner == aTraveler) continue;
            if (tExpensivestLand == null) {
                tExpensivestLand = tLand;
                continue;
            }
            if (tExpensivestLand.mFeeCost < tLand.mFeeCost)
                tExpensivestLand = tLand;
        }
        return tExpensivestLand;
    }
    //所有者がいないマスの値段の平均を返す
    static public float calculatePriceAverage(this GameFeild aFeild) {
        float tTotal = 0;
        int tNum = 0;
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOwner != null) continue;
            tTotal += tLand.mPurchaseCost;
            tNum++;
        }
        if (tNum == 0) return 0;
        return tTotal / tNum;
    }
    //指定したトラベラーが所有する土地の中で価値が最も低い土地を返す
    static public LandMass searchCheapestValueLand(this GameFeild aFeild, TravelerStatus aTraveler) {
        LandMass tCheapest = null;
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOwner != aTraveler) continue;
            if (tCheapest == null) {
                tCheapest = tLand;
                continue;
            }
            if (tCheapest.mTotalValue > tLand.mTotalValue)
                tCheapest = tLand;
        }
        return tCheapest;
    }
    //指定したトラベラーが所有する土地の中で料金が最も低い土地を返す
    static public LandMass searchCheapestFeeLand(this GameFeild aFeild, TravelerStatus aTraveler) {
        LandMass tCheapest = null;
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOwner != aTraveler) continue;
            if (tCheapest == null) {
                tCheapest = tLand;
                continue;
            }
            if (tCheapest.mFeeCost > tLand.mFeeCost)
                tCheapest = tLand;
        }
        return tCheapest;
    }
    //指定したトラベラーが所有する指定した売却額を超える土地の中で料金が最も低い土地を返す
    static public LandMass searchCheapestFeeOfOverSellCostLand(this GameFeild aFeild, TravelerStatus aTraveler, int aSellCost) {
        LandMass tCheapest = null;
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mOwner != aTraveler) continue;
            if (tLand.mSellCost < aSellCost) continue;
            if (tCheapest == null)
                tCheapest = tLand;
            if (tCheapest.mFeeCost > tLand.mFeeCost)
                tCheapest = tLand;
        }
        return tCheapest;
    }
    //ステージに含まれる土地の属性を返す
    static public List<LandAttribute> getAttributes(this GameFeild aFeild) {
        List<LandAttribute> tAttributes = new List<LandAttribute>();
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mAttributes[0] != LandAttribute.none) {
                if (!tAttributes.Contains(tLand.mAttributes[0]))
                    tAttributes.Add(tLand.mAttributes[0]);
            }
            if (tLand.mAttributes[1] != LandAttribute.none) {
                if (!tAttributes.Contains(tLand.mAttributes[1]))
                    tAttributes.Add(tLand.mAttributes[1]);
            }
        }
        return tAttributes;
    }
    //ステージに含まれる土地の属性を被りを含めて全て返す
    static public List<LandAttribute> getAttributesIncludingCover(this GameFeild aFeild) {
        List<LandAttribute> tAttributes = new List<LandAttribute>();
        foreach (GameMass tMass in aFeild.mMassList) {
            if (!(tMass is LandMass)) continue;
            LandMass tLand = (LandMass)tMass;
            if (tLand.mAttributes[0] != LandAttribute.none)
                tAttributes.Add(tLand.mAttributes[0]);
            if (tLand.mAttributes[1] != LandAttribute.none)
                tAttributes.Add(tLand.mAttributes[1]);
        }
        return tAttributes;
    }
}