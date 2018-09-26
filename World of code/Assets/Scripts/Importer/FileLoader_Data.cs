using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static partial class FileLoader
{

    public static t[] LoadJson<t>(string filePath)
    {
        string jsonRaw = File.ReadAllText(filePath);
        string jsonTrimmed = jsonRaw.RemoveWhitespace();
        string[] jsonObjs = jsonTrimmed.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);

        t[] objs = new t[jsonObjs.Length];
        for (int i = 0; i < jsonObjs.Length; i++)
        {
            Debug.Log((i == 0 ? "" : "{") + jsonObjs[i] + (i == jsonObjs.Length - 1 ? "" : "}"));
            objs[i] = JsonUtility.FromJson<t>((i == 0 ? "{" : "") + jsonObjs[i] + (i == jsonObjs.Length - 1 ? "}" : ""));
        }
        return objs;
    }

    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }

}
