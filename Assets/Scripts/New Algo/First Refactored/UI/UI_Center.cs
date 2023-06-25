using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text=TMPro.TMP_Text;
using UniRx;
using System;

[AddComponentMenu("_My Library/UI Binder & Data (Center)")]
public class UI_Center : UI
{
    //! Data are just for inspection, they are read-only!

    [Header("[Binder] Center : Mob")]
    public Image mob;

    [Header("[Binder] Center : Team")]
    public GameObject teammate1;
    public GameObject teammate2;
    public GameObject teammate3;
    public GameObject teammate4;
    public GameObject teammate5;

    //! =====================================================================
    [Header("~~~~~分隔線係我~~~~~")]
    public string 分隔線="大家好，我係山並。";

    [Header("[Data] Center : Mob")]
    public string mobName;

    public void UpdateTestBoard(){
      //   if(idiomId && turnState && gameState && cheatButton && turnCount){
      //       currentIdiomId=Convert.ToInt32(idiomId.text);
      //       currentTurnState=(IntegratedStates.TurnState) Enum.Parse(typeof(IntegratedStates.TurnState),turnState.text);
      //       currentGameState=(IntegratedStates.GameState) Enum.Parse(typeof(IntegratedStates.GameState),gameState.text);
      //       currentTurnCount=Convert.ToInt32(turnCount.text);
      //   }
    }
    
    //P: Setters here are public api to be used, will be included here, don't worry.






    void Start(){
        Observable.Timer(TimeSpan.FromSeconds(3))
            .Subscribe(_ => {
                UpdateTestBoard();
            });
    }
}




    // [Header("[Binder] Center : 9 Pic & Animation")] // public string jkl; // [Header("[Binder] Main : The Team")] // public string mno; // [Header("[Binder] Main : Cardboard")] // public string pqr;
    // [Header("[Data] Center : 9 Pic & Animation")] // public int pic9; // [Header("[Data] Main : The Team")] // public int theTeam; // [Header("[Data] Main : Cardboard")] // public int cardboard;