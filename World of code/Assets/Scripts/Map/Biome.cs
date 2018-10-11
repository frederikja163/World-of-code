using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Class for defining biomes, the conditions and the specifications for how they look.
/// </summary>
[Serializable]
public class Biome
{
    /// <summary>
    /// Name of biome
    /// </summary>
    public string name;

    /// <summary>
    /// Height minimum value for height range
    /// </summary>
    public float heightMin;
    /// <summary>
    /// Height maximum value for height range
    /// </summary>
    public float heightMax;

    /// <summary>
    /// Humidity minimum value for humidity range
    /// </summary>
    public float humidityMin;
    /// <summary>
    /// Humidity maximum value for humidity range
    /// </summary>
    public float humidityMax;

    /// <summary>
    /// Color of biome
    /// </summary>
    public Color color;

    /// <summary>
    /// Struct for the flora of this biome
    /// </summary>
    [Serializable]
    public struct Flora
    {
        /// <summary>
        /// Name of prop containing objects
        /// </summary>
        public string prop;
        /// <summary>
        /// Spawnrate of flora
        /// </summary>
        public float spawnRate;
    }
    /// <summary>
    /// Array of all the flora in this biome
    /// </summary>
    public Flora[] flora;

    /// <summary>
    /// Struct containing the information of a single tile
    /// </summary>
    [Serializable]
    public struct Tile
    {
        public string texturePath;
        public float spawnRate;
        private Texture2D _texture;
        public Texture2D texture
        {
            get
            {
                if (_texture == null)
                {
                    _texture = FileLoader.LoadTexture(Path.Combine(Application.streamingAssetsPath, texturePath));
                }
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }
    }
    public Tile[] tiles;

    /// <summary>
    /// Constructor to set all variables
    /// </summary>
    /// <param name="name">Name of biome</param>
    /// <param name="heightMin">Minimum height</param>
    /// <param name="heightMax">Maximum height</param>
    /// <param name="humidityMin">Minimum humidity</param>
    /// <param name="humidityMax">Maximum humidity</param>
    /// <param name="color">Color of biome</param>
    public Biome(string name, float heightMin, float heightMax, float humidityMin, float humidityMax, Color color, params Flora[] props)
    {
        this.name = name;
        this.heightMin = heightMin;
        this.heightMax = heightMax;
        this.humidityMin = humidityMin;
        this.humidityMax = humidityMax;
        this.color = color;
        this.flora = props;
    }

    /// <summary>
    /// Check wether this biome is within the range of provided variables
    /// </summary>
    /// <param name="height">Height to check on</param>
    /// <param name="humidity">Humidity to check on</param>
    /// <returns>Wether or not this biome is within the range of the height and humidity provided</returns>
    public bool WithinRange(float height, float humidity)
    {
        return heightMin <= height && height <= heightMax &&
            humidityMin <= humidity && humidity <= humidityMax;
    }
}