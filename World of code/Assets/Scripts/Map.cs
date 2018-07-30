using UnityEngine;

/// <summary>
/// Contains the variables needed for the map
/// </summary>
[RequireComponent(typeof(MapGenerator))]
public class Map : MonoBehaviour
{
    /// <summary>
    /// Singleton for map
    /// </summary>
    public static Map Instance;
    /// <summary>
    /// Map generator generating this map
    /// </summary>
    public MapGenerator generator;

    /// <summary>
    /// Reference to the player so we can show the world around the player
    /// </summary>
    [SerializeField]
    private PlayerMovement playerMovement;

    /// <summary>
    /// Size of area around player that has tiles drawn
    /// </summary>
    [SerializeField]
    private Vector2Int size;
    /// <summary>
    /// The array of the tiles making up the map
    /// </summary>
    private Tile[,] tiles;
    /// <summary>
    /// Offset for the y axis in the area drawn around the player
    /// </summary>
    [SerializeField]
    private int yOffset;

    /// <summary>
    /// Manages the singleton at the start of the game
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
    /// Generates the map
    /// </summary>
    private void Start()
    {
        generator = GetComponent<MapGenerator>();

        generator.GenerateSeed();
        tiles = generator.GenerateMap(size);
        UpdateMap();
    }

    /// <summary>
    /// Update the map every time the player moves
    /// </summary>
    public void UpdateMap()
    {
        generator.UpdateMap(ref tiles, size, playerMovement.playerPosition - new Vector2Int(size.x / 2, size.y / 2));
    }

}
