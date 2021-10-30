using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData : MonoBehaviour
{
    public static SaveData SD;

    [HideInInspector]
    public List<Match> matches;
    [HideInInspector]
    public Settings settings;

    private void Awake()
    {
        SD = this;
        LoadMatches();
        LoadSettings();
    }

    public void AddMatch(Match _m)
    {
        matches.Add(_m);
        SaveMatches();
    }

    public void SaveMatches()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Matches.as");
        Matches data = new Matches
        {
            matches = new List<Match>(matches)
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadMatches()
    {
        if (File.Exists(Application.persistentDataPath + "/Matches.as"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Matches.as", FileMode.Open);
            Matches data = (Matches)bf.Deserialize(file);
            file.Close();

            matches = new List<Match>(data.matches);
        }
        else
        {
            matches = new List<Match>();
            SaveMatches();
        }
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
public class Matches
{
    public List<Match> matches;
}

[Serializable]
public class Settings
{
    public Language language;
}