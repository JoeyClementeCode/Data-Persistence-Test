using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Text playerName;
    public string bestPlayerName;
    public int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }
    public void SaveBestPlyaerName()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = bestPlayerName;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/SaveBestPlayerName.json", json);
    }
    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/SaveBestScore.json", json);
    }
    public void LoadBestPlayerName()
    {
        string path = Application.persistentDataPath + "/SaveBestPlayerName.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
        }
    }
    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/SaveBestScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
        }
    }

}