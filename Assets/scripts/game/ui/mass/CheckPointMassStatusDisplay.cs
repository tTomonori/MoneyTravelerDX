﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMassStatusDisplay : MassStatusDisplay {
    [SerializeField]
    public TextMesh mBonusMesh;
    public TextMesh mExactlyBonusMesh;
    public override void setStatus(GameMass aMass) {
        CheckPointMass tStart = (CheckPointMass)aMass;
        mBonusMesh.text = tStart.mBonus.ToString();
        mExactlyBonusMesh.text = tStart.mExactlyBonus.ToString();
    }
}
