using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RoundManager : MonoBehaviour
{
    public RoundData roundData;
    public Board board;
    public UIManage uiManage;

    private GameState.State _currentGameState = GameState.State.IsInitalizing;
    public GameState.State currentGameState { get { return _currentGameState; } set { _currentGameState = value; } }

    //public bool isWin = false;
    //public bool isDead = false;
    //public bool isGameOver = false;

    // Pls create a object script for it
    //public GameObject popUpWindow;

    void Awake()
    {
        //Debug.Log("I am running!");
        //disable multi touch
        Input.multiTouchEnabled = false;

        roundData = gameObject.GetComponent<RoundData>();
        board = GameObject.Find("Board").GetComponent<Board>();
        uiManage = GameObject.Find("UI Manager").GetComponent<UIManage>();

    }

    void Start()
    {
        roundData.InitializeData();
    }
    void Update()
    {

    }



    public void GameOver(GameState.State status)
    {
        int designatedPopupID;
        switch (status)
        {
            case GameState.State.PlayerLose:
                //Debug.Log($"{Time.time} GG, you lose");
                //Player.OnDefeatedEvent -= GameOver;
                // Shit solutions, the ID will be +1 after calling the function
                designatedPopupID = uiManage.GetPopupCounts() + 1;

                uiManage.SpawnPopup("gameover", 4, () => SettingBtn.StopAdventure(), () => uiManage.currentPopups[designatedPopupID].DestroyPopup(designatedPopupID));
                
                break;

            case GameState.State.PlayerWin:
                //Debug.Log($"{Time.time} GG, you win");
                //RoundData.OnAllMobDefectedEvent -= GameOver;
                // Shit solutions, the ID will be +1 after calling the function
                designatedPopupID = uiManage.GetPopupCounts() + 1;

                uiManage.SpawnPopup("gameover", 5, () => SettingBtn.StopAdventure(), () => uiManage.currentPopups[designatedPopupID].DestroyPopup(designatedPopupID));

                break;
        }
    }
}
