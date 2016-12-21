using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeSpawner : MonoBehaviour
{
    public string StartRegion = "Forest";
    public string EndRegion = "Grass";
    public float Distance = 200.0f;
    public Transform Tree;
    public LayerMask GroundLayer;
    public float StartHeight = 205;
    public float MapScale = 10;
    public int TreeGap = 9;

    private bool placeTrees = true;
    private float[,] treeNoiseMap;
    private int counter;

    private MapGenerator mapGenerator;

    void Start()
    {
        treeNoiseMap = new float[MapGenerator.MapWidht, MapGenerator.MapHeight];
        StartCoroutine(PlantTree());

        counter = 0;

        mapGenerator = FindObjectOfType<MapGenerator>();
    }


    IEnumerator PlantTree()
    {
        treeNoiseMap = MapGenerator.NoiseMap;
        yield return new WaitForSeconds(3);

        if (placeTrees)
        {
            for (int x = 0; x < MapGenerator.MapWidht * MapScale; x += TreeGap)
            {
                for (int z = 0; z < MapGenerator.MapHeight * MapScale; z += TreeGap)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                       

                        if (counter < 3000 && Random.Range(0, 16) == 0)
                        {
                            if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegion].height && hit.point.y <=  MeshGenerator.MaxHeight * mapGenerator[EndRegion].height)
                            {
                                var tree = Instantiate(Tree, hit.point, Quaternion.identity);
                                tree.Rotate(-90, 0, 0);
                                ++counter;
                            }
                        }
                    }
                }
            }
        }

    }
}

