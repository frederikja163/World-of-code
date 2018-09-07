using System;


[Serializable]
public class BiomeCollection
{

    public Biome[] biomes;

    public Biome FindBiome(Predicate<Biome> predicate)
    {
        for (int i = 0; i < biomes.Length; i++)
        {
            if (predicate.Invoke(biomes[i]))
            {
                return biomes[i];
            }
        }
        return null;
    }
}
