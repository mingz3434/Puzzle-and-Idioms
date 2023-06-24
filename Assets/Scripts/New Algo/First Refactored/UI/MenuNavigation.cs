using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public GameObject[] panels;
    public Button[] buttons;

    
    public void navigationPanelChange(GameObject activePanel)
    {

        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        activePanel.SetActive(true);

    }

    public void navigationBarItemChange(Button buttonOnActive)
    {
        foreach (Button button in buttons)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.SetActive(true);
            button.transform.GetChild(2).gameObject.SetActive(false);
            button.transform.GetChild(3).gameObject.SetActive(true);
        }
        buttonOnActive.transform.GetChild(0).gameObject.SetActive(true);
        buttonOnActive.transform.GetChild(1).gameObject.SetActive(false);
        buttonOnActive.transform.GetChild(2).gameObject.SetActive(true);
        buttonOnActive.transform.GetChild(3).gameObject.SetActive(false);
    }
}
