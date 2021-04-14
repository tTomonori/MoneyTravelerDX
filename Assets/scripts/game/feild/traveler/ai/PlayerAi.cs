using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAi : TravelerAi {
    public override void rollDice(DiceManager aDiceManager) {
        Subject.addObserver(new Observer("playerAiRollDice", (aMessage) => {
            switch (aMessage.name) {
                case "diceBackPushed":
                    aDiceManager.open();
                    return;
                case "dice1Pushed":
                    aDiceManager.open1();
                    return;
                case "dice2Pushed":
                    aDiceManager.open2();
                    return;
                case "dice3Pushed":
                    aDiceManager.open3();
                    return;
            }
        }));
    }
    public override void endRollDice() {
        Subject.removeObserver("playerAiRollDice");
    }
}
