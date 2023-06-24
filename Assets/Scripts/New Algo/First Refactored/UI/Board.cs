// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class Board : MonoBehaviour
// {
//     #region Scripts
//     #endregion

//     #region Data importing
//     // Loading idiom data
//     public IdiomList idiomData;
//     public int idiomDataSize;
//     public RoundData roundData;
//     #endregion

//     #region Game object references
//     public GameObject answerTilesSpawner;
//     public GameObject answerTilesCells;
//     public Image boardFog;
//     #endregion

//     #region Board data? However they should import from player which import from player data, should delete****
//     // Tile spawning stat, should import from roundData(which import from outside data aswell), so need to be delete after testing
//     public int normalTileSpawnRate = 80;
//     public int specialTileSpawnRate = 80;
//     public int specialTileLimit = 2;
//     #endregion

//     #region References
//     private Tile _answerTile;
//     public Tile answerTile { get { return _answerTile; } set { _answerTile = value; } }
//     #endregion

//     #region Board elements counting
//     public int[] tileCell = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
//     public List<Tile> tilesInBoard;
//     public int[] answerCount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
//     #endregion

//     void Awake()
//     {
//         // Not ideal place
//         roundData = GameObject.Find("Round Manager").GetComponent<RoundData>();

//         idiomData = ImportData.idioms;
//         idiomDataSize = idiomData.idiom.Length;
//     }

//     #region Board functions
//     // Spawn tile function here, more like an order

//     // Controlling the access of the board
//     public void EnableBoard()
//     {
//         boardFog.gameObject.SetActive(false);
//     }

//     public void DisableBoard() { boardFog.gameObject.SetActive(true); }

//     public void SpawnTiles(int spawnNum)
//     {

//         // Order factories to manufacture tiles, loop as to fill all blank cell in board
//         while (spawnNum > 0)
//         {
//             // Check if there is any blank cell, if there's no then break
//             int blankCells = CheckBlankCell();

//             if (blankCells == 0)
//             {
//                 break;
//             }
//             else
//             {
//                 //List<int> filledTilePosition = new List<int>();

//                 // If there's blank cell, randomize the position
//                 int randomTilePosition = UnityEngine.Random.Range(0, answerTilesCells.transform.childCount);
//                 //Debug.Log("Random tile position: " + randomTilePosition);

//                 // Draw the randomTilePosition until no blank tile cell
//                 if (tileCell[randomTilePosition] == 0)
//                 {
//                     // Decide the type of tile that will be spawn
//                     int spawnTypeRandom = UnityEngine.Random.Range(0, 100);
//                     //Debug.Log("Spawn control number: " + spawnTypeRandom);

//                     //Debug.Log("1. Should spawn to: " + random.ToString() + GameObject.Find("Answer tiles cell (" + random.ToString() + ")").gameObject.transform.position);
//                     // Decide type of tile to be spawn, may need to rewrite
//                     if (spawnTypeRandom < (100 - specialTileSpawnRate))
//                     {
//                         //Debug.Log("specialTileSpawnRate: " + specialTileSpawnRate);
//                         //Debug.Log("Condition 1 output value 1: " + (spawnTypeRandom > 0));
//                         //Debug.Log("Condition 1 output value 2: " + (spawnTypeRandom < (100 - specialTileSpawnRate)));

//                         // Only spawning normal tile needs to be checking duplication
//                         int randomWordPosition = UnityEngine.Random.Range(0, roundData.roundIdiomSize);
//                         int randomIdiomID = UnityEngine.Random.Range(0, ImportData.idioms.idiom.Length);
//                         string randomWord = ImportData.idioms.idiom[randomIdiomID].name[randomWordPosition].ToString();

//                         // Check any duplicated idiom tile in board 
//                         if ((roundData.tilesWordIdiomsID.Contains(randomIdiomID) == true) || (roundData.tilesWord.Contains(randomWord) == true))
//                         {
//                             //int duplicatedRandom = random;
//                             //string duplicatedRandomWord = randomWord;
//                             do
//                             {
//                                 randomWordPosition = UnityEngine.Random.Range(0, roundData.roundIdiomSize);
//                                 randomIdiomID = UnityEngine.Random.Range(0, ImportData.idioms.idiom.Length);
//                                 randomWord = ImportData.idioms.idiom[randomIdiomID].name[randomWordPosition].ToString();
//                                 //Debug.Log(i + "draw another random ID: " + (random + 1) + " word = " + randomWord + " round missing word = " + gameData.roundMissingWord);
//                             }
//                             while ((roundData.tilesWordIdiomsID.Contains(randomIdiomID) == true) || (roundData.tilesWord.Contains(randomWord) == true));
//                         }

//                         //Debug.Log($"After checking: tile idiom ID: {randomIdiomID} word = {randomWord}");
//                         // Add answers into the answer list, according to the round missing word order
//                         roundData.tilesWordIdiomsID.Add(randomIdiomID);
//                         roundData.tilesWord.Add(randomWord);

