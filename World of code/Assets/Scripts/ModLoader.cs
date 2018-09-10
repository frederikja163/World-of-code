using System.IO;
using UnityEngine;

public class ModLoader : MonoBehaviour
{

    private void Awake()
    {
        LoadBiome();
    }
	
    public void LoadBiome()
    {
        string jsonRaw = File.ReadAllText(Application.streamingAssetsPath + "/biomes.json");
        BiomeManager.AddCollection(JsonUtility.FromJson<BiomeCollection>(jsonRaw));
    }
}
