using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SystemSave {

    public static void SaveGameManager()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameManager.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, GameManager.GetInstance());
        stream.Close();
    }

    public static GameManager LoadGameManager()
    {
        string path = Application.persistentDataPath + "/gameManager.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            return formatter.Deserialize(stream) as GameManager;
        }
        else
        {
            Debug.LogError("File doesn't exist " + "\nPath : " + path);
            return null;
        }
    }
}
