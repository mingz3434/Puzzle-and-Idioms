using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum TeammateType
{
    // Normally, I would set enum as 3-digit system as to leave space for future expension.
    // But this time, I want it to relate the order of dictionary from player's teammates
    TeammateLeader = 0,
    TeammateTwo = 1,
    TeammateThree = 2,
    TeammateFour = 3,
    TeammateFriend = 4
}

public class Teammate : Entity, IPointerDownHandler, IDropHandler
{
    #region Scripts
    #endregion

    #region Game object references
    //public GameObject teammateBox;
    public Vector2 position;
    public Image teammatePic;
    public TMP_Text currentActiveSkillCDText;
    public TMP_Text outputValueText;
    // Temp, for ability
    public Image frame;
    #endregion

    #region Teammate data
    [SerializeField] private int _teammateID;
    public int teammateID { get { return _teammateID; } set { _teammateID = value; } }
    [SerializeField] private string _teammateName;
    public string teammateName { get { return _teammateName; } set { _teammateName = value; } }
    [SerializeField] private string _picName;
    public string picName { get { return _picName; } set { _picName = value; } }
    [SerializeField] private float _currentTotalAttackValue;
    public float currentTotalAttackValue { get { return _currentTotalAttackValue; } set { _currentTotalAttackValue = value; } }

    public float lerpSpeed;
    //public int weaponType;
    //public Image weaponTypeIcon;
    public float outputValue;
    #endregion

    #region Flow
    void Awake()
    {

        // Not ideal place
        player = GameObject.Find("Round Manager").GetComponent<Player>();
        board = GameObject.Find("Board").GetComponent<Board>();
        roundData = GameObject.Find("Round Manager").GetComponent<RoundData>();
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
        uiManage = GameObject.Find("UI Manager").GetComponent<UIManage>();
    }

    void Update()
    {
        if (currentActiveAbilityCD <= 0 && (roundManager.currentGameState == GameState.State.IsFlying || roundManager.currentGameState == GameState.State.IsBattling))
        {
            frame.gameObject.SetActive(true);
            // Temp
            //currentTotalAttackValue += (attackPoint.value * 10f);
            if (currentMaxActiveAbilityCD.GetStatValue() <= 1)
            {
                currentMaxActiveAbilityCD.SetStatValue(1);
            }

            currentActiveAbilityCD = 0;
        }
        else
        {
            frame.gameObject.SetActive(false);
        }

        if (currentActiveAbilityCD > currentMaxActiveAbilityCD.GetStatValue())
        {
            currentActiveAbilityCD = (int)currentMaxActiveAbilityCD.GetStatValue();
        }
        
        currentActiveSkillCDText.text = currentActiveAbilityCD.ToString();
        UpdateOutputValue(currentTotalAttackValue, 0);
    }


    #endregion

    #region Teammate functions
    public void UpdateOutputValue(float newValue, int decimalplace)
    {
        lerpSpeed = 3f * Time.deltaTime;
        outputValue = Mathf.Lerp(outputValue, newValue, lerpSpeed);

        // Processing bar text
        outputValueText.text = outputValue.ToString("F" + decimalplace);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentTotalAttackValue > 0)
        {
            player.Attack(roundData.currentMob, currentTotalAttackValue);
            currentTotalAttackValue = 0;
            player.isActioned = true;
        }
        else if (currentActiveAbilityCD <= 0 && roundData.currentPowerScore >= activeAbility.currentAbilityCost)
        {
            activeAbility.OnTrigger(this);
            currentActiveAbilityCD = (int)currentMaxActiveAbilityCD.GetStatValue();
            player.isActioned = true;
        }        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Tile dragTile = eventData.pointerDrag.GetComponent<Tile>();

