using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareMass : GameMass {
    [SerializeField]
    public GameMass mSharedMass;
    public override GameMass getNotShared() {
        return mSharedMass;
    }
    private void OnValidate() {
        if (mSharedMass == null) return;
        this.position = mSharedMass.position;
    }
}
