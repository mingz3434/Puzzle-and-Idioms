using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text=TMPro.TMP_Text;

[AddComponentMenu("_My Library/UI Binder & Data")]
public class UI : MonoBehaviour
{
    //! Data are just for inspection, they are read-only!

    [Header("[Binder] Top Bar : Scoreboard")]
    public Text idiomId,turnState,gameState;
    public Button cheatButton;
    public Text turnCount;

    [Header("[Binder] Top Bar : Question Box")]
    public string def;

    [Header("[Binder] Top Bar : Timer")]
    public string ghi;

    [Header("[Binder] Center : 9 Pic & Animation")]
    public string jkl;

    [Header("[Binder] Main : The Team")]
    public string mno;

    [Header("[Binder] Main : Cardboard")]
    public string pqr;

    [Header("~~~~~分隔線係我~~~~~")]
    public string 分隔線="大家好，我係山並。";

    [Header("[Data] Top Bar : Scoreboard")]
    public int scoreboard;

    [Header("[Data] Top Bar : Question Box")]
    public int questionBox;

    [Header("[Data] Top Bar : Timer")]
    public int timer;

    [Header("[Data] Center : 9 Pic & Animation")]
    public int pic9;

    [Header("[Data] Main : The Team")]
    public int theTeam;

    [Header("[Data] Main : Cardboard")]
    public int cardboard;

    //P: Setters here are public api to be used, will be included here, don't worry.
}