        player.Answer(this);

    }

    public void AnsweredCorrectly(Tile tile)
    {
        Debug.Log($"{Time.time} Teammate.AnswerCorrectly (start)");

        Debug.Log($"{Time.time} ^5.5A teammate get the correct answer!");

        // Temp
        //Heal(maxHp.value * tile.interactTeammate.defencePoint);
        if (player.currentHealthValue < player.currentMaxHealthValue)//(currentHp.GetStatValue() < maxHp.GetStatValue())
        {
            player.Heal(player.currentMaxHealthValue * 0.15f);
            //Debug.Log($"$healed hp = {maxHp.value * 0.15f}");
        }

        currentActiveAbilityCD -= 1;
        
        if (roundManager.currentGameState == GameState.State.IsFlying)
        {
            roundData.currentDistance += (int)roundData.player.currentSpeedValue;

            float controlNum = (roundData.roundMobNumber - roundData.currentSpawnedMobNumber) / ((roundData.roundDistance - roundData.currentDistance) / roundData.player.currentSpeedValue);
            //Debug.Log($"&controlNum = {controlNum}");
            float randomNum = UnityEngine.Random.Range(0, 100) / 100f;
            //Debug.Log($"&randomNum = {randomNum}");

            if (randomNum < controlNum)
            {
                roundData.flyToBattle = true;
                //roundManager.currentGameState = GameState.State.IsBattling;
            }
        }
        else if (roundManager.currentGameState == GameState.State.IsBattling)
        {
            // Consider to use tile.currentvalueModifier later
            currentTotalAttackValue += (roundData.player.GetAttackPoint() * GetAttackValue() * (tile.tileLevel + 1));
        }
        
        // Stop the wrong count combo
        roundData.wrongCountCombo = 0;

        // Contribute the score to board
        roundData.currentPowerScore += tile.GetOutPutPower() * 3;

        // Destroy the tile
        tile.toBeDestroyed = true;
        board.UpdateTileCell();
        tile.DestroyTile(tile);

        // Force next move/ round
        player.isActioned = true;
    }

    public void AnsweredWrongly(Tile tile)
    {

        if (roundManager.currentGameState == GameState.State.IsFlying)
        {
            // Some punishment here
            roundData.currentDistance -= (int)roundData.player.currentSpeedValue / 2;

        }


        // Some punishment here
        player.TakeDamage(player.currentMaxHealthValue * 0.15f);
        roundData.currentPowerScore -= tile.GetOutPutPower() * 2;

        // Record the count of wrong submittion
        roundData.wrongCountCombo += 1;
        roundData.totalWrongCount += 1;

        // Destroy the tile
        tile.toBeDestroyed = true;
        board.UpdateTileCell();
        tile.DestroyTile(tile);

        if (roundData.wrongCountCombo >= 5)
        {
            // Do some visual shit/popup/conversation
            string popupMessage = $"{roundData.player.entityName}答錯太多次啦! 正確答案係: {roundData.currentAnswerWord}";
            uiManage.SpawnPopup(popupMessage, "emergency");

            board.answerTile.transform.GetChild(0).GetComponent<Image>().color = new Color32(160, 60, 60, 255);
            board.answerTile.transform.GetChild(2).GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);

            roundData.wrongCountCombo = 0;

            board.answerTile.toBeDestroyed = true;
            board.UpdateTileCell();

            // Destroy the tile
            //board.answerTile.DestroyTile(board.answerTile);

            this.Wait(roundData.currentTurnDuration, () => player.isActioned = true);
        }
        else
        {
            // Force next move/ round
            player.isActioned = true;
        }

    }

    public void AnsweredSpecialTile(Tile tile)
    {

        if (roundManager.currentGameState == GameState.State.IsFlying)
        {
            // ?
        }
        else if (roundManager.currentGameState == GameState.State.IsBattling)
        {
            // ?
        }

        // Force next move/ round
        player.isActioned = true;

        // Destroy the tile
        tile.toBeDestroyed = true;
        board.UpdateTileCell();
        tile.DestroyTile(tile);
    }
    #endregion
}

