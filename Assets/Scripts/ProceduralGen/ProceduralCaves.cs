using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Tilemaps;

public class ProcedularalCaves : MonoBehaviour 
{

    /// <summary>
    /// This script was inspired utilisng Diving_Squids Youtube Video based on Procedural Cave Generation released in 2021
    /// https://www.youtube.com/watch?v=lhWjEd8I4fM&ab_channel=diving_squid
    /// </summary>



    [SerializeField] int width, height; // Width and height of the generated map
    [SerializeField] float smoothness; // Smoothness factor for terrain generation
    [SerializeField] float seed; // Seed value for perlin noise
    [SerializeField] TileBase groundtile, cavetile;  // Ground tile to be used for rendering
    [SerializeField] Tilemap groundtilemap, cavetilemap; 
    int[,] map;  // 2D array to store the generated map, could be used for saving in future work


    [Range(0, 1)]
    [SerializeField] float modifier;

    private void Start()
    {
        Generation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Generation();
        }
    }

    void Generation()
    {
        seed = Random.Range(-10000, 10000);
        clearmap();
        groundtilemap.ClearAllTiles();
        map = GenerateArray(width, height, true);
        map = TerrainGeneration(map);
        RenderMap(map, groundtilemap, cavetilemap, groundtile, cavetile);
    }

    public int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = empty ? 1 : 0;
            }
        }
        return map;
    }

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
                    // map[x, y] = 1;
                    int caveValue = Mathf.RoundToInt(Mathf.PerlinNoise((x*modifier) + seed, (y * modifier) + seed));
                    map[x, y] = (caveValue == 1) ? 2 : 1;
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }
        return map;
    }


    public void RenderMap(int[,] map, Tilemap groundtilemap, Tilemap cavetilemap, TileBase tileBase, TileBase cavetile)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                {
                    groundtilemap.SetTile(new Vector3Int(x, y, 0), tileBase);
                }
                else if (map[x, y] == 2)
                {
                    cavetilemap.SetTile(new Vector3Int(x,y, 0), cavetile);
                }
            }
        }
    }


    void clearmap()
    {
        groundtilemap.ClearAllTiles();
        cavetilemap.ClearAllTiles();
    }
}
