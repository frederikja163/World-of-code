using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Handles loading of mods
/// </summary>
public class ModLoader : MonoBehaviour
{
    /// <summary>
    /// Loads all the different mods
    /// </summary>
    private void Awake()
    {
        LoadProps("props.json");
        LoadBiome("biomes.json");
    }

    /// <summary>
    /// Loads prop definitions
    /// </summary>
    /// <param name="filePath">path to load from</param>
    private void LoadProps(string filePath)
    {
        var jsonraw = File.ReadAllText(Application.streamingAssetsPath + "\\" + filePath);
        var prop = JsonUtility.FromJson<PropDefinition>(jsonraw);

        Instantiate(prop.GetGameObject(), new Vector3(0, 0, 5), Quaternion.identity);
    }

    /// <summary>
    /// Load biome definitions
    /// </summary>
    /// <param name="filePath">path to load from</param>
    public void LoadBiome(string filePath)
    {
        Debug.Log(JsonUtility.ToJson(new Biome(0, 1, 0, 1, Color.cyan) { name = "Dessert" }));
        JsonUtility.FromJson<Biome>("{\"name\":\"Dessert\",\"humidityMin\":0.2,\"humidityMax\":0.4,\"heightMin\":0.0,\"heightMax\":0.6,\"color\":{\"r\":1.0, \"g\":0.85, \"b\":0.15, \"a\":1.0}}");

        BiomeManager.AddBiome(FileLoader.LoadJson<Biome>(Application.streamingAssetsPath + "\\" + filePath));
    }
}