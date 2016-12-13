using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float Noisescale;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapHeight, mapWidth, Noisescale);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }


}
