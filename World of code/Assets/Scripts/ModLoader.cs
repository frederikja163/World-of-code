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
        string jsonRaw = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, filePath));
        PropDefinition prop = JsonUtility.FromJson<PropDefinition>(jsonRaw);
        Instantiate(prop.GetGameObject(), new Vector3(0, 0, 5), Quaternion.identity);
    }

    /// <summary>
    /// Load biome definitions
    /// </summary>
    /// <param name="filePath">path to load from</param>
    public void LoadBiome(string filePath)
    {
        string jsonRaw = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, filePath));
        BiomeManager.AddCollection(JsonUtility.FromJson<BiomeCollection>(jsonRaw));
    }
}