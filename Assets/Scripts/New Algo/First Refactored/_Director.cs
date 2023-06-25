using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


public class _Director : MonoBehaviour
{
    //give me title of unity editor
    //how?
    public IntegratedStates z;
    public UI u1,u2,u3,u4;
    public CoroutineEvents c;
    public ImportantCheckpoints i;

    void Start() {
        Observable.Timer(TimeSpan.FromSeconds(2))
            .Subscribe(_ => {
                if (!((UI_Top)u1).idiomId) { Debug.LogError("(MyMsg) UI_Top@dir: #idiomId has not bound yet!"); } else {i.AddTextToLoaderLogger("#idiomId has bound.");}
                if (!((UI_Top)u1).turnState) { Debug.LogError("(MyMsg) UI_Top@dir: #turnState has not bound yet!"); } else {i.AddTextToLoaderLogger("#turnState has bound.");}
                if (!((UI_Top)u1).gameState) { Debug.LogError("(MyMsg) UI_Top@dir: #gameState has not bound yet!"); } else {i.AddTextToLoaderLogger("#gameState has bound.");}
                if (!((UI_Top)u1).cheatButton) { Debug.LogError("(MyMsg) UI_Top@dir: #cheatButton has not bound yet!"); } else {i.AddTextToLoaderLogger("#cheatButton has bound.");}
                if (!((UI_Top)u1).turnCount) { Debug.LogError("(MyMsg) UI_Top@dir: #turnCount has not bound yet!"); } else {i.AddTextToLoaderLogger("#turnCount has bound.");}
            })                
            .AddTo(this);

        Observable.Timer(TimeSpan.FromSeconds(2))
            .Subscribe(_ => {
                if (!((UI_Top)u1).char1) { Debug.LogError("(MyMsg) UI_Top@dir: #char1 has not bound yet!"); } else {i.AddTextToLoaderLogger("#char1 has bound.");}
                if (!((UI_Top)u1).char2) { Debug.LogError("(MyMsg) UI_Top@dir: #char2 has not bound yet!"); } else {i.AddTextToLoaderLogger("#char2 has bound.");}
                if (!((UI_Top)u1).char3) { Debug.LogError("(MyMsg) UI_Top@dir: #char3 has not bound yet!"); } else {i.AddTextToLoaderLogger("#char3 has bound.");}
                if (!((UI_Top)u1).char4) { Debug.LogError("(MyMsg) UI_Top@dir: #char4 has not bound yet!"); } else {i.AddTextToLoaderLogger("#char4 has bound.");}
                if (!((UI_Top)u1).missingTile) { Debug.LogError("(MyMsg) UI_Top@dir: #missingTile has not bound yet!"); } else {i.AddTextToLoaderLogger("#missingTile has bound.");}
            })                
            .AddTo(this);

    
    }
}

