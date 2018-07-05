﻿using UnityEngine;

/// <summary>
/// Generates and manages the world
/// </summary>
public class WorldGenerator : MonoBehaviour
{
    /// <summary>
    /// A singleton to be called by other classes
    /// </summary>
    public static WorldGenerator Instance;

    /// <summary>
    /// Scale of the generated map
    /// </summary>
    [SerializeField]
    private float scale = 100f;
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
    private Transform player;

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

    /// <summary>
    /// Generates the map at the start of the game
    /// </summary>
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        tiles = new Tile[size.x, size.y];

        GenerateSeed();
        GenerateMap();
    }

    /// <summary>
    /// Updates the map. This can be called multiple times since it dosen't instantiate any objects
    /// </summary>
    public void UpdateMap()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                UpdateTile(ref tiles[x, y], new Vector2Int(x - size.x / 2 + (int)player.position.x, y - size.y / 2 + (int)player.position.z + yOffset));
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

        tile.SetBiome(FloatToBiome(GetPerlinNoise(position)));
    }

    /// <summary>
    /// Takes a float representing something from a noisemap which will be turned into a biome
    /// </summary>
    /// <param name="noise">Noise which will be used to turn into a biome</param>
    /// <returns>Int which represents a biome</returns>
    private int FloatToBiome(float noise)
    {
        return (int)(noise * 4);
    }

    /// <summary>
    /// Get a perlin noise for use with biomes
    /// </summary>
    /// <param name="position">Position to take noise from</param>
    /// <returns>The float of the position in the noise map</returns>
    private float GetPerlinNoise(Vector2 position)
    {
        return Mathf.PerlinNoise(seed + (float)position.x / scale, seed + (float)position.y / scale);
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

}
