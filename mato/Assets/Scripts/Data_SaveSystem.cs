using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Data_SaveSystem
{
    public static void SaveAudio(AudioManager audioManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/audio.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data_Settings data = new Data_Settings(audioManager);

        formatter.Serialize(stream, data);
        Debug.Log("Saved in " + path);
        stream.Close();
    }

    public static Data_Settings LoadAudio()
    {
        string path = Application.persistentDataPath + "/audio.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data_Settings data = formatter.Deserialize(stream) as Data_Settings;
            stream.Close();
            return data;
        }

        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}