//                         // Spawning normal tile
//                         newTile.name = "Tile " + randomTilePosition.ToString();
//                         //Debug.Log($"Successfully spawned a normal tile: {newTile.name} {newTile.transform.GetChild(2).GetComponent<Text>().text} in position {randomTilePosition}");

//                         tileCell[randomTilePosition] = 1;
//                         tilesInBoard.Add(newTile);
//                         spawnNum = spawnNum - 1;
//                     }
//                     else if (CheckTileTypeCount("special") < specialTileLimit)
//                     {
//                         // Another random to control special tile element?
//                         int randomTileEffect = UnityEngine.Random.Range(0, ImportData.tileEffectDictionary.Count);

//                         // Spawning special tile
//                         newTile.name = "Tile " + randomTilePosition.ToString();

//                         tileCell[randomTilePosition] = 2;
//                         tilesInBoard.Add(newTile);
//                         spawnNum = spawnNum - 1;
//                     }
//                     else
//                     {
//                         //Debug.Log("Nope");
//                     }
                    
//                 }
//             }
            
//             // Break pt for finishing spawning
//             if (spawnNum == 0)
//             {
//                 //DrawAnswer();
//                 break;
//             }
//         }

//     }
//     public int MaxSpawnTileLevel()
//     {

//         int maxTileLevel = 0;
//         int[] tileLevelScoreArr = { 0, 500, 1500, 3000, 5000, 7500, 10500, 14000, 18000, 22500};

//         for(int i = 0; i < tileLevelScoreArr.Length; i++)
//         {
//             if (roundData.currentPowerScore <= tileLevelScoreArr[i])
//             {
//                 maxTileLevel = i;
//                 break;
//             }
//         };

//         return maxTileLevel;
//     }
//     public void DisplayTileCell()
//     {

//         string tileCellMap = "";
//         for (int i = 0; i < tileCell.Length; i++)
//         {
//             if (i != tileCell.Length)
//             {
//                 tileCellMap = tileCellMap + tileCell[i].ToString() + ", ";
//             }
//             else 
//             {
//                 tileCellMap = tileCellMap + tileCell[i].ToString();
//                 break;
//             }

//             if (i != 0 && (i+1)%5 == 0)
//             {
//                 tileCellMap = tileCellMap + "\n";
//             }
//         }


//     }
//     public int CheckBlankCell()
//     {

//         int blankCellsCount = 0;
//         for (int i = 0; i < tileCell.Length; i++)
//         {
//             if (tileCell[i] == 0)
//             {
//                 blankCellsCount = blankCellsCount + 1;
//             }
//         }

//         return blankCellsCount;
//     }

//     public void CheckAndDestroyTiles()
//     {
//         List<int> tilesToBeDestroyed = new List<int>();
//         string destroyTilesString = "";

//         for (int i = 0; i < tileCell.Length; i++)
//         {
//             //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
//             if (tileCell[i] == -1)
//             {
//                 Tile tile = GameObject.Find("Tile " + i).GetComponent<Tile>();
//                 tile.toBeDestroyed = true;
//             }
//         }

//         for (int i = 0; i < tilesInBoard.Count; i++)
//         {
//             if (tilesInBoard[i].toBeDestroyed == true)
//             {
//                 tilesToBeDestroyed.Add(tilesInBoard[i].tileCellPositionAtStart);
//                 destroyTilesString = destroyTilesString + tilesInBoard[i].tileCellPositionAtStart + ", ";

//             }
//         }

//         foreach (int tilesPosition in tilesToBeDestroyed)
//         {
//             Tile tile = GameObject.Find("Tile " + tilesPosition).GetComponent<Tile>();
//             tile.DestroyTile(tile);
//         }

//     }

//     public int CheckTileTypeCount(string tiletype)
//     {

//         int tileTypeCount = 0;
//         for (int i = 0; i < tilesInBoard.Count; i++)
//         {
//             if (tilesInBoard[i].tileType == tiletype)
//             {
//                 tileTypeCount = tileTypeCount + 1;
//             }
//         }

//         return tileTypeCount;
//     }

//     public void DrawAnswer()
//     {
//         bool isReDrawable = false;
//         int answerWordPosition = -1;
//         List<int> answerListForDrawing = new List<int>();

//         //Debug.Log("answerTilesSpawner.transform.childCount = " + answerTilesSpawner.transform.childCount);
//         // Check any answer tiles in board
//         for (int i = 0; i < answerTilesSpawner.transform.childCount; i++)
//         {
//             //Debug.Log("DrawAns loop i = " + i);
//             Tile tile = GameObject.Find("Tile " + i).GetComponent<Tile>();
//             if (tile.isAnswer)
//             {
//                 //Debug.Log("Board has a answer tile here: " + tile.name + " ,no need to choose another answer tile!");
//                 //isReDrawable = false;
//                 break;
//             }

