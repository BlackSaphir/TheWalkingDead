using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeSpawner : MonoBehaviour
{
    public float distance = 200.0f;
    public Transform tree;
    public LayerMask GroundLayer;

    private int mapSize;
    private float startHeight;
    private bool placeTree;
    

    void Start()
    {
        mapSize = MapGenerator.mapHeight * MapGenerator.mapWidht;
        startHeight = 205;

        if (!placeTree)
        {
            for (int x = 0; x < MapGenerator.mapWidht; x++)
            {
                for (int z = 0; z < MapGenerator.mapHeight; z++)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, startHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (hit.point.y > 15 && hit.point.y < 20)
                        {
                            Instantiate(tree, hit.point, Quaternion.identity);
                        }
                    }
                }
            }

        }
    }
}
