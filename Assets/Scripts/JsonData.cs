using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static UnityEditor.Progress;

[Serializable]
public sealed class SaveData
{
    public string Name;
    public Vector3 Position;

    public override string ToString() => $"Name {Name} Position {Position.x}, {Position.y}, {Position.z}";
}

[Serializable]
public struct Vector3Serializable
{
    public float X;
    public float Y;
    public float Z;

    private Vector3Serializable(float valueX, float valueY, float valueZ)
    {
        X = valueX;
        Y = valueY;
        Z = valueZ;
    }

    public static implicit operator Vector3(Vector3Serializable value)
    {
        return new Vector3(value.X, value.Y, value.Z);
    }
}

public class JsonData<T> : IData<T>
{
    public void Save(T data, string path = null)
    {
        var str = JsonUtility.ToJson(data);
        File.WriteAllText(path, str);
    }

    public void SaveFromList(List<SaveData> data, string path = null)
    {
        List<string> allData = new List<string>();

        foreach (var item in data)
        {
            allData.Add(JsonUtility.ToJson(item));
        }

        File.WriteAllLines(path, allData);
    }

    public T Load(string path = null)
    {
        var str = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(str);
    }
}