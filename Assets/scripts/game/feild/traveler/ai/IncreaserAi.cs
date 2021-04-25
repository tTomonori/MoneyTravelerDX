using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaserAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が自分の土地の増資コストの平均を下回らなければ購入する
        float tAve = aMaster.mFeild.calculateIncreaseCostAverage(aMyStatus);
        if (aMyStatus.mMoney - aLand.mPurchaseCost < tAve) {
            aCallback(false);
            return;
        }
        aCallback(true);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に増資する
        aCallback(true);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所持金が自分の土地の増資コストの平均を下回らないかつ100を下回らないなら購入する
        float tAve = aMaster.mFeild.calculateIncreaseCostAverage(aMyStatus);
        if (aMyStatus.mMoney - aLand.mAcquisitionCost < tAve || aMyStatus.mMoney - aLand.mAcquisitionCost < 100) {
            aCallback(false);
            return;
        }
        aCallback(true);
    }
    public override void selectPurchase(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //増資済みの土地があるなら、料金が最も高い土地を購入する
        //増資済みの土地がないなら、所持金が自分の土地の増資コストの平均を下回らないかつ100を下回らないなら最も購入価格が安い土地を購入する
        //最も増資レベルが高い土地を探す
        List<LandMass> tMostEncreased = new List<LandMass>();
        foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(null)) {
            if (!aMyStatus.canPurchase(tLand)) continue;
            if (tMostEncreased.Count == 0) {
                tMostEncreased.Add(tLand);
                continue;
            }
            if (tMostEncreased[0].mIncreaseLevel > tLand.mIncreaseLevel) continue;
            if (tMostEncreased[0].mIncreaseLevel == tLand.mIncreaseLevel) {
                tMostEncreased.Add(tLand);
                continue;
            }
            tMostEncreased.Clear();
            tMostEncreased.Add(tLand);
        }
        if (tMostEncreased.Count == 0) {
            aCallback(null);
            return;
        }
        if (tMostEncreased[0].mIncreaseLevel == 0) {
            //増資済みの土地なし
            LandMass tCheapest = null;
            foreach (LandMass tLand in tMostEncreased) {
                if (tCheapest == null) {
                    tCheapest = tLand;
                    continue;
                }
                if (tCheapest.mPurchaseCost > tLand.mPurchaseCost) {
                    tCheapest = tLand;
                }
            }
            float tAve = aMaster.mFeild.calculateIncreaseCostAverage(aMyStatus);
            if (aMyStatus.mMoney - tCheapest.mPurchaseCost < tAve || aMyStatus.mMoney - tCheapest.mPurchaseCost < 100) {
                aCallback(null);
                return;
            }
            aCallback(tCheapest);
            return;
        } else {
            //増資済みの土地あり
            LandMass tExpensivest = null;
            foreach (LandMass tLand in tMostEncreased) {
                if (tExpensivest == null) {
                    tExpensivest = tLand;
                    continue;
                }
                if (tExpensivest.mFeeCost <= tLand.mFeeCost) {
                    tExpensivest = tLand;
                }
            }
            aCallback(tExpensivest);
            return;
        }
    }
    public override void selectIncrease(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //最も増資コストが高い土地に増資する
        LandMass tExpensivest = null;
        foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(aMyStatus)) {
            if (!aMyStatus.canIncrease(tLand)) continue;
            if (aMyStatus.mMoney < tLand.mIncreaseCost) continue;
            if (tExpensivest == null) {
                tExpensivest = tLand;
                continue;
            }
            if (tExpensivest.mIncreaseCost <= tLand.mIncreaseCost) {
                tExpensivest = tLand;
            }
        }
        aCallback(tExpensivest);
    }
}
