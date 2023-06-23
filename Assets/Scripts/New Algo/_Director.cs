using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class _Director : MonoBehaviour
{
    //give me title of unity editor
    //how?
    public IntegratedStates z;
    public UI u;
    public CoroutineEvents c;
    public ImportantCheckpoints i;

    void Start() {
        Observable.Range(1, 10)
            .Where(x => x % 2 == 0)
            .Select(x => x * 2)
            .Subscribe(x => Debug.Log("hi"+x));
    }
}

