using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BattleMethod {
    public string mName;
    abstract public bool isFinish(GameMaster aMaster);
}
abstract public class WithResultBonusMethod : BattleMethod {
    public int mResultBonus = 500;
    public bool mMaxAssets = true;//最大総資産
    public bool mMoveDistance = true;//移動マス数
    public bool mLandNumber = true;//所有地の数
    public bool mFeeAmount = true;//支払い料金
    public bool mDisasterDamageAmount = true;//災害被害額
}

public class LosingMethod : BattleMethod {
    public LosingMethod() {
        mName = "負け抜け";
    }
    public override bool isFinish(GameMaster aMaster) {
        int tTravelerNumber = 0;
        foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
            if (!tTraveler.mIsRetired)
                tTravelerNumber++;
        }
        return tTravelerNumber <= 1;
    }
}
public class AssetsGoalMethod : BattleMethod {
    public int mGoalAmount = 9000;//目標金額
    public AssetsGoalMethod() {
        mName = "総資産目標";
    }
    public override bool isFinish(GameMaster aMaster) {
        foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
            if (tTraveler.mAssets >= mGoalAmount)
                return true;
        }
        return false;
    }
}
public class TurnLimitsMethod : WithResultBonusMethod {
    public int mLimitsTurn = 50;//制限ターン数
    public TurnLimitsMethod() {
        mName = "ターン制限";
    }
    public override bool isFinish(GameMaster aMaster) {
        if (aMaster.mTurnNumber < mLimitsTurn) return false;
        for (int i = aMaster.mTurnIndex + 1; i < aMaster.mTurnOrder.Count; i++) {
            if (aMaster.mTurnOrder[i].mIsRetired)
                continue;
            else
                return false;
        }
        return true;
    }
}
public class LapGoalMethod : WithResultBonusMethod {
    public int mGoalLap = 15;//目標周回数
    public LapGoalMethod() {
        mName = "周回目標";
    }
    public override bool isFinish(GameMaster aMaster) {
        foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
            if (tTraveler.mOrbit >= mGoalLap + 1)
                return true;
        }
        return false;
    }
}
public class BottomConfirmedMethod : WithResultBonusMethod {
    public BottomConfirmedMethod() {
        mName = "ビリ確定";
    }
    public override bool isFinish(GameMaster aMaster) {
        foreach (TravelerStatus tTraveler in aMaster.mTurnOrder) {
            if (tTraveler.mIsRetired)
                return true;
        }
        return false;
    }
}