using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Text playerName;
    public Text bestScore;
    private int bestScoreInt;
    private string bestPlayerName;

    private void Start()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.LoadBestScore();
            DataManager.Instance.LoadBestPlayerName();
            bestScoreInt = DataManager.Instance.bestScore;
            bestPlayerName = DataManager.Instance.bestPlayerName;


        }
        bestScore.text = $"Best Score : {bestScoreInt} : {bestPlayerName}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
