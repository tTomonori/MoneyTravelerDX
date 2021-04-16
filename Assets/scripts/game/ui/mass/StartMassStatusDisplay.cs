﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMassStatusDisplay : MassStatusDisplay {
    [SerializeField]
    public TextMesh mBonusMesh;
    public TextMesh mExactlyBonusMesh;
    public override void setStatus(GameMass aMass) {
        StartMass tStart = (StartMass)aMass;
        mBonusMesh.text = tStart.mPassBonus.ToString();
        mExactlyBonusMesh.text = (tStart.mStopBonus - tStart.mPassBonus).ToString();
    }
}
