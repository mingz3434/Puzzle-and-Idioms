using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tile : Affectable, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler, IEndDragHandler, IDropHandler
{
    #region Scripts
    public Board board;
    public RoundData roundData;
    #endregion

    #region Game object references
    // Blank tile without anythiung shown in the canvas, need to be initialize later
    public GameObject tile;
    public TMP_Text tileLevelText;
    public GameObject tileEffectParent;
    public Image tileEffectIcon;
    public TMP_Text tileEffectTurnsText;

    // Controlling the tile transparent
    public CanvasGroup canvasGroup;
    // Controlling the tile rect transform shit
    public RectTransform tileRect;
    #endregion

    #region Tile info
    // Spawning chance, >0 ~ <100?
    public int spawnRate;
    // "normal", "special", "teammate"
    public string tileType;
    // (x,y)
    public Vector2 tileLocation;
    // 0-24 in the answer board
    public int tileCellPosition;
    // For storing tile cell position
    public int tileCellPositionAtStart;
    // 0-?
    public int tileLevel;
    // Name of the element
    public string tileElementType;
    // (r, b, g, a), 0-255
    public Color32 tileColor;

    // Tile effects
    public TileEffect currentTileEffect;


    public TileState.State currentState = TileState.State.IsInitalizing;
    public int tileStatusProcessed = 0;
    #endregion

    #region Tile records
    // Which turn the tile spawned, 0-?
    public int tileSpawnedTurns;
    // Total existed turn for the tile, 1-?
    public int tileExistedTurns;
    // Total moved turn for the tile, 0-?
    public int tileMovedTurns;
    // Stores this tile moving record
    public int[] tilePositionRecords;
    // Output value
    public int outputValue;
    // Base score **modifier**
    public float baseValueModifier;
    // Adjusted current score
    public float currentvalueModifier;
    // For managing score tile bonus based on bonus, testing only
    public float[] valueModifierArr = { 0, 0.05f, 0.1125f, 0.2531f, 0.5695f, 1.2814f, 2.8833f, 6.4873f, 14.5965f, 32.8420f, 73.8946f, 166.2628f, 374.0914f, 841.7056f, 1893.8376f, 4261.1346f, 9587.5530f};
    // Board score related
    public int outputPower;
    public int[] tileLevelScoreArr = { 0, 2000, 5000, 9000, 14000, 20000, 27000, 34000, 43000, 53000};
    #endregion

    #region Tile booleans
    // For detecting answer
    public bool isAnswer;
    // Checking if the tile moved
    public bool isMoved;
    // For those methods of the tile, change to state and will be deleted
    public bool isSelect;
    public bool isDrag;
    public bool toBeDestroyed;
    public bool isControllable;
    #endregion

    #region Tile interaction
    // May need to change/delete
    public bool inSlot;
    public Teammate interactTeammate;
    public Vector2 interactSlotLocation;
    public bool inTile;
    public Tile interactTile;
    public Vector2 interactTileLocation;
    #endregion

    #region Flow
    void Awake() 
    {
        roundData = GameObject.Find("Round Manager").GetComponent<RoundData>();
        board = GameObject.Find("Board").GetComponent<Board>();
        currentvalueModifier = baseValueModifier;
    }


    #endregion
    
    #region Tile events
    //public static event Action OnEndTurnEvent;
    #endregion

    #region Tile functions
    public int GetOutPutPower() { return (outputPower * (tileLevel + 1)); }
    public TileEffect GetTileEffect(Entity selfEntity, int tileEffectID, int tileEffectTurns) { currentTileEffect = ImportData.tileEffectDictionary[tileEffectID]; currentTileEffect.OnInit(this, selfEntity, tileEffectTurns); return currentTileEffect; }

    public void SetTileLocation() { tile.transform.position = tileLocation; }

    public void BeingSelected() { isSelect = true; }

    public void StartDrag()
    {

        isDrag = true;
        roundData.player.SetDragTile(this);

        // Getting the tile count in the answer tile spawner
        transform.SetSiblingIndex(board.transform.GetChild(6).childCount);

        tile.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        canvasGroup.alpha = 0.75f;
        canvasGroup.blocksRaycasts = false;
    }

    public void UnSelected() { isSelect = false; roundData.player.SetDragTile(null); }

    public void TileUpgrade(Tile tilePrefab)
    {
        if (tilePrefab.tileLevelText.isActiveAndEnabled == false)
        {
            tilePrefab.tileLevelText.gameObject.SetActive(true);
        }
        tilePrefab.tileLevel++;
        tilePrefab.currentvalueModifier = tilePrefab.valueModifierArr[tilePrefab.tileLevel];
        tilePrefab.tileLevelText.text = tilePrefab.tileLevel.ToString();
    }

    public void ResetTile(Tile tilePrefab)
    {
        // Reset stat
        if (tilePrefab.interactTile != null || tilePrefab.interactTeammate != null)
        {
            //tile.interactTile.position = new Vector2(0, 0);
            tilePrefab.interactTile = null;

            //tile.interactSlot.position = new Vector2(0, 0);
            tilePrefab.interactTeammate = null;
        }

        // If tile get merge flag, destroy the tile
        /*
        if (tilePrefab.toBeDestroyed == true)
        {
            tilePrefab.toBeDestroyed = false;
            //Debug.Log("Tile being merged (" + tilePrefab.tileCellPositionAtStart.ToString() + ") is : " + );
            DestroyTile(tilePrefab);

            Debug.Log("6.1A tile destroyed");
        }
        */

        tilePrefab.inTile = false;
        tilePrefab.inSlot = false;
        tilePrefab.currentState = TileState.State.Idle;
        tilePrefab.isMoved = false;
        tileStatusProcessed = 0;

    }

    public void EndDrag(Tile tilePrefab)
    {
        isDrag = false;
        // Before apply move tile function, only return to it's own position
        transform.SetSiblingIndex(tileCellPosition);
        //ResetTile(this);
        // Should rewrite it as when one turn passes, do such steps...
        UpdatePosition(this);
        //board.RenameTiles();
        board.CheckAndDestroyTiles();


        tile.GetComponent<BoxCollider2D>().size = new Vector2(232, 247);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        roundData.player.SetDragTile(null);


        

        // Final reset
        ResetTile(this);
    }

    public void UpdatePosition(Tile tilePrefab)
    {
        // For displaying map for tile cell
        board.DisplayTileCell();

        // Check if the tile changed its position
        // If tile position had been drop to another tile cell, which the tile cell is blank cell
        if (board.tileCell[tilePrefab.tileCellPosition] == 0)
        {

            tilePrefab.isMoved = true;

            //roundData.player.isActioned = true;

            // Change tile location
            tilePrefab.gameObject.transform.position = board.transform.GetChild(5).gameObject.transform.GetChild(tileCellPosition).position;
            // Indicate tile cell has new tile
            if (tilePrefab.tileType == "normal")
            {
                board.tileCell[tilePrefab.tileCellPosition] = 1;
            }
            else if (tilePrefab.tileType == "special")
            {
                board.tileCell[tilePrefab.tileCellPosition] = 2;
            }
            
            board.tileCell[tilePrefab.tileCellPositionAtStart] = 0;
            // Set tile new position
            tilePrefab.tileCellPositionAtStart = tileCellPosition;
        }
        // If that tile cell is occupied, return its starting position
        else 
        {
            

            // Don't set it
            //tilePrefab.isMoved = false;
            tilePrefab.gameObject.transform.position = board.transform.GetChild(5).gameObject.transform.GetChild(tileCellPositionAtStart).position;
        }

        // For displaying map for tile cell
        board.DisplayTileCell();
        //Debug.Log("board.answerCell after: \n" + tileCellMap);

    }

    public void DestroyTile(Tile tilePrefab)
    {
        // Mark the cell is blank now
        board.tileCell[tilePrefab.tileCellPositionAtStart] = 0;
        
        //Debug.Log("Tile destroyed: " + tilePrefab.gameObject.transform.GetSiblingIndex().ToString());
        // Remove item in answerTile list
        board.tilesInBoard.Remove(tilePrefab);
        if (tilePrefab.CompareTag("NormalTile"))
        {
            // Remove item in roundData
            string removeWord = tilePrefab.transform.GetComponent<NormalTile>().tileText.text.ToString();
            int removeID = (int)tilePrefab.transform.GetComponent<NormalTile>().tileIdiomID;

            if (tilePrefab.isAnswer)
            {
                tilePrefab.isAnswer = false;
            }

            roundData.tilesWord.Remove(removeWord);
            roundData.tilesWordIdiomsID.Remove(removeID);

            
            // For debug
            /*
            string answerString = "";
            foreach (string ans in roundData.answers)
            {
                Debug.Log(ans);
                answerString = String.Join(", ", roundData.answers);
            }
            Debug.Log("answers counts in board: " + roundData.answers.Count + " and answers are: " + answerString);

            string answerIdiomIDString = "";
            foreach (int ID in roundData.answerIdiomsID)
            {
                Debug.Log(ID);
                answerIdiomIDString = String.Join(", ", roundData.answerIdiomsID);
            }
            Debug.Log("answerIdiomsID counts in board: " + roundData.answerIdiomsID.Count + " and answers id are: " + answerIdiomIDString);
            */
        }

        Destroy(tilePrefab.gameObject);
        tilePrefab.transform.SetParent(null);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDrag && isSelect)
        {
            if (collision.CompareTag("Teammate"))
            {
                Debug.Log($"{Time.time} ^3.1A enter teammate {collision.name} ----------------------------");
                inSlot = true;
                interactTeammate = collision.gameObject.GetComponent<Teammate>();

                // For effect
                interactSlotLocation = collision.transform.position;
            }
            else if (collision.CompareTag("NormalTile") || collision.CompareTag("SpecialTile"))
            {
                Debug.Log($"{Time.time} ^3.1B enter tile {collision.name} ----------------------------");
                inTile = true;
                interactTile = collision.gameObject.GetComponent<Tile>();
                interactTile.interactTile = this;
                interactTileLocation = collision.transform.position;
                interactTile.interactTileLocation = tileLocation;
            }

            if (collision.CompareTag("Cell")) { tileCellPosition = collision.gameObject.transform.GetSiblingIndex(); }
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isDrag && isSelect)
        {
            if (collision.CompareTag("Teammate"))
            {

                inSlot = false;
                //interactSlot.position = new Vector2(0, 0);
                interactTeammate = null;
            }
            else if (collision.CompareTag("NormalTile") || collision.CompareTag("SpecialTile"))
            {

                inTile = false;
                //interactTile.interactTile.position = new Vector2(0, 0);
                interactTile.interactTile = null;

                //interactTile.position = new Vector2(0, 0);
                interactTile = null;
            }

            if (collision.CompareTag("Cell"))
            {


                tileCellPosition = tileCellPositionAtStart;
            }
        }
    }
    #endregion

    #region Tile pointerEvents (in order)
    // Click //
    public void OnPointerDown(PointerEventData eventData)
    {


        if (isControllable)
        {
            currentState = TileState.State.Clicked;
            tileStatusProcessed += 1;
            BeingSelected();
        }
        

    }

    // Start drag, called only once, if just click won't be triggered //
    public void OnBeginDrag(PointerEventData eventData)
    {


        if (isControllable)
        {

            currentState = TileState.State.StartDrag;
            roundData.currentTurnState = TurnState.State.PlayerAction;

            tileStatusProcessed += 1;
            StartDrag();
        }
        

    }

    // Dragging //
    public void OnDrag(PointerEventData eventData)
    {
        

        //Debug.Log("^3 dragging the tile");
        //isDrag = true;

        if (isControllable)
        {
            currentState = TileState.State.Dragging;
            transform.position = Input.mousePosition;
        }


    }

    // Drop, then dropper handler will receive after this //
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isControllable)
        {
            currentState = TileState.State.Released;
            //roundData.currentTurnState = TurnState.State.BeforePlayerMoveEnd;

            tileStatusProcessed += 1;
            //UnSelected();

            // Sometime player only just clicked the tile, by this will just end the whole dragging process.
            // To prevent this, just check the status processed(if the tile didn't go through OnBeginDrag, tileStatusProcessed will less than 3)
            if (tileStatusProcessed < 3)
            {
                UnSelected();

                // Forcing tile to reset
                ResetTile(this);
                tileStatusProcessed = 0;
            }


        }
    }

    // For tile merge, receive after onpointerup (AS A DROP RECEIVER) //
    public virtual void OnDrop(PointerEventData eventData)
    {
    }

    // End drag, called only once, for reset. If just click won't be triggered //
    public void OnEndDrag(PointerEventData eventData)
    {

        currentState = TileState.State.EndDrag;
        tileStatusProcessed += 1;

        EndDrag(this);

    }

    public virtual void OnBeforeTurnStart()
    {

    }
    public virtual void OnTurnEnd()
    {
        tileExistedTurns += 1;

    }
    #endregion
}