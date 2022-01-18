using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    /// <summary>
    /// Save Data
    /// </summary>
    public static DataManager Instance; //save data
    public string userName;
    public int userScore;

    //Added
    public string currentUser;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        // /*Was in MainManager.Awake()*/ userName.text = MenuManager.Instance.playerName;
    }

    [System.Serializable]  // Save Data
    class SaveData
    {
        public string bestUser;
        public int bestScore;
    }

    public void SaveBest(string user, int score) // Save Data
    {
        SaveData data = new SaveData(); // create new instance of save data
        data.bestUser = user;
        data.bestScore = score;

        string json = JsonUtility.ToJson(data); // convert saved instance into JSON
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBest() // Load Data
    {
        string path = Application.persistentDataPath + "/savefile.json"; // checks for saved instance
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // reads the saved instance
            SaveData data = JsonUtility.FromJson<SaveData>(json); //transforms JSON back into a saved instance 

            userName = data.bestUser; //sets userName to the one that was saved 
            userScore = data.bestScore;
        }
    } /// <summary>
      /// Save Data
      /// </summary>
}
