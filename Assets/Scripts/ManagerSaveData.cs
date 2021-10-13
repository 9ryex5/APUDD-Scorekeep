using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ManagerSaveData : MonoBehaviour
{
    public static ManagerSaveData MSD;

    [HideInInspector]
    public Settings settings;

    private void Awake()
    {
        MSD = this;
        LoadSettings();
    }

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Settings.as");
        Settings data = new Settings
        {
            language = settings.language
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/Settings.as"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Settings.as", FileMode.Open);
            Settings data = (Settings)bf.Deserialize(file);
            file.Close();

            settings.language = data.language;
        }
        else
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Portuguese:
                    settings.language = Language.PT;
                    break;
                default:
                    settings.language = Language.EN;
                    break;
            }

            SaveSettings();
        }
    }
}

[Serializable]
public class Settings
{
    public Language language;
}