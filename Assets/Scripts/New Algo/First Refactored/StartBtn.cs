using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
public void StartPlaying() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
}
