using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameFeild : MyBehaviour {
    static public readonly float mHorizontalMargin = 15;
    static public readonly float mAdditionalSouthMargin = 15;
    static public readonly float mCeilingMargin = 15;
    static public readonly float mFloorMargin = 0.1f;
    [SerializeField]
    public List<GameMass> mMassList;
    public MyBehaviour mMassContainer;
    public MyBehaviour mRouteContainer;
    public MyBehaviour mComaContainer;
    public MyBehaviour mWallContainer;
    public float mNorth;
    public float mEast;
    public float mSouth;
    public float mWest;
    public float mFloor;
    public float mCeiling;

    //inspector用
    private void createContainer() {
        if (mMassContainer == null)
            mMassContainer = this.createChild<MyBehaviour>("massContainer");
        if (mRouteContainer == null)
            mRouteContainer = this.createChild<MyBehaviour>("routeContainer");
        if (mComaContainer == null)
            mComaContainer = this.createChild<MyBehaviour>("comaContainer");
        if (mWallContainer == null)
            mWallContainer = this.createChild<MyBehaviour>("wallContainer");
    }
    private void createRoute() {
        createContainer();
        //ルートを削除
        foreach (Transform tRoute in mRouteContainer.GetComponentsInChildrenWithoutSelf<Transform>())
            EditorApplication.delayCall += () => DestroyImmediate(tRoute.gameObject);
        mMassList = new List<GameMass>();
        //マスをリストに記録
        mMassList.AddRange(mMassContainer.GetComponentsInChildren<GameMass>());
        //ルート作成
        for (int i = 0; i < mMassList.Count; i++) {
            int tIndex1 = i;
            int tIndex2 = (i + 1 == mMassList.Count) ? 0 : i + 1;
            if (!mustCreateRout(tIndex1, tIndex2)) continue;
            createRoute(mMassList[tIndex1].getNotShared(), mMassList[tIndex2].getNotShared()).transform.SetParent(mRouteContainer.transform, true);
        }
        //jumpMassのルート
        for(int i = 0; i < mMassList.Count; i++) {
            if(mMassList[i] is JumpMassNumberMass && ((JumpMassNumberMass)mMassList[i]).mRequireRoute) {
                foreach(GameMass tMass in ((JumpMassNumberMass)mMassList[i]).mJumpToMassList) {
                    createRoute(mMassList[i], tMass).transform.SetParent(mRouteContainer.transform, true);
                }
            }
        }
    }
    private bool mustCreateRout(int aIndex1, int aIndex2) {
        if (mMassList[aIndex1].getNotShared() is SpecialMoveMass && mMassList[aIndex2].getNotShared() is SpecialMoveMass)
            return false;
        if (!(mMassList[aIndex1] is ShareMass) && !(mMassList[aIndex2] is ShareMass))
            return true;
        for (int i = 0; i < mMassList.Count; i++) {
            if (i == aIndex1) continue;
            int tIndex1 = i;
            int tIndex2 = (i + 1 == mMassList.Count) ? 0 : i + 1;
            if ((mMassList[aIndex1].getNotShared() == mMassList[tIndex1].getNotShared() && mMassList[aIndex2].getNotShared() == mMassList[tIndex2].getNotShared()) ||
                (mMassList[aIndex1].getNotShared() == mMassList[tIndex2].getNotShared() && mMassList[aIndex2].getNotShared() == mMassList[tIndex1].getNotShared())) {
                if (aIndex1 > tIndex1) return false;
            }
        }
        return true;
    }
    //2つのマスを結ぶ線を生成
    static public MyBehaviour createRoute(GameMass aMass1, GameMass aMass2) {
        MyBehaviour tRoute = MyBehaviour.create<MyBehaviour>();
        SpriteRenderer tRenderer = tRoute.gameObject.AddComponent<SpriteRenderer>();
        tRoute.name = "route";
        tRenderer.sprite = Resources.Load<Sprite>("sprites/feild/mass/tile/route");
        tRenderer.material = Resources.Load<Material>("materials/My_Translucent");
        tRenderer.color = new Color(0.9f, 0.9f, 0.9f, 0.8f);
        tRoute.position = (aMass1.worldPosition + aMass2.worldPosition) / 2f;
        tRoute.positionY -= 0.05f;
        tRoute.scale = new Vector3(Vector3.Distance(aMass1.worldPosition, aMass2.worldPosition) / 2f, 1, 1);
        float tRotate1 = VectorCalculator.corner(new Vector2(1, 0), new Vector2(Vector2.Distance(new Vector2(aMass1.worldPosition.x, aMass1.worldPosition.z), new Vector2(aMass2.worldPosition.x, aMass2.worldPosition.z)), aMass1.positionY - aMass2.positionY)); ;
        float tRotate2 = VectorCalculator.corner(new Vector2(aMass1.positionX - aMass2.positionX, aMass1.positionZ - aMass2.positionZ), new Vector2(1, 0)); ;
        tRoute.transform.Rotate(90, 0, 0, Space.World);
        tRoute.transform.Rotate(0, 0, tRotate1, Space.World);
        tRoute.transform.Rotate(0, tRotate2, 0, Space.World);
        return tRoute;
    }
    //マスの位置からフィールドのサイズを計算
    private void calculateSize() {
        if (mMassList.Count == 0) return;
        float tNorth = mMassList[0].worldPosition.z;
        float tEast = mMassList[0].worldPosition.x;
        float tSouth = mMassList[0].worldPosition.z;
        float tWest = mMassList[0].worldPosition.x;
        float tFloor = mMassList[0].worldPosition.y;
        float tCeiling = mMassList[0].worldPosition.y;
        foreach (GameMass tMass in mMassList) {
            if (tNorth < tMass.worldPosition.z) tNorth = tMass.worldPosition.z;
            if (tEast < tMass.worldPosition.x) tEast = tMass.worldPosition.x;
            if (tSouth > tMass.worldPosition.z) tSouth = tMass.worldPosition.z;
            if (tWest > tMass.worldPosition.x) tWest = tMass.worldPosition.x;
            if (tFloor > tMass.worldPosition.y) tFloor = tMass.worldPosition.y;
            if (tCeiling < tMass.worldPosition.y) tCeiling = tMass.worldPosition.y;
        }
        mNorth = tNorth + mHorizontalMargin;
        mEast = tEast + mHorizontalMargin;
        mSouth = tSouth - mHorizontalMargin - mAdditionalSouthMargin;
        mWest = tWest - mHorizontalMargin;
        mFloor = tFloor;
        mCeiling = tCeiling + mCeilingMargin;
    }
    //床と壁の位置を設定
    private void setWall() {
        MyBehaviour tWall;
        //北の壁
        tWall = mWallContainer.findChild<MyBehaviour>("northWall");
        if (tWall != null) {
            tWall.rotate = new Vector3(0, 0, 0);
            tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(mEast - mWest, mCeiling - mFloor + mFloorMargin);
            tWall.position = new Vector3((mEast + mWest) / 2f, (mCeiling + mFloor - mFloorMargin) / 2f, mNorth);
        }
        //東の壁
        tWall = mWallContainer.findChild<MyBehaviour>("eastWall");
        if (tWall != null) {
            tWall.rotate = new Vector3(0, 90, 0);
            tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(mNorth - mSouth, mCeiling - mFloor + mFloorMargin);
            tWall.position = new Vector3(mEast, (mCeiling + mFloor - mFloorMargin) / 2f, (mNorth + mSouth) / 2f);
        }
        //西の壁
        tWall = mWallContainer.findChild<MyBehaviour>("westWall");
        if (tWall != null) {
            tWall.rotate = new Vector3(0, -90, 0);
            tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(mNorth - mSouth, mCeiling - mFloor + mFloorMargin);
            tWall.position = new Vector3(mWest, (mCeiling + mFloor - mFloorMargin) / 2f, (mNorth + mSouth) / 2f);
        }
        //床
        tWall = mWallContainer.findChild<MyBehaviour>("floor");
        if (tWall != null) {
            tWall.rotate = new Vector3(90, 0, 0);
            tWall.GetComponent<RectTransform>().sizeDelta = new Vector2(mEast - mWest, mNorth - mSouth);
            tWall.position = new Vector3((mEast + mWest) / 2f, mFloor - mFloorMargin, (mNorth + mSouth) / 2f);
        }
    }
}
[CustomEditor(typeof(GameFeild))]
public class GameFeildEditor : Editor {

    public override void OnInspectorGUI() {
        // 元のインスペクター部分を表示
        base.OnInspectorGUI();
        GameFeild tFeild = target as GameFeild;

        if (GUILayout.Button("createContainer")) {
            tFeild.SendMessage("createContainer", null, SendMessageOptions.DontRequireReceiver);
        }
        if (GUILayout.Button("createRoute")) {
            tFeild.SendMessage("createRoute", null, SendMessageOptions.DontRequireReceiver);
        }
        if (GUILayout.Button("calculateSize")) {
            tFeild.SendMessage("calculateSize", null, SendMessageOptions.DontRequireReceiver);
        }
        if (GUILayout.Button("setWall")) {
            tFeild.SendMessage("setWall", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}