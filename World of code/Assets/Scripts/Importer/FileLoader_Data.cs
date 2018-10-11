using System;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// Loads files in this case data files
/// </summary>
public static partial class FileLoader
{
    /// <summary>
    /// Loads a json file and outputs the array of loaded objects
    /// </summary>
    /// <typeparam name="t">Type of object to expect from json</typeparam>
    /// <param name="filePath">Path of json file to load</param>
    /// <returns>Array of loaded objects</returns>
    public static t[] LoadJson<t>(string filePath)
    {
        //Load file and remove all whitespace
        string jsonRaw = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<t[]>(jsonRaw);
        
        string jsonTrimmed = jsonRaw.RemoveWhitespace();
        string[] jsonObjs = jsonTrimmed.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);

        //Serialize json using JsonUtility from unity
        t[] objs = new t[jsonObjs.Length];
        for (int i = 0; i < jsonObjs.Length; i++)
        {
            string obj = (i == 0 ? "" : "{") + jsonObjs[i] + (i == jsonObjs.Length - 1 ? "" : "}");
            //objs[i] = JsonUtility.FromJson<t>(obj);
        }
        return objs;
    }

}
