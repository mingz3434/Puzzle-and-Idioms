// using System;
// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Events;
// using UnityEngine.SceneManagement;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

// public class UIManage : MonoBehaviour
// {

//     #region Scripts
//     private RoundData roundData;
//     #endregion

//     #region Game object references
//     public Dictionary<int, Popup> currentPopups = new Dictionary<int, Popup>();
//     public GameObject popupPrefabTiny;
//     public GameObject popupPrefabSmall;
//     public GameObject popupPrefabMedium;
//     public GameObject popupPrefabGameover;
//     #endregion

//     #region UI Manager data
//     public int popupCounts = -1;
//     #endregion

//     #region Flow
//     void Awake()
//     {
//         roundData = GameObject.Find("Round Manager").GetComponent<RoundData>();
//     }
    

//     #endregion

//     #region UI Manager functions
//     // Change to Enum if possible

//     // For battling msg
//     public void SpawnPopup(string msg, string scheme)
//     {

//         // Temp
//         popupCounts += 1;

//         switch (scheme)
//         {
//             case "normal":
//                 popup.bodyTextBox.GetComponent<Image>().color = new Color32(230, 250, 100, 255);
//                 //popup.bodyText.color = new Color32(230, 250, 100, 255);
//                 break;

//             case "emergency":
//                 popup.bodyTextBox.GetComponent<Image>().color = new Color32(250, 80, 80, 255);
//                 //popup.bodyText.color = new Color32(230, 250, 100, 255);
//                 break;

//             case "special":
//                 popup.bodyTextBox.GetComponent<Image>().color = new Color32(250, 250, 100, 255);
//                 //popup.bodyText.color = new Color32(230, 250, 100, 255);
//                 break;

//         }
        
//         currentPopups.Add(popup.popupID, popup);

//         // Add time for displaying the popup
//         roundData.currentTurnDuration += popup.popupDuration;

//         // Need to be self-destroyed after 2s(default = 2s from popup class)
//         this.Wait(popup.popupDuration, () => currentPopups[popup.popupID].DestroyPopup(popup.popupID));

//     }

//     // Other cases
//     public void SpawnPopup(int caseRefID)
//     {


//         // Temp
//         // popupCounts += 1;
//         // Popup popup = popupFactory.CreatePopup(popupPrefabSmall, this.gameObject, caseRefID);
//         // //popup.popupID = popupCounts;
//         // currentPopups.Add(popup.popupID, popup);
//     }
//     public void SpawnPopup(int caseRefID, UnityAction buttonAction1)
//     {


//         // // Temp
//         // popupCounts += 1;
//         // Popup popup = popupFactory.CreatePopup(popupPrefabMedium, this.gameObject, caseRefID, buttonAction1);
//         // //popup.popupID = popupCounts;
//         // currentPopups.Add(popup.popupID, popup);
//     }
//     public void SpawnPopup(string size, int caseRefID, UnityAction buttonAction1, UnityAction buttonAction2)
//     {


//         // Temp
//         popupCounts += 1;
//         GameObject popupPrefab = new GameObject();
//         switch (size)
//         {
//             case "medium":
//                 popupPrefab = popupPrefabMedium;
//                 break;

//             case "gameover":
//                 popupPrefab = popupPrefabGameover;
//                 break;
//         }
//         // Popup popup = popupFactory.CreatePopup(popupPrefab, this.gameObject, caseRefID, buttonAction1, buttonAction2);
//         // //popup.popupID = popupCounts;
//         // currentPopups.Add(popup.popupID, popup);
//     }

//     public int GetPopupCounts()
//     {
//         return popupCounts;
//     }
//     #endregion
// }
