using System;
using UnityEngine;

/// <summary>
/// Class for defining biomes, the conditions and the specifications for how they look.
/// </summary>
[Serializable]
public class Biome
{

    public float heightMin;
    public float heightMax;
    public float humidityMin;
    public float humidityMax;
    public Color color;

    public Biome(float heightMin, float heightMax, float humidityMin, float humidityMax, Color color)
    {
        this.heightMin = heightMin;
        this.heightMax = heightMax;
        this.humidityMin = humidityMin;
        this.humidityMax = humidityMax;
        this.color = color;
    }

    public bool WithinRange(float height, float humidity)
    {
        return heightMin <= height && height <= heightMax &&
            humidityMin <= humidity && humidity <= humidityMax;
    }
}