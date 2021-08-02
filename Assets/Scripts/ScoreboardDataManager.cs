using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; //Serialise data

//Purpose: Open and Save data to file
public class ScoreboardDataManager : MonoBehaviour
{
    // Take arguements (Name, Score, File name) and store in data file
    public void SaveData(string playerName, int playerScore, string playerForm, string fileName)
    {
        List<ScoreboardEntry> tempDataList = new List<ScoreboardEntry>();
        tempDataList = LoadData(fileName);
        tempDataList.Add(new ScoreboardEntry() { name = playerName, score = playerScore, formRoom = playerForm });
        tempDataList = SortData(tempDataList);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);

        bf.Serialize(file, tempDataList);
        file.Close();
    }

    // Take a list of objects and sort them in order of lowest time to highest time 
    private List<ScoreboardEntry> SortData(List<ScoreboardEntry> dataList)
    {
        ScoreboardEntry temp;

        for (int i = 0; i < dataList.Count; i++)
        {
            for (int j = i + 1; j < dataList.Count; j++)
            {
                if (dataList[j].score < dataList[i].score)
                {
                    temp = dataList[i];
                    dataList[i] = dataList[j];
                    dataList[j] = temp;
                }
            }
        }
        return dataList;
    }

    // Open data file and load the data into a list based on arguement fileName
    public List<ScoreboardEntry> LoadData(string fileName)
    {
        List<ScoreboardEntry> tempDataList = new List<ScoreboardEntry>();

        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);

            tempDataList = (List<ScoreboardEntry>)bf.Deserialize(file);
            file.Close();
        }
        return tempDataList;
    }

    // Delete data file based on fileName
    public void DeleteFile(string fileName)
    {
        string filePath = Application.persistentDataPath + fileName;

        if (File.Exists(filePath))
        {
            // print debug message file found and deleted
            File.Delete(filePath);
        }
        else
        {
            Debug.Log("Could not delete file: " + filePath);
        }
    }
}

// Get user Name, score and user formRoom to sort by school class
[Serializable]
public class ScoreboardEntry
{

    public string name;
    public int score;
    public string formRoom;
}

