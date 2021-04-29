using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class JumpMassNumberMass : SpecialMoveMass {
    [SerializeField]
    public bool mRequireRoute;
    public List<SpecialMoveMass> mJumpToMassList;
    abstract public SpecialMoveMass getJumpToMass(TravelerStatus aTraveler, GameMaster aMaster);
}
