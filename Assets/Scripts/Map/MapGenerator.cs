using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh };
    public DrawMode MyDrawMode;
    //const int mapChunkSize = 241;
    [Range(25, 60)]
    public float Noisescale;
    [Range(1, 15)]
    public int Octaves;
    [Range(0.1f, 0.3f)]
    public float Persistance;
    [Range(1, 9)]
    public float Lacunarity;
    [Range(1, 150)]
    public int Seed;
    [Range(150, 320)]
    public float MeshHeightMultiplier;
    public TerrainType[] Regions;
    public Vector2 Offset;
    public AnimationCurve MeshHeightCurve;
    public bool AutoUpdate;

    public static int MapWidht = 180;
    public static int MapHeight = 180;
    public static float[,] NoiseMap;

    void Awake()
    {

        Noisescale = Random.Range(25, 60);
        Octaves = Random.Range(1, 15);
        Persistance = Random.Range(0.1f, 0.3f);
        Lacunarity = Random.Range(1, 9);
        Seed = Random.Range(1, 150);
        MeshHeightMultiplier = Random.Range(150, 320);
        GenerateMap();
    }

    public void GenerateMap()
    {
        NoiseMap = Noise.GenerateNoiseMap(MapWidht, MapHeight, Noisescale, Octaves, Persistance, Lacunarity, Seed, Offset);

        // save all colors
        Color[] colorMap = new Color[MapWidht * MapHeight];
        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidht; x++)
            {
                float currentHeight = MeshHeightCurve.Evaluate(NoiseMap[x, y]);
                for (int i = 0; i < Regions.Length; i++)
                {
                    if (currentHeight <= Regions[i].height)
                    {
                        colorMap[y * MapWidht + x] = Regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (MyDrawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(NoiseMap));
        }
        else if (MyDrawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, MapWidht, MapHeight));

        }
        else if (MyDrawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(NoiseMap, MeshHeightMultiplier, MeshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, MapWidht, MapHeight));
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, MapWidht, MapHeight));
        }
    }

    void OnValidate()
    {
        if (MapWidht < 1)
        {
            MapWidht = 1;
        }

        if (MapHeight < 1)
        {
            MapHeight = 1;
        }

        if (Octaves < 0)
        {
            Octaves = 1;
        }

        if (Lacunarity < 1)
        {
            Lacunarity = 1;
        }
    }

    /// <summary>
    /// Indexer for TerrainTypes
    /// </summary>
    /// <param name="name">The name of the TerrainType.</param>
    /// <returns></returns>
    public TerrainType this[string name]
    {
        get
        {
            foreach (var type in Regions)
            {
                if (type.name == name)
                {
                    return type;
                }
            }
            return default(TerrainType);
        }
    }
}

// Region Settings 
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}