//             if (tile.CompareTag("NormalTile"))
//             {
//                 answerListForDrawing.Add(i);
//                 //Debug.Log("Tile " + i + " had been add to the drawing list for answer");
//             }
            
//             // If there's no answer tile in board, turn on the answer flag
//             if (i == answerTilesSpawner.transform.childCount - 1)
//             {
//                 //Debug.Log("No answer in board!");
//                 isReDrawable = true;
//                 break;
//             }
//         }

//         // After checking, start drawing answer from the answerBox list
//         if (isReDrawable)
//         {
//             //System.Random random = new System.Random();
//             answerWordPosition = UnityEngine.Random.Range(0, answerListForDrawing.Count);

//             // Remember we are drawing the array order from the answerDrawBox
//             // At this moment, the old answer tile still here and not being destroyed(If it draw the same position, so they will have the same fucking game object name)
//             string answerTileName = "Answer tiles spawner/Tile " + answerListForDrawing[answerWordPosition];
//             //Debug.Log(answerTileName);
//             answerTile = GameObject.Find(answerTileName).GetComponent<Tile>();
//             //Debug.Log($"{Time.time} Answer tile is: Tile {answerListForDrawing[answerWordPosition]}");

//             answerTile.isAnswer = true;
//             //Debug.Log($"{Time.time} Is {answerTile.GetComponent<NormalTile>().tileText.text} answer? -> {answerTile.isAnswer}");

//             // Register idiom data to roundData, **assume all answer tile are normal tile**
//             roundData.currentIdiom = answerTile.GetComponent<NormalTile>().tileIdiom;
//             //Debug.Log($"{Time.time} roundData.currentIdiom = {roundData.currentIdiom}");
//             roundData.currentIdiomID = answerTile.GetComponent<NormalTile>().tileIdiomID;
//             //Debug.Log($"{Time.time} roundData.currentIdiomID = {roundData.currentIdiomID}");
            
//             roundData.currentAnswerWord = answerTile.GetComponent<NormalTile>().tileText.text;
//             //Debug.Log($"{Time.time} roundData.roundMissingWord = {roundData.currentAnswerWord}");
//             roundData.currentAnswerWordOrder = answerTile.GetComponent<NormalTile>().tileWordOrder;
//             //Debug.Log($"{Time.time} roundData.roundMissingWordOrder = {roundData.currentAnswerWordOrder}");

//             //answerTile.transform.GetChild(2).GetComponent<TMP_Text>().color = new Color32(160, 60, 60, 255);

//             roundData.askedAnswerWords.Add(roundData.currentAnswerWord);
//             roundData.askedAnswerIdiomsIDs.Add(roundData.currentIdiomID);
//             roundData.askedAnswerPositions.Add(roundData.currentAnswerTilePosition);
//         }

//         // Find a better place for displaying idiom!!
//         roundData.UI_firstCharacter.text = roundData.currentIdiom.firstWord;
//         roundData.UI_secondCharacter.text = roundData.currentIdiom.secondWord;
//         roundData.UI_thirdCharacter.text = roundData.currentIdiom.thirdWord;
//         roundData.UI_fourthCharacter.text = roundData.currentIdiom.fourthWord;
//         roundData.UI_currentIdiomIDText.text = roundData.currentIdiomID.ToString();
//         roundData.missingWordTile.transform.position = roundData.questionTiles.transform.GetChild(roundData.currentAnswerWordOrder).transform.position;

//         Debug.Log($"{Time.time} Board.DrawAnswer (end)");
//     }

//     public void UpdateTileCell()
//     {
//         for (int i = 0; i < answerTilesSpawner.transform.childCount; i++)
//         {
//             Tile tile = answerTilesSpawner.transform.GetChild(i).GetComponent<Tile>();
//             if (tile.toBeDestroyed)
//             {
//                 tileCell[tile.tileCellPositionAtStart] = -1;
//             }
//         }

//     }

//     public int CheckAnswerTile()
//     {
//         int answerTilesCount = 0;

//         for (int i = 0; i < answerTilesSpawner.transform.childCount; i++)
//         {
//             Tile tile = answerTilesSpawner.transform.GetChild(i).GetComponent<Tile>();
//             if (tile.isAnswer)
//             {
//                 answerTilesCount = answerTilesCount + 1;
//             }
//         }

//         if (answerTilesCount == 0)
//         {
//             answerTile = null;
//             SpawnTiles(CheckBlankCell());
//             DrawAnswer();
//         }

//         return answerTilesCount;
//     }
    
//     public void RenameTiles()
//     {
//         //List<Tile> newTilesInBoard = new List<Tile>();
//         // Change tile name according to it's position
//         for (int i = 0; i < answerTilesSpawner.transform.childCount; i++)
//         {
//             Tile tile = answerTilesSpawner.transform.GetChild(i).GetComponent<Tile>();
//             tile.name = "Tile " + tile.tileCellPositionAtStart;
//         }
//     }
//     #endregion
// }