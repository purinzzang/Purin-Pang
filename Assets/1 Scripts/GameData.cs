using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] highScores;
    public int[] stars;
}

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;
    
    void Awake()
    {
        if (gameData == null)
        {
            DontDestroyOnLoad(gameObject);
            gameData = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Load();
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string filePath = Application.persistentDataPath + "/player.dat";
        if (!File.Exists(filePath))
        {
            FileStream file = File.Create(filePath);
            file.Close();
        }

        FileStream fileStream = File.Open(filePath, FileMode.Open);
        SaveData data = saveData;
        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public void Load()
    {
        Debug.Log("Load data path: " + Application.persistentDataPath + "/player.dat");
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
        }
        else
        {
            saveData = new SaveData();
            saveData.isActive = new bool[27];
            saveData.stars = new int[27];
            saveData.highScores = new int[27];
            saveData.isActive[0] = true;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }
}
