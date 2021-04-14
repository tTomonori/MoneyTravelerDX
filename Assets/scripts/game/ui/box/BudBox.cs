using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudBox : MyBehaviour {
    [SerializeField]
    public MyBehaviour mBox;//boxのimageのbehaviour
    public SpriteRenderer mRenderer;//boxのimage
    public MyBehaviour mContents;//中身
    public bool mIsOpened;
    public enum BoxColor {
        blue, red
    }

    static public BudBox create() {
        return GameObject.Instantiate(Resources.Load<BudBox>("prefabs/game/ui/budBox"));
    }
    public void setColor(BoxColor aColor) {
        switch (aColor) {
            case BoxColor.blue:
                mRenderer.sprite = Resources.Load<Sprite>("sprites/game/box/box");
                return;
            case BoxColor.red:
                mRenderer.sprite = Resources.Load<Sprite>("sprites/game/box/eventBox");
                return;
        }
    }
    public void open() {
        if (mIsOpened) return;
        mIsOpened = true;
        mBox.scaleTo(new Vector2(0, 0), 0.3f);
        mContents.scaleTo(new Vector2(1, 1), 0.3f);
        //sound
        MySoundPlayer.playSe("open", false);
        //light
        for (int i = 0; i < 5; i++)
            throwLight();
    }
    public void throwLight() {
        MyBehaviour tLight = MyBehaviour.create<MyBehaviour>();
        tLight.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/particle/light");
        float tScale = Random.Range(0.3f, 0.6f);
        tLight.scale2D = new Vector2(tScale, tScale) * this.scale2D;
        tLight.worldPosition = this.worldPosition - new Vector3(0, 0, 0.05f);
        tLight.changeLayer(5);
        float tMoveDistance = 2 * Random.Range(1, 4);
        float tMoveSpeed = 3 * Random.Range(1, 3);
        Vector2 tDirection = Quaternion.Euler(0, 0, Random.Range(0, 359)) * new Vector2(tMoveDistance, 0);
        tLight.moveByWithSpeed(tDirection, tMoveSpeed, () => {
            tLight.delete();
        });
    }
}
