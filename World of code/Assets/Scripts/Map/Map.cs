﻿using UnityEngine;

/// <summary>
/// Generates and manages the world
/// </summary>
public class Map : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// A singleton to be called by other classes
    /// </summary>
    public static Map Instance;

    /// <summary>
    /// Scale of the generated map
    /// </summary>
    [SerializeField]
    private float heightScale = 100f;
    [SerializeField]
    private float humidityScale = -100f;
    [SerializeField]
    private float floraScale = 2;
    [SerializeField]
    private float tileScale = 1.5f;
    /// <summary>
    /// Seed to generate map with
    /// </summary>
    [SerializeField]
    private int seed = -1;

    /// <summary>
    /// Size of the area around the player which is loaded at once
    /// </summary>
    [Space]
    [SerializeField]
    private Vector2Int size;
    /// <summary>
    /// Offset for the y axis in the area drawn around the player
    /// </summary>
    [SerializeField]
    private int yOffset;
    /// <summary>
    /// Transform of player used to draw in the surrounding area
    /// </summary>
    [SerializeField]
    private PlayerMovement player;

    /// <summary>
    /// Prefab of the first tile
    /// </summary>
    [Space]
    [SerializeField]
    private Tile tilePrefab;

    /// <summary>
    /// 2d array to store all tiles so we can reuse them
    /// </summary>
    private Tile[,] tiles;

    public enum NoiseType
    {
        height, humidity, flora, tile
    }
    #endregion Variables

    #region Unity calls
    /// <summary>
    /// Manages singleton
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Generates the map at the start of the game
    /// </summary>
    private void Start()
    {
        tiles = new Tile[size.x, size.y];

        GenerateSeed();
        GenerateMap();
    }
    #endregion Unity calls

    #region Update and generate map
    /// <summary>
    /// Updates the map. This can be called multiple times since it dosen't instantiate any objects
    /// </summary>
    public void UpdateMap()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                UpdateTile(ref tiles[x, y],
                    new Vector2Int(x - size.x / 2 + player.mapPosition.x,
                        y - size.y / 2 + player.mapPosition.y + yOffset));
            }
        }
    }

    /// <summary>
    /// Generates a map from new
    /// </summary>
    private void GenerateMap()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                CreateTile(new Vector2Int(x, y));
            }
        }
        UpdateMap();
    }

    /// <summary>
    /// Creates a new tile. WARNING: does not set the position of the tile you will have to do that yourself through UpdateTile
    /// </summary>
    /// <param name="position">The tile position within the array</param>
    private void CreateTile(Vector2Int position)
    {
        //Start by instantiating and setting the basic values of the tile
        Tile tile = Instantiate(tilePrefab.gameObject, transform).GetComponent<Tile>();
        tile.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
        tiles[position.x, position.y] = tile;
    }

    /// <summary>
    /// Updates the tile and sets it position.
    /// </summary>
    /// <param name="tile">Tile to update</param>
    /// <param name="position">Position to update to</param>
    private void UpdateTile(ref Tile tile, Vector2Int position)
    {
        tile.transform.position = new Vector3(position.x, 0, position.y);
        tile.name = "( " + position.x + ", " + position.y + ")";

        tile.SetBiome(MapPositionToBiome(position), GetPerlinNoise(position, NoiseType.tile));
        tile.SetFlora(GetPerlinNoise(position, NoiseType.flora));
    }
    #endregion Update and generate map

    #region Converters/utility
    /// <summary>
    /// Takes a float representing something from a noisemap which will be turned into a biome
    /// </summary>
    /// <param name="noise">Noise which will be used to turn into a biome</param>
    /// <returns>Int which represents a biome</returns>
    private Biome MapPositionToBiome(Vector2Int mapPosition)
    {
        float height = GetPerlinNoise(mapPosition, NoiseType.height);
        float humidity = GetPerlinNoise(mapPosition, NoiseType.humidity);
        return AssetManager<Biome>.FindObj(x => x.WithinRange(height, humidity));
    }

    /// <summary>
    /// Get a perlin noise for use with biomes
    /// </summary>
    /// <param name="position">Position to take noise from</param>
    /// <returns>The float of the position in the noise map</returns>
    private float GetPerlinNoise(Vector2 position, NoiseType noiseType)
    {
        Vector2 perlPos = Vector2.zero;
        switch (noiseType)
        {
            case NoiseType.height:
                perlPos = new Vector2(position.x / heightScale + seed, position.y / heightScale + seed);
                break;
            case NoiseType.humidity:
                perlPos = new Vector2(position.x / humidityScale + seed, position.y / humidityScale + seed);
                break;
            case NoiseType.flora:
                perlPos = new Vector2(position.x / floraScale + seed, position.y / floraScale + seed);
                break;
            case NoiseType.tile:
                perlPos = new Vector2(position.x / tileScale + seed, position.y / tileScale + seed);
                break;
            default:
                break;
        }

        return Mathf.Clamp01(Mathf.PerlinNoise(perlPos.x, perlPos.y));
    }

    /// <summary>
    /// Generates a seed and populates the seed variable
    /// </summary>
    private void GenerateSeed()
    {
        if (seed == -1)
        {
            seed = Random.Range(0, 100000);
        }
    }
    
    /// <summary>
    /// Maps a world position to a map position
    /// </summary>
    /// <param name="worldPosition">World position to map from</param>
    /// <returns>The position within the map</returns>
    public static Vector2Int WorldToMapPosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
    }
    #endregion Converters/utility
}
