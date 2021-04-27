using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMassStatusDisplay : MassStatusDisplay {
    [SerializeField]
    public TextMesh mEventTypeMesh;
    public TextMesh mRunTypeMesh;
    public TextMesh mDescription1Mesh;
    public TextMesh mDescription2Mesh;
    public override void setStatus(GameMass aMass) {
        EventMass tEvent = (EventMass)aMass;
        mEventTypeMesh.text = tEvent.mEventType.getName();
        mRunTypeMesh.text = tEvent.mRunType.getName();
        switch (tEvent.mEventType) {
            case EventMassType.heart:
                mDescription1Mesh.text = "なにかいいことが起こるかも?";
                mDescription2Mesh.text = "止まれたらラッキー!";
                break;
            case EventMassType.bat:
                mDescription1Mesh.text = "わるいことが起こりそうな予感・・・";
                mDescription2Mesh.text = "止まらないように気をつけよう";
                break;
            case EventMassType.magic:
                mDescription1Mesh.text = "いろんなことが起こりそう";
                mDescription2Mesh.text = "止まってからのお楽しみ";
                break;
            case EventMassType.disaster:
                mDescription1Mesh.text = "災害イベントが発生!?";
                mDescription2Mesh.text = "吉と出るか凶と出るか・・・";
                break;
        }
    }
}
