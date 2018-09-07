using System.Collections.Generic;

public static class BiomeManager{

    private static List<BiomeCollection> collections = new List<BiomeCollection>();
    
    public static void AddCollection(BiomeCollection biomeCollection)
    {
        collections.Add(biomeCollection);
    }

    public static Biome GetBiome(float height, float humidity)
    {
        for (int i = 0; i < collections.Count; i++)
        {
            Biome biome = collections[i].FindBiome(x => x.WithinRange(height, humidity));
            if (biome != null)
            {
                return biome;
            }
        }
        return null;
    }
}
