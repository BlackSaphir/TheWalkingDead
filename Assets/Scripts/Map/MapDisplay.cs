﻿using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{
    // get reference to set it
    public Renderer textRenderer;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] ColorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

            }
        }
    }

}
