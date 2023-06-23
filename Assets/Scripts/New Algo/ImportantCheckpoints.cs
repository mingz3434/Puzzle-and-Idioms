using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantCheckpoints : MonoBehaviour
{
    public enum TrueFalse { TRUE,FALSE }
    public TrueFalse loadedIdioms=TrueFalse.FALSE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setLoadedIdioms(){ this.loadedIdioms=TrueFalse.TRUE; }
}
