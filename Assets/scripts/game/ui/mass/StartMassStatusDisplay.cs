using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMassStatusDisplay : MassStatusDisplay {
    [SerializeField]
    public TextMesh mBonusMesh;
    public TextMesh mExactlyBonusMesh;
    public override void setStatus(GameMass aMass) {
        StartMass tStart = (StartMass)aMass;
        mBonusMesh.text = tStart.mBonus.ToString();
        mExactlyBonusMesh.text = tStart.mExactlyBonus.ToString();
    }
}
