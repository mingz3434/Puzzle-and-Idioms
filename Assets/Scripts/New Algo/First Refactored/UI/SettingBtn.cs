using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingBtn : MonoBehaviour
{
    // #region Scripts
    // private UIManage uiManage;
    // #endregion

    // #region Game object reference
    // [SerializeField] private Button settingBtn;
    // #endregion

    // #region Flow
    // void Awake()
    // {
    //     uiManage = GameObject.Find("UI Manager").GetComponent<UIManage>();
    //     settingBtn = gameObject.GetComponent<Button>();
    // }

    // #endregion

    // #region Button functions
    // public void OnClick()
    // {
    //     // Shit solutions, the ID will be +1 after calling the function
    //     int designatedPopupID = uiManage.GetPopupCounts() + 1;
    //     uiManage.SpawnPopup("medium", 3, () => uiManage.currentPopups[designatedPopupID].DestroyPopup(designatedPopupID), () => StopAdventure());
    // }

    // public static void StopAdventure()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    // }
    // #endregion
}

