using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureBtn : MonoBehaviour
{
    public void StartAdventure() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
}
