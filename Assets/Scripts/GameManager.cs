using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text Username_field;

    public Text best;

    public int highScore;

    public string playerName;

    public string playerNameBest;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGame();

        Debug.Log(playerName);

        //best = gameObject.GetComponent<Text>();
        best.text = " Best Score: " + playerNameBest + " : " + highScore;

    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string playerNameBest;
        public int highScore;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.highScore = highScore;
        data.playerNameBest = playerNameBest;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefiletest.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefiletest.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.playerName;
            highScore = data.highScore;
            playerNameBest = data.playerNameBest;
        }
    }



    public void StartNew()
    {
      SceneManager.LoadScene(1);
    }

    public void testInput()
    {
        playerName = Username_field.text.ToString();
        Debug.Log(playerName);
    }

    public void Exit()
    {
        SaveGame();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
