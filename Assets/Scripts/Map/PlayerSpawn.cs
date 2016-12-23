using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
   
    public float Distance = 900;
    public float StartHeight = 321;
    public float MapScale = 10;
   
    private float[,] treeNoiseMap;
    private MapGenerator mapGenerator;

    void Awake()
    {
        treeNoiseMap = new float[MapGenerator.MapWidht, MapGenerator.MapHeight];
    }

    //spawn Player
    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();

        Vector3 position = transform.position + new Vector3(Random.Range(-700, 700), StartHeight, Random.Range(-700, 700));
        this.transform.position = new Vector3(position.x, position.y, position.z);
    }
}
