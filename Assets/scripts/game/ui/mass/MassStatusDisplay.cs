using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class MassStatusDisplay : MyBehaviour {
    [SerializeField] public MassStatusUiButtons mUiButtons;
    abstract public void setStatus(GameMass aMass);
    public void close() {
        this.delete();
    }
    static public MassStatusDisplay create(GameMass aMass, List<MassStatusUiButtonData> aButtonDataList) {
        MassStatusDisplay tDisplay=null;
        switch (aMass) {
            case LandMass tLand:
                tDisplay = GameObject.Instantiate(Resources.Load<MassStatusDisplay>("prefabs/game/ui/massStatus/landMassStatusDisplay"));
                break;
            case EventMass tEvent:
                tDisplay = GameObject.Instantiate(Resources.Load<MassStatusDisplay>("prefabs/game/ui/massStatus/eventMassStatusDisplay"));
                break;
            case CheckPointMass tCheck:
                tDisplay = GameObject.Instantiate(Resources.Load<MassStatusDisplay>("prefabs/game/ui/massStatus/checkMassStatusDisplay"));
                break;
            case StartMass tStart:
                tDisplay = GameObject.Instantiate(Resources.Load<MassStatusDisplay>("prefabs/game/ui/massStatus/startMassStatusDisplay"));
                break;
        }
        tDisplay.setStatus(aMass);
        tDisplay.mUiButtons.setButtons(aButtonDataList);
        return tDisplay;
    }
}
