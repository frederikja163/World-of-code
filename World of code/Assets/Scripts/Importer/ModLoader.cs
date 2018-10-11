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
        LoadAsset<PropDefinition>("props.json");
        LoadAsset<Biome>("biomes.json");
    }

    /// <summary>
    /// Load asset definitions
    /// </summary>
    /// <param name="filePath">path to load from</param>
    public void LoadAsset<t>(string filePath)
    {
        AssetManager<t>.AddObjs(FileLoader.LoadJson<t>(Path.Combine(Application.streamingAssetsPath, filePath)));
    }
}