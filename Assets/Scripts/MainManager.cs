using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public int bestScore;
    public Rigidbody Ball;
    public Text currentPlayerName;
    public Text ScoreText;
    public GameObject GameOverText;
    public Text bestScoreText;

    private bool m_Started = false;
    private int m_Points;
    private string bestPlayerName;
    private bool m_GameOver = false;


    private void Awake()
    {
        bestPlayerName = DataManager.Instance.bestPlayerName;
        if (DataManager.Instance != null)
        {
            DataManager.Instance.LoadBestScore();
            DataManager.Instance.LoadBestPlayerName();
            bestScore = DataManager.Instance.bestScore;
            currentPlayerName.text = DataManager.Instance.playerName.text;
        }

        bestScoreText.text = $"Best Score: {bestScore} : {bestPlayerName}";
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void BestScoreInt(int x)
    {
        if (x < m_Points)
        {
            bestScore = m_Points;
            SaveBestScore();
            SaveBestScoreName();
        }
    }
    public void SaveBestScore()
    {
        if (DataManager.Instance != null)
        {

            DataManager.Instance.bestScore = bestScore;
            DataManager.Instance.SaveBestScore();
        }
    }
    public void SaveBestScoreName()
    {
        DataManager.Instance.bestPlayerName = currentPlayerName.text;
        DataManager.Instance.SaveBestPlyaerName();
    }
}
