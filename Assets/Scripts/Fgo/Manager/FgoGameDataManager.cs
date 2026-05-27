using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FgoGameDataManager : MonoBehaviour
{
   public static FgoGameDataManager Instance {  get; private set; }

    [Serializable]
    private class SerializationWrapper<T>
    {
        public List<T> items;
    }

    public Dictionary<string, FgoCharacterData> CharacterDataList {  get; private set; }
    public Dictionary<string, FgoCommandCardData> CommandCardDataList { get; private set; }


    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
        }

        LoadAll();
    }

    private Dictionary<string, T> LoadData<T>(string fileName) where T : FgoGameDataBase
    {
        string path = $"JsonOutput/{fileName}";
        TextAsset textAsset = Resources.Load<TextAsset>(path);

        if (textAsset == null )
        {
            return new Dictionary<string, T>();
        }

        try
        {
            string jsonString = textAsset.text;

            string wrappedJson = "{\"items\":" + jsonString + "}";
            SerializationWrapper<T> wrapper = JsonUtility.FromJson<SerializationWrapper<T>>(wrappedJson);
            Dictionary<string, T> resultDictionary = new Dictionary<string, T>();

            foreach (T item in wrapper.items)
            {
                resultDictionary.Add(item.Id, item);
            }

            return resultDictionary;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[JSON 로드 오류]{fileName}: {ex.Message }");
        }
        return new Dictionary<string, T>();
    }

    public void LoadAll()
    {
        CharacterDataList = LoadData<FgoCharacterData>("FgoCharacterData");
        CommandCardDataList = LoadData<FgoCommandCardData>("FgoCommandCardData");
    }

    public FgoCharacterData GetCharacterData(string id)
    {
        if (CharacterDataList.ContainsKey(id))
        {
            return CharacterDataList[id];
        }
        return null;
    }

    public FgoCommandCardData GetCommandCardData(string id) 
    { 
        if (CommandCardDataList.ContainsKey(id))
        {
            return CommandCardDataList[id];
        }
        return null;
    }
}
