using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SL : MonoBehaviour
{
    [SerializeField]
    DB db;
    [SerializeField]
    bool canLoad;


    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/save"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save/game.sav");
        var Json = JsonUtility.ToJson(db);

        byte[] jsonToEncode = Encoding.UTF8.GetBytes(Json);
        string encodedJson = Convert.ToBase64String(jsonToEncode);

        bf.Serialize(file, encodedJson);
        file.Close();
        Debug.Log("save");

    }
    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (Directory.Exists(Application.persistentDataPath + "/save") == true)
        {
            if (File.Exists(Application.persistentDataPath + "/save/game.sav") == true)
            {
                FileStream file = File.Open(Application.persistentDataPath + "/save/game.sav", FileMode.Open);

                byte[] decodedBytes = Convert.FromBase64String((string)bf.Deserialize(file));
                string decodedText = Encoding.UTF8.GetString(decodedBytes);

                JsonUtility.FromJsonOverwrite(decodedText, db);
                file.Close();
                Debug.Log("Load");
            }
        }
    }
}

