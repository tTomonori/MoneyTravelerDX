using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceManager : MyBehaviour {
    static public int[] mDice1Numbers { get { return new int[3] { 1, 3, 5 }; } }
    static public int[] mDice2Numbers { get { return new int[4] { 0, 2, 4, 6 }; } }
    static public int[] mDice3Numbers { get { return new int[2] { 0, 1 }; } }
    [SerializeField] private MyBehaviour mContainer;
    [SerializeField] private BudBox mBox1;
    [SerializeField] private BudBox mBox2;
    [SerializeField] private BudBox mBox3;
    [SerializeField] private TextMesh mNumberMesh1;
    [SerializeField] private TextMesh mNumberMesh2;
    [SerializeField] private TextMesh mNumberMesh3;
    [NonSerialized] public bool mIsAllOpened = false;
    private Action<int> mEndCallback;
    static public void setDice(GameObject aParent, Action<DiceManager> aPrepared, Action<int> aEnd) {
        DiceManager tManager = GameObject.Instantiate(Resources.Load<DiceManager>("prefabs/game/ui/diceManager"));
        tManager.name = "diceManager";
        tManager.transform.SetParent(aParent.transform, false);
        tManager.mEndCallback = aEnd;
        tManager.animate(() => {
            aPrepared(tManager);
        });
    }
    private void animate(Action aCallback) {
        mContainer.position2D = new Vector2(0, 7);
        mContainer.moveTo(new Vector2(0, 0.5f), 0.5f, aCallback);
    }
    public void open() {
        if (!mBox1.mIsOpened) {
            open1();
            return;
        }
        if (!mBox2.mIsOpened) {
            open2();
            return;
        }
        if (!mBox3.mIsOpened) {
            open3();
            return;
        }
    }
    public void open1() {
        if (mBox1.mIsOpened) {
            open();
            return;
        }
        int[] tNumbers = mDice1Numbers;
        mNumberMesh1.text = tNumbers[UnityEngine.Random.Range(0, tNumbers.Length)].ToString();
        mBox1.open();
        checkAllOpened();
    }
    public void open2() {
        if (mBox2.mIsOpened) {
            open();
            return;
        }
        int[] tNumbers = mDice2Numbers;
        mNumberMesh2.text = tNumbers[UnityEngine.Random.Range(0, tNumbers.Length)].ToString();
        mBox2.open();
        checkAllOpened();
    }
    public void open3() {
        if (mBox3.mIsOpened) {
            open();
            return;
        }
        int[] tNumbers = mDice3Numbers;
        mNumberMesh3.text = tNumbers[UnityEngine.Random.Range(0, tNumbers.Length)].ToString();
        mBox3.open();
        checkAllOpened();
    }
    //全てのboxを開けたかチェック
    private void checkAllOpened() {
        if (!(mBox1.mIsOpened && mBox2.mIsOpened && mBox3.mIsOpened)) return;
        mIsAllOpened = true;
        MyBehaviour.setTimeoutToIns(1, () => {
            mEndCallback(int.Parse(mNumberMesh1.text) + int.Parse(mNumberMesh2.text) + int.Parse(mNumberMesh3.text));
        });
    }
    public void hide() {
        mContainer.gameObject.SetActive(false);
    }
    public void show() {
        mContainer.gameObject.SetActive(true);
    }
}
