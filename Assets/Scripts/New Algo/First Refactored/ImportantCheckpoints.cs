using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantCheckpoints : MonoBehaviour
{
    [TextArea(10,20)]
    public string loaderLogger;
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

    public void AddTextToLoaderLogger(string x){loaderLogger+=x+"\n";}
}
