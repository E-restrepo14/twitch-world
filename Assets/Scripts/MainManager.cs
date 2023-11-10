using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainManager : MonoBehaviour
{
    public Text bitsText;       // done
//    public Text PlayerText;
//    public GameObject GameOverPanel;

    [SerializeField] private int m_bits = 0;       // done
    public string currentPlayerName = "no name";       // done

    [SerializeField] private int hihgestScore;       // done
    [SerializeField] private string topPlayer;       // done


    // Start is called before the first frame update
    void Start()
    {
        LoadBits();
    }

    public void AddPoint(int point) // esto se debe llamar desde un coso externo
    {
        m_bits += point;
        bitsText.text = $"Score : {m_bits}";
        SaveBits();
    }

    void ShowScore(string name, int score)
    {
        print("Best Score : " + name + " : " + score);
    }

    public void GameOver()
    {
        LoadBits();
    }

    [System.Serializable]
    class SaveData
    {
        public int dataUserBits;
        public string dataUserName;
    }


    public void SaveBits()
    {
        print("data saved");
        SaveData data = new SaveData();
        data.dataUserBits = m_bits;
        data.dataUserName = currentPlayerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBits()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            print("data loaded");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            hihgestScore = data.dataUserBits;
            topPlayer = data.dataUserName;
            ShowScore(topPlayer, hihgestScore);
        }
    }
}
