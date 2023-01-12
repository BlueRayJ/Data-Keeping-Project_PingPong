using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputIDField;
    public TMP_Text bestScoreText;
    
    private void Start()
    {
        inputIDField.text = MainManager.instance.playerData.ID;
        SetBesetScore();
    }

    void SetBesetScore()
    {
        MainManager.instance.LoadScore();
        string text = "0";
        if (MainManager.instance.BestScoreDatas.Count != 0)
        {
            if (!string.IsNullOrEmpty(MainManager.instance.BestScoreDatas[0].ID))
            {
                text = MainManager.instance.ScoreToString(MainManager.instance.BestScoreDatas[0]);
            }
            else
            {
                text = "-1";
            }
        }
        bestScoreText.SetText("Best Score : " + text);
    }

    public void DeleteScore()
    {
        MainManager.instance.DeleteScoreFile();
        SetBesetScore();
    }

    public void InputID()
    {
        MainManager.instance.playerData.ID = inputIDField.text;  
    }
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(MainManager.instance.playerData.ID))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("ID를 입력해 주십시오");
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
