using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnEnvelope : MyBehaviour {
    [SerializeField]
    public SpriteRenderer mCharaImage;
    public TextMesh mCharaENameMesh;
    public SpriteRenderer mMarkRenderer;
    public MyBehaviour mObjects;
    public MyBehaviour mMark;
    static public TurnEnvelope create(TravelerStatus aTraveler) {
        TurnEnvelope tEnvelope = GameObject.Instantiate(Resources.Load<TurnEnvelope>("prefabs/game/ui/turnEnvelope"));
        tEnvelope.set(aTraveler);
        tEnvelope.changeLayer(5, true);
        return tEnvelope;
    }
    public void set(TravelerStatus aTraveler) {
        mCharaImage.sprite = aTraveler.mTravelerData.mTravelerCharaData.getImage();
        mCharaENameMesh.text = aTraveler.mTravelerData.mTravelerCharaData.mEName;
        mMarkRenderer.color = aTraveler.playerColor;
    }
    public void animate(Action aCallback) {
        mMark.scale2D = Vector2.zero;
        this.position2D = new Vector2(-13, 8);
        this.rotate = Vector3.zero;
        this.rotateZBy(-30, 0.5f, () => {
            mMark.rotateZBy(360, 0.3f);
            mMark.scale2D = new Vector2(5, 5);
            mMark.scaleTo(new Vector2(1, 1), 0.3f, () => {
                mObjects.scaleTo(new Vector2(0.9f, 0.9f), 0.1f, () => {
                    mObjects.scaleTo(new Vector2(1f, 1f), 0.1f, () => {
                        MyBehaviour.setTimeoutToIns(0.5f, () => {
                            this.rotateZBy(30, 0.5f, aCallback);
                        });
                    });
                });
            });
        });
    }
}
