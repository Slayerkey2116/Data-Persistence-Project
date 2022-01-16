using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO; // Save Data

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    /// <summary>
    /// Start of what i added
    /// </summary>
    public static MainManager Instance; //save data
    public Text userName;
    public Text BestText;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
    }
    [System.Serializable]  // Save Data
    class SaveData
    {
        public Text userName;
    }

    public void SaveName() // Save Data
    {
        SaveData data = new SaveData(); // create new instance of save data
        data.userName = userName;

        string json = JsonUtility.ToJson(data); // convert saved instance into JSON
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName() // Load Data
    {
        string path = Application.persistentDataPath + "/savefile.json"; // checks for saved instance
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // reads the saved instance
            SaveData data = JsonUtility.FromJson<SaveData>(json); //transforms JSON back into a saved instance 

            userName = data.userName; //sets userName to the color that was saved 
        }
    } /// <summary>
    /// End of what i added
    /// </summary>

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

        BestText.text = "Best score by " + userName + ": " + m_Points;
    }
}
