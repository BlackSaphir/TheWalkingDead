using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveSpawner : MonoBehaviour
{
    public string StartRegionTree = "Forest";
    public string EndRegionTree = "Grass";
    public string StartRegionZombies = "Forest";
    public string EndRegionZombies = "Grass";
    public float Distance = 200.0f;
    public Transform Tree;
    public Transform Zombie;
    public Transform Barrel;
    public Transform SafeHouse;
    public Transform RadioTower;
    public Transform OilTank;
    public LayerMask GroundLayer;
    public float StartHeight = 205;
    public float MapScale = 10;
    public int TreeGap = 9;

    private bool placeObjects = true;
    private float[,] treeNoiseMap;
    private int treeCounter;
    private int zombieCounter;

    private MapGenerator mapGenerator;

    void Start()
    {
        treeNoiseMap = new float[MapGenerator.MapWidht, MapGenerator.MapHeight];
        StartCoroutine(SpawnTree());
        StartCoroutine(SpawnZombies());

        treeCounter = 0;
        zombieCounter = 0;

        mapGenerator = FindObjectOfType<MapGenerator>();
    }


    IEnumerator SpawnTree()
    {
        treeNoiseMap = MapGenerator.NoiseMap;
        yield return new WaitForSeconds(1);

        if (placeObjects)
        {
            for (int x = 0; x < MapGenerator.MapWidht * MapScale; x += TreeGap)
            {
                for (int z = 0; z < MapGenerator.MapHeight * MapScale; z += TreeGap)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (treeCounter < 2800 && Random.Range(0, 16) == 0)
                        {
                            if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionTree].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionTree].height)
                            {
                                var tree = Instantiate(Tree, hit.point, Quaternion.identity);
                                tree.Rotate(-90, 0, 0);
                                ++treeCounter;
                            }
                        }
                    }
                }
            }
        }
    }


    IEnumerator SpawnZombies()
    {
        treeNoiseMap = MapGenerator.NoiseMap;
        yield return new WaitForSeconds(2);
        if (placeObjects)
        {
            for (int x = 0; x < MapGenerator.MapWidht * MapScale; x += TreeGap)
            {
                for (int z = 0; z < MapGenerator.MapHeight * MapScale; z += TreeGap)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (zombieCounter < 20 && Random.Range(0, 30) == 0)
                        {
                            if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionZombies].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionZombies].height)
                            {
                                var zombie = Instantiate(Zombie, hit.point, Quaternion.identity);
                                ++zombieCounter;
                            }
                        }
                    }
                }
            }
        }
    }
}


