using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public SaveData playerData;
    public List<SaveData> BestScoreDatas = new List<SaveData>();

    public void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }
    public string ScoreToString(SaveData sd)
    {
        return sd.ID + " : " + sd.Score;
    }

    public bool ComparScore(SaveData sd)
    {
        if (BestScoreDatas.Count == 0) return true;
        return BestScoreDatas[0].Score < sd.Score;
    }

    [System.Serializable]
    public class SaveData
    {
        public string ID;
        public int Score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        if(!ComparScore(playerData))
        {
            return;
        }
        data = playerData;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/scorefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/scorefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (BestScoreDatas.Count != 0) BestScoreDatas[0] = data;
            else
            {
                BestScoreDatas.Add(data);
            }
        }
        else
        {
            Debug.Log("점수 파일이 존재하지 않습니다.");
        }   
    }

    public void DeleteScoreFile()
    {
        string path = Application.persistentDataPath + "/scorefile.json";
        if (File.Exists(path))
        {
            File.Delete(Application.persistentDataPath + "/scorefile.json");
            BestScoreDatas.Clear();
        }
        else
        {
            Debug.Log("점수 파일이 존재하지 않습니다.");
        }
    }
}
