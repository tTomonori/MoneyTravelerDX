using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiSystem : MyBehaviour {
    public float mTop;
    public float mBottom;
    public float mLeft;
    public float mRight;
    public float mSpeed = -1;
    public float mCreateParSecond;
    public bool _IsCreating;
    public bool mIsCreating {
        get { return _IsCreating; }
        set { _IsCreating = value; mElapsedTime = 0; }
    }
    private float mElapsedTime;
    private void Update() {
        if (!mIsCreating) return;
        mElapsedTime += Time.deltaTime;
        if (mElapsedTime < 1f / mCreateParSecond) return;
        for (int i = 0; i < mElapsedTime / mCreateParSecond; i++) {
            createConfettie();
        }
        mElapsedTime = mElapsedTime % (1f / mCreateParSecond);
    }
    public Confetti createConfettie() {
        Confetti tConfetti = this.createChild<Confetti>("confetti");
        if (mSpeed >= 0)
            tConfetti.mSpeed = mSpeed;
        tConfetti.mMaxMovedDistance = mTop - mBottom;
        tConfetti.position2D = new Vector2(UnityEngine.Random.Range(mLeft, mRight), mTop);
        return tConfetti;
    }
}
