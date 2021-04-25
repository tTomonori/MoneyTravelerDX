using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerAi : CpuAi {
    public override void purchaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に購入する
        aCallback(true);
    }
    public override void increaseLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //所有者がいる土地が8割を超えていてかつ所持金が100を下回らなければ場合増資する
        List<LandMass> tAllLands = aMaster.mFeild.getAllLands();
        List<LandMass> tOwnedLands = aMaster.mFeild.getOtherOwnedLand(null);
        if (tOwnedLands.Count / (float)(tAllLands.Count) > 0.8f && aMyStatus.mMoney - aLand.mIncreaseCost >= 100) {
            aCallback(true);
            return;
        }
        //所持金が所有者がいないマスの購入価格の平均を下回らなければ増資する
        if (aMaster.mFeild.calculatePriceAverage() < aMyStatus.mMoney - aLand.mIncreaseCost) {
            aCallback(true);
            return;
        }
        aCallback(false);
    }
    public override void acquireLand(TravelerStatus aMyStatus, LandMass aLand, GameMaster aMaster, Action<bool> aCallback) {
        //常に買収する
        aCallback(true);
    }
    public override void sellLand(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //1つ売却しただけで破産を回避できる土地の中で最も料金が安い土地を売却する
        LandMass tLand = aMaster.mFeild.searchCheapestFeeOfOverSellCostLand(aMyStatus, -aMyStatus.mMoney);
        if (tLand != null) {
            aCallback(tLand);
            return;
        }
        //最も料金が安い土地を売却する
        tLand = aMaster.mFeild.searchCheapestFeeLand(aMyStatus);
        aCallback(tLand);
    }
    public override void selectPurchase(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //最も料金が高い土地を購入する
        LandMass tTarget = null;
        foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(null)) {
            if (!aMyStatus.canPurchase(tLand)) continue;
            if (tTarget == null) {
                tTarget = tLand;
                continue;
            }
            if (tTarget.mFeeCost < tLand.mFeeCost) {
                tTarget = tLand;
                continue;
            }
        }
        aCallback(tTarget);
    }
    public override void selectIncrease(TravelerStatus aMyStatus, GameMaster aMaster, Action<LandMass> aCallback) {
        //所有者がいる土地が8割を超えているか所持金が所有者がいないマスの購入価格の平均を下回らなければ最も増資コストが安い土地に増資する
        LandMass tTarget = null;
        //最も増資コストが安い土地を求める
        foreach (LandMass tLand in aMaster.mFeild.getOwnedLand(aMyStatus)) {
            if (!aMyStatus.canIncrease(tLand)) continue;
            if (aMyStatus.mMoney < tLand.mIncreaseCost) continue;
            if (tTarget == null) {
                tTarget = tLand;
                continue;
            }
            if (tTarget.mIncreaseCost > tLand.mIncreaseCost) {
                tTarget = tLand;
                continue;
            }
        }
        //所有者がいる土地が8割を超えているか
        List<LandMass> tAllLands = aMaster.mFeild.getAllLands();
        List<LandMass> tOwnedLands = aMaster.mFeild.getOtherOwnedLand(null);
        if (tOwnedLands.Count / (float)(tAllLands.Count) > 0.8f) {
            aCallback(tTarget);
            return;
        }
        //所持金が所有者がいないマスの購入価格の平均を下回らないか
        if (aMaster.mFeild.calculatePriceAverage() < aMyStatus.mMoney - tTarget.mIncreaseCost) {
            aCallback(tTarget);
            return;
        }
        aCallback(null);
    }
}
