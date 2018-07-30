using UnityEngine;

/// <summary>
/// Generates and manages the world
/// </summary>
public class MapGenerator : MonoBehaviour
{
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
    /// Prefab of the first tile
    /// </summary>
    [Space]
    [SerializeField]
    private Tile tilePrefab;

    /// <summary>
    /// Updates the map. This can be called multiple times since it dosen't instantiate any objects
    /// </summary>
    public void UpdateMap(ref Tile[,] tiles, Vector2Int size, Vector2Int lowerLeftCornor)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                UpdateTile(ref tiles[x, y], new Vector2Int(x, y));
            }
        }
    }

    /// <summary>
    /// Generates a map from new
    /// </summary>
    public Tile[,] GenerateMap(Vector2Int size)
    {
        Tile[,] tiles = new Tile[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                tiles[x, y] = CreateTile(new Vector2Int(x, y));
            }
        }
        return tiles;
    }

    /// <summary>
    /// Creates a new tile. WARNING: does not set the position of the tile you will have to do that yourself through UpdateTile
    /// </summary>
    /// <param name="position">The tile position within the array</param>
    private Tile CreateTile(Vector2Int position)
    {
        //Start by instantiating and setting the basic values of the tile
        Tile tile = Instantiate(tilePrefab.gameObject, transform).GetComponent<Tile>();
        tile.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
        return tile;
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
    public void GenerateSeed()
    {
        if (seed == -1)
        {
            seed = Random.Range(0, 100000);
        }
    }

}
