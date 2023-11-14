using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public PlayerInfo[] generalInfo;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        ReadGeneralInfo();
    }

    public void ReadGeneralInfo()
    {
        if (!File.Exists(Application.streamingAssetsPath + "GeneralInfo.json"))
        {
            NewGInfo();
        }
        else
        {
            ReadGInfo();
        }

    }
    public void ReadGInfo()
    {
        string url = Application.streamingAssetsPath + "Player.json";
        string json = File.ReadAllText(url);
        generalInfo = JsonHelper.FromJson<PlayerInfo>(json);
        print(generalInfo);

    }
    public void NewGInfo()
    {
        generalInfo = new PlayerInfo[1];
        generalInfo[0] = new PlayerInfo(0, 0, 0);
        string json = JsonHelper.ToJson(generalInfo, true);
        string url = Application.streamingAssetsPath + "Player.json";

        File.WriteAllText(url, json);

        print(json);

    }
    public void SaveGInfo()
    {

        string json = JsonHelper.ToJson(generalInfo, true);
        string url = Application.streamingAssetsPath + "Player.json";
        File.WriteAllText(url, json);

        print("Saved: " + json);

        Debug.Log("Salvado");
    }
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
