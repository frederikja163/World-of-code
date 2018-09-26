using System.Collections.Generic;

/// <summary>
/// Manages biomes and searches for the correct biome
/// </summary>
public static class BiomeManager
{
    /// <summary>
    /// Collections of biomes, this collections stores the definition of all biomes.
    /// </summary>
    private static List<Biome> biomes = new List<Biome>();
    
    /// <summary>
    /// Add a new collection to the collection list
    /// </summary>
    /// <param name="biomes">Biome to add to the list of collections</param>
    public static void AddBiome(params Biome[] biomes)
    {
        BiomeManager.biomes.AddRange(biomes);
    }

    /// <summary>
    /// Gets a biome based on height and humidity map generated in the map class
    /// </summary>
    /// <param name="height">Height noise to search for a biome</param>
    /// <param name="humidity">Humidity noise to search for a biome</param>
    /// <returns>Found biome or null if none found</returns>
    public static Biome GetBiome(float height, float humidity)
    {
        for (int i = 0; i < biomes.Count; i++)
        {
            if (biomes[i].WithinRange(height, humidity))
            {
                return biomes[i];
            }
        }
        return null;
    }
}
