using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BattleMethod {
    public string mName;
}
abstract public class WithResultBonusMethod : BattleMethod {
    public int mResultBonus = 500;
    public bool mMaxAssets = true;//最大総資産
    public bool mMoveDistance = true;//移動マス数
    public bool mLandNumber = true;//土地の数
    public bool mFeeAmount = true;//支払い料金
    public bool mDisasterDamageAmount = true;//災害被害額
}

public class LosingMethod : BattleMethod {
    public LosingMethod() {
        mName = "負け抜け";
    }
}
public class AssetsGoalMethod : BattleMethod {
    public int mGoalAmount = 9000;//目標金額
    public AssetsGoalMethod() {
        mName = "総資産目標";
    }
}
public class TurnLimitsMethod : WithResultBonusMethod {
    public int mLimitsTurn = 50;//制限ターン数
    public TurnLimitsMethod() {
        mName = "ターン制限";
    }
}
public class LapGoalMethod : WithResultBonusMethod {
    public int mGoalLap = 15;//目標周回数
    public LapGoalMethod() {
        mName = "周回目標";
    }
}
public class BottomConfirmedMethod : WithResultBonusMethod {
    public BottomConfirmedMethod() {
        mName = "ビリ確定";
    }
}