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
    public Text UserName;

    public void NewNameSelected(Text text)
    {
        // add code here to handle when a color is selected

        MainManager.Instance.userName = text;
    }

    private void Start()
    {
       /* UserName.Init();
        //this will call the NewNameSelected function when the color picker have a color button clicked.
        UserName.onNameChanged += NewNameSelected;

        UserName.SelectName(MainManager.Instance.userName); //pre-select a saved color*/
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit() // Conditional compiling, changes functionality based on where the application is running
    {
        MainManager.Instance.SaveName(); // saves the user's last selected color when the app closes
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveNameTyped()
    {
        MainManager.Instance.SaveName();
    }

    public void LoadNameTyped()
    {
        MainManager.Instance.LoadName();
        //UserName.SelectName(MainManager.Instance.userName);
    }
}
