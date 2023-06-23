using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SpecialTile : Tile
{
    #region Game object references
    public Image tileSpecialIcon;
    #endregion

    #region Tile data
    public TileEffect tileMainEffect;
    #endregion

    #region Flow
    void Awake()
    {
        roundData = GameObject.Find("Round Manager").GetComponent<RoundData>();
        board = GameObject.Find("Board").GetComponent<Board>();

        //Debug.Log("I am running!");
        canvasGroup = GetComponent<CanvasGroup>();
        //mainCamera = Camera.main;

        tile = this.gameObject;
        tileSpecialIcon = tile.transform.GetChild(2).GetComponent<Image>();
        
        tileLevelText = tile.transform.GetChild(3).GetComponent<TMP_Text>();
        tileEffectIcon = tile.transform.GetChild(4).transform.GetChild(0).GetComponent<Image>();
        tileEffectTurnsText = tile.transform.GetChild(4).transform.GetChild(1).GetComponent<TMP_Text>();

        tileLevelText.gameObject.SetActive(true);
        tileEffectParent.gameObject.SetActive(false);

        tileRect = this.GetComponent<RectTransform>();
        tileLocation = this.transform.position;
        //tileCellPosition = this.transform.GetSiblingIndex();

        //tileLevel = Convert.ToInt32(tileLevelText.text);
        //tileEffectATurn = Convert.ToInt32(tileEffectATurnText.text);
        //tileEffectBTurn = Convert.ToInt32(tileEffectBTurnText.text);

        // Initialise the data of the tile
        //spawnRate = board.specialTileSpawnRate;
        tileLevel = 0;
        //tileSpawnedTurns = 當前;
        tileExistedTurns = 1;
        tileSpawnedTurns = 0;

        // For testing
        //tileEffectATurn = 20;
        //tileEffectBTurn = 20;

        tileLevelText.text = tileLevel.ToString();
        //tileEffectATurnText.text = tileEffectATurn.ToString();
        //tileEffectBTurnText.text = tileEffectBTurn.ToString();

        // Make sure tilePositionRecords is empty

        //score = (int)player.attackPoint;
        baseValueModifier = valueModifierArr[0];

        // Set false for answer cheat here

    }
    #endregion

    #region Tile functions
    public TileEffect GetTileMainEffect(Entity selfEntity, int tileEffectID)
    {


        tileMainEffect = ImportData.tileEffectDictionary[tileEffectID];
        tileMainEffect.OnInit(this, selfEntity, tileSpecialIcon);
        
        return tileMainEffect;
    }
    // For tile merge, receive after onpointerup (AS A DROP RECEIVER) //
    public override void OnDrop(PointerEventData eventData)
    {

        currentState = TileState.State.Processing;
        tileStatusProcessed += 1;

        // They are the same thing
        Tile dragTile = eventData.pointerDrag.GetComponent<Tile>();

        // ****************************************
        roundData.player.SetDragTile(dragTile);
        //player.tile = dragTile;

        // Detect drag tile-> slot
        if (dragTile.inSlot == true)
        {
            
        }
        else
        {
            // Detect drag tile -> other tile
            if (dragTile.inTile == true)
            {


                // DragTile itself is answer
                if (dragTile.isAnswer == true)
                {


                    // Some punishment here
                    roundData.player.TakeDamage(roundData.player.currentMaxHealthValue * 0.15f);
                    roundData.currentPowerScore -= GetOutPutPower() * 2;

                    // Reset new round
                    //Debug.Log("dragTile.interactTile: " + dragTile.interactTile.name);
                    dragTile.interactTile.toBeDestroyed = true;
                    //board.tileCell[dragTile.interactTile.tileCellPosition] = -1;
                    //Debug.Log("dragTile: " + dragTile.name);
                    dragTile.toBeDestroyed = true;

                    // Only setting tile has moved here but not checking afterwards, since they will getting destroyed
                    dragTile.isMoved = true;
                    roundData.player.isActioned = true;

                    //board.tileCell[dragTile.tileCellPosition] = -1;
                    board.UpdateTileCell();
                    //board.DrawAnswer();
                }
                // DragTile is not answer
                else
                {
                    // The tile is answer
                    if (isAnswer == true)
                    {
                        // Some punishment here
                        roundData.player.TakeDamage(roundData.player.currentMaxHealthValue * 0.15f);
                        roundData.currentPowerScore -= GetOutPutPower() * 2;

                        // Reset new round
                        //Debug.Log("dragTile.interactTile: " + dragTile.interactTile.name);
                        dragTile.interactTile.toBeDestroyed = true;
                        //Debug.Log("dragTile: " + dragTile.name);
                        dragTile.toBeDestroyed = true;

                        // Only setting tile has moved here but not checking afterwards, since they will getting destroyed
                        dragTile.isMoved = true;
                        roundData.player.isActioned = true;

                        board.UpdateTileCell();
                        //board.DrawAnswer();
                    }

                    // There should be one new function as MergeTile ***************************************************
                    // the tile is not answer
                    else 
                    {
                        // Check tileLevel
                        if (dragTile.tileLevel == tileLevel)
                        {
                            // Upgrade the tile level that merge into
                            TileUpgrade(dragTile.interactTile);

                            //Debug.Log("dragTile: " + dragTile.name);
                            dragTile.toBeDestroyed = true;

                            // Only setting tile has moved here but not checking afterwards, since they will getting destroyed
                            dragTile.isMoved = true;
                            roundData.player.isActioned = true;

                            // Contribute the score to board
                            roundData.currentPowerScore += GetOutPutPower();

                            //board.tileCell[dragTile.tileCellPosition] = -1;
                            board.UpdateTileCell();
                        }
                        else
                        {
                        }
                    }
                }
            }
        }

    }
    public override void OnBeforeTurnStart()
    {
        base.OnBeforeTurnStart();
        tileMainEffect.OnBeforeTurnStart(this);
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        
        if (currentTileEffect != null)
        {
            currentTileEffect.tileEffectRemainingTurns -= 1;
            tileEffectTurnsText.text = currentTileEffect.tileEffectRemainingTurns.ToString();
        }
    }
    #endregion
}
