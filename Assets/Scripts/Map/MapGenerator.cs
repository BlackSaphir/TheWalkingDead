using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public enum drawMode { noiseMap, colorMap, Mesh };
    public drawMode DrawMode;
    //const int mapChunkSize = 241;
    [Range(25, 60)]
    public float noisescale;
    [Range(1, 15)]
    public int octaves;
    [Range(0.0f, 0.2f)]
    public float persistance;
    [Range(1, 9)]
    public float lacunarity;
    [Range(1, 150)]
    public int seed;
    [Range(140, 270)]
    public float meshHeightMultiplier;
    public Vector2 offset;
    public TerrainType[] regions;
    public AnimationCurve meshHeightCurve;
    public Transform tree;
    public bool autoUpdate;

    private int mapWidht = 180;
    private int mapHeight = 180;


    void Start()
    {

        noisescale = Random.Range(25, 60);
        octaves = Random.Range(1, 15);
        persistance = Random.Range(0.0f, 0.2f);
        lacunarity = Random.Range(1, 9);
        seed = Random.Range(1, 150);
        meshHeightMultiplier = Random.Range(140, 270);
        GenerateMap();
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidht, mapHeight, noisescale, octaves, persistance, lacunarity, seed, offset);

        // save all colors
        Color[] colorMap = new Color[mapWidht * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidht; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidht + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (DrawMode == drawMode.noiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (DrawMode == drawMode.colorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidht, mapHeight));

        }
        else if (DrawMode == drawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, mapWidht, mapHeight));
        }
    }

    void OnValidate()
    {
        if (mapWidht < 1)
        {
            mapWidht = 1;
        }

        if (mapHeight < 1)
        {
            mapHeight = 1;
        }

        if (octaves < 0)
        {
            octaves = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
    }




    //public void PlaceTree()
    //{
    //    if (regions[].height == 0.45f)
    //    {
    //        for (int x = 0; x <; x++)
    //        {

    //        }
    //        Instantiate(tree, new Vector3(), Quaternion.identity);
    //    }

    //}
}

// Region Settings 
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}

