using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnOrderAnimator : MyBehaviour {
    public void animate(List<TravelerStatus> aTravelers, List<TravelerStatus> aTurnOrder, Action aCallback) {
        List<BudBox> tBoxes = new List<BudBox>();
        for (int i = 0; i < 4; i++) {
            if (aTravelers[i].mTravelerData.mTravelerCharaData == TravelerCharaData.none) {
                tBoxes.Add(null);
                continue;
            }
            int tOrder = aTurnOrder.IndexOf(aTravelers[i]);
            //箱作成
            BudBox tBox = this.createChild<BudBox>("box" + i.ToString());
            tBox.setColor(BudBox.BoxColor.blue);
            MyBehaviour tOrderImg = tBox.mContents.createChild<MyBehaviour>("order");
            tOrderImg.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/number/ranking/" + (tOrder + 1).ToString());
            tOrderImg.changeLayer(5);
            tOrderImg.scale2D = new Vector2(0.7f, 0.7f);
            tBox.position2D = new Vector2(-4.5f + 3 * i, 1.5f);
            tBox.scale2D = new Vector2(0.5f, 0.5f);
            tBoxes.Add(tBox);
            //切手作成
            FaceStamp tStamp = FaceStamp.create(aTravelers[i].mTravelerData.mTravelerCharaData, 5);
            tStamp.transform.SetParent(this.transform);
            tStamp.position2D = new Vector2(-4.5f + 3 * i, -1.5f);
            tStamp.scale2D = new Vector2(0.4f, 0.4f);
        }
        //アニメーション開始
        this.positionY = 8;
        this.moveTo(new Vector2(0, 0), 1f, () => {
            MyBehaviour.setTimeoutToIns(0.5f, () => { tBoxes[0]?.open(); });
            MyBehaviour.setTimeoutToIns(0.9f, () => { tBoxes[1]?.open(); });
            MyBehaviour.setTimeoutToIns(1.3f, () => { tBoxes[2]?.open(); });
            MyBehaviour.setTimeoutToIns(1.7f, () => { tBoxes[3]?.open(); });
            MyBehaviour.setTimeoutToIns(3.5f, () => {
                this.moveTo(new Vector2(0, -8), 1f, () => {
                    aCallback();
                });
            });
        });
    }
}
