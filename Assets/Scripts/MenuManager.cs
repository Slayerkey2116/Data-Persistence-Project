using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] TMP_InputField UserName;
    public string playerName;

  
    private void Start()
    {
       //Added
       if(DataManager.Instance.currentUser !=" ")
        {
            UserName.text = DataManager.Instance.currentUser;
        }
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        //Added
        DataManager.Instance.currentUser = UserName.text;
    }

    public void Exit() // Conditional compiling, changes functionality based on where the application is running
    {
       // DataManager.Instance.SaveBest(); // saves the last user when the app closes
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void LoadData()
    {
        DataManager.Instance.LoadBest();
       // BestUser.SelectName(DataManager.Instance.userName);
    }
}
