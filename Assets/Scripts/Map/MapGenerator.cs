using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public enum drawMode { noiseMap, colorMap, Mesh };
    public drawMode DrawMode;
    public int mapWidht;
    public int mapHeight;
    [Range(25, 100)]
    public float noisescale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;
    public TerrainType[] regions;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;
    public bool autoUpdate;

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
            octaves = 0;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
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