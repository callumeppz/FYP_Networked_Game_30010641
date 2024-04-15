using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Tilemaps;

public class ProcedularalGen : MonoBehaviour 
{
    /// <summary>
    /// This script was inspired utilisng Diving_Squids Youtube Video based on Procedural Generation released in 2021
    /// https://www.youtube.com/watch?v=puLpZIAGAcM&t=1s&ab_channel=diving_squid
    /// </summary>

    [SerializeField] int width, height; // Width and height of the generated map
    [SerializeField] float smoothness; // Smoothness factor for terrain generation
    [SerializeField] float seed; // Seed value for perlin noise
    [SerializeField] TileBase groundtile; // Ground tile to be used for rendering
    [SerializeField] Tilemap groundtilemap; // Reference to the tilemap for ground tiles
    [SerializeField] float modifier; // Modifier for terrain generation
    private int[,] map; // 2D array to store the generated map

   


    private void Start()
    {
        Generation(); // Generate the map when the game starts
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Generation(); // Generate the map when pressing space
        }
    }

    void Generation()
    {
        groundtilemap.ClearAllTiles();  // Clear existing tiles from the tilemap
        map = GenerateArray(width, height, true); // Generate an empty map
        map = TerrainGeneration(map); // Generate terrain based on perlin noise
        RenderMap(map, groundtilemap, groundtile); // Render the generated map
    }

    // Method to generate an empty array with given dimensions
    public int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = empty ? 1 : 0; // Initialise the array with 1s or 0s based on the 'empty' flag
            }
        }
        return map;
    }

    // Method to generate terrain using perlin noise
    public int[,] TerrainGeneration(int[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            int perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise((x + seed) / smoothness, seed) * height / 2);
            perlinHeight += height / 2;
            for (int y = 0; y < height; y++)
            {
                if (y <= perlinHeight)
                {
                    map[x, y] = 1; // Set terrain height
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }
        return map;
    }

    // Method to render the generated map on the tilemap
    public void RenderMap(int[,] map, Tilemap groundtilemap, TileBase tileBase)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                {
                    groundtilemap.SetTile(new Vector3Int(x, y, 0), tileBase);
                }
            }
        }
    }



}
