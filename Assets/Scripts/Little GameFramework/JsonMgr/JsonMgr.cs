using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Json方案枚举
/// </summary>
public enum E_JsonType
{
    JsonUtlity,
    LitJson
}

/// <summary>
/// Json管理器
/// </summary>
public class JsonMgr : Singleton<JsonMgr>
{
    private JsonMgr() { }

    /// <summary>
    /// 将对象数据存储到硬盘
    /// </summary>
    /// <param name="data">对象名称</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="type">Json方案</param>
    public void SaveData(object data, string fileName, E_JsonType type = E_JsonType.JsonUtlity)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string jsonStr = "";

        switch(type)
        {
            case E_JsonType.JsonUtlity:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case E_JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }

        File.WriteAllText(path, jsonStr);
    }

    /// <summary>
    /// 将对象数据读取出来
    /// </summary>
    /// <typeparam name="T">读取出的对象类型</typeparam>
    /// <param name="fileName">文件名称</param>
    /// <param name="type">Json方案</param>
    /// <returns>读取出的对象</returns>
    public T LoadData<T>(string fileName, E_JsonType type = E_JsonType.JsonUtlity) where T : new()
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";

        if (!File.Exists(path))
        {
            path = Application.persistentDataPath + "/" + fileName + ".json";
        }
        if (!File.Exists(path))
        {
            return new T();
        }

        string jsonStr = File.ReadAllText(path);
        T data = default(T);

        switch(type)
        {
            case E_JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case E_JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }

        return data;
    }
}
