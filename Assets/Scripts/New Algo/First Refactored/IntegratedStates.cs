using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntegratedStates : MonoBehaviour
{
    public enum EntityState { LOADING, ALIVE, DEAD }
    public EntityState _EntityState = EntityState.LOADING;
    public EntityState GetEntityState() { return _EntityState; }
    public void SetAlive() { _EntityState = EntityState.ALIVE; }
    public void SetDead() { _EntityState = EntityState.DEAD; }
    public void ResetEntityState() { _EntityState = EntityState.LOADING; }

    public enum GameState { LOADING, PLAYING, PAUSED, WIN, GAME_OVER }
    public GameState _GameState = GameState.LOADING;
    public GameState GetGameState() { return _GameState; }
    public void SetPlaying() { _GameState = GameState.PLAYING; }
    public void SetPaused() { _GameState = GameState.PAUSED; }
    public void SetWin() { _GameState = GameState.WIN; }
    public void SetGameOver() { _GameState = GameState.GAME_OVER; }
    public void ResetGameState() { _GameState = GameState.LOADING; }

    // ! REPOSITION LATER
    // public enum TileState { LOADING, IDLE, CLICKED, START_DRAG, DRAGGING, RELEASED, PROCESSING, END_DRAG }
    // public TileState _TileState = TileState.LOADING;
    // public TileState GetTileState() { return _TileState; }
    // public void SetIdle() { _TileState = TileState.IDLE; }
    // public void SetClicked() { _TileState = TileState.CLICKED; }
    // public void SetStartDrag() { _TileState = TileState.START_DRAG; }
    // public void SetDragging() { _TileState = TileState.DRAGGING; }
    // public void SetReleased() { _TileState = TileState.RELEASED; }
    // public void SetProcessing() { _TileState = TileState.PROCESSING; }
    // public void SetEndDrag() { _TileState = TileState.END_DRAG; }
    // public void ResetTileState() { _TileState = TileState.LOADING; }

    public enum TurnState { LOADING, BEFORE_TURN_START, TURN_START, BEFORE_PM, PM, END_PM, BEFORE_MM, MM, END_MM, ANIMATION, TURN_END }
    public TurnState _TurnState = TurnState.LOADING;
    public TurnState GetTurnState() { return _TurnState; }
    public void SetFirstTimeInit() { _TurnState = TurnState.LOADING; }
    public void SetBeforeTurnStart() { _TurnState = TurnState.BEFORE_TURN_START; }
    public void SetTurnStart() { _TurnState = TurnState.TURN_START; }
    public void SetBeforePM() { _TurnState = TurnState.BEFORE_PM; }
    public void SetPM() { _TurnState = TurnState.PM; }
    public void SetEndPM() { _TurnState = TurnState.END_PM; }
    public void SetBeforeMM() { _TurnState = TurnState.BEFORE_MM; }
    public void SetMM() { _TurnState = TurnState.MM; }
    public void SetEndMM() { _TurnState = TurnState.END_MM; }
    public void SetAnimation() { _TurnState = TurnState.ANIMATION; }
    public void SetTurnEnd() { _TurnState = TurnState.TURN_END; }
    public void ResetTurnState() { _TurnState = TurnState.LOADING; }
}

