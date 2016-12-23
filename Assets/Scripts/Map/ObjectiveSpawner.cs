using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveSpawner : MonoBehaviour
{
    public string StartRegionTree = "Beach";
    public string EndRegionTree = "Grass";
    public string StartRegionZombies = "Forest";
    public string EndRegionZombies = "Grass";
    public string StartRegionPlayer = "Dark Rock";
    public string StartRegionRadioTower = "Dark Rock";
    public float Distance = 200.0f;
    public Transform Player;
    public Transform Tree;
    public Transform Zombie;
    public Transform Barrel;
    public Transform SafeHouse;
    public Transform RescuePlatform;
    public Transform Torch;
    public Transform OilTank;
    public Transform RadioTower;
    public LayerMask GroundLayer;
    public float StartHeight = 205;
    public float MapScale = 10;
    public int TreeGap = 9;
    public int ZombieGap = 16;

    private bool b_placeTrees = true;
    private bool b_placePlayer = true;
    private bool b_placeZombies = true;
    private bool b_placeRadioTower = true;
    private int treeCounter;
    private int zombieCounter;
    private int playerCounter;
    private int radioTowerCounter;
    private float[,] treeNoiseMap;

    private MapGenerator mapGenerator;

    void Awake()
    {
        treeNoiseMap = new float[MapGenerator.MapWidht, MapGenerator.MapHeight];
        //StartCoroutine(SpawnPlayer());
        //StartCoroutine(SpawnTree());
        //StartCoroutine(SpawnRadioTower());
        //StartCoroutine(SpawnZombies());

        treeCounter = 0;
        zombieCounter = 0;
        playerCounter = 0;
        radioTowerCounter = 0;

    }

    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();

        treeNoiseMap = MapGenerator.NoiseMap;

        //spawn Player
        if (b_placePlayer)
        {
            for (int x = 0; x < MapGenerator.MapWidht * MapScale; ++x)
            {
                for (int z = 0; z < MapGenerator.MapHeight * MapScale; ++z)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (playerCounter < 1)
                        {
                            if (hit.point.y > MeshGenerator.MaxHeight * mapGenerator[StartRegionPlayer].height)
                            {
                                var player = Instantiate(Player, hit.point, Quaternion.identity);
                                ++playerCounter;
                                b_placePlayer = false;
                            }
                        }
                    }
                }
            }
        }
    }


    //spawn Trees
    //IEnumerator SpawnTree()
    //{
    //    yield return new WaitForSeconds(3);

    //    if (b_placeTrees)
    //    {
    //        for (int x = 0; x < MapGenerator.MapWidht * MapScale; x += TreeGap)
    //        {
    //            for (int z = 0; z < MapGenerator.MapHeight * MapScale; z += TreeGap)
    //            {
    //                RaycastHit hit;
    //                Vector3 position = transform.position + new Vector3(x, StartHeight, z);
    //                if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
    //                {
    //                    if (treeCounter < 2700 && Random.Range(0, 16) == 0)
    //                    {
    //                        if (hit.point.y > MeshGenerator.MaxHeight * mapGenerator[StartRegionTree].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionTree].height)
    //                        {
    //                            var tree = Instantiate(Tree, hit.point, Quaternion.identity);
    //                            tree.Rotate(-90, 0, 0);
    //                            ++treeCounter;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}


    //IEnumerator SpawnRadioTower()
    //{
    //    mapGenerator = FindObjectOfType<MapGenerator>();
    //    yield return new WaitForSeconds(6);
    //    if (b_placeRadioTower)
    //    {
    //        for (int x = 0; x < MapGenerator.MapWidht * MapScale; ++x)
    //        {
    //            for (int z = 0; z < MapGenerator.MapHeight * MapScale; ++z)
    //            {
    //                RaycastHit hit;
    //                Vector3 position = transform.position + new Vector3(x, StartHeight, z);
    //                if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
    //                {
    //                    if (radioTowerCounter < 1)
    //                    {
    //                        if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionRadioTower].height)
    //                        {
    //                            var radioTower = Instantiate(RadioTower, hit.point, Quaternion.identity);
    //                            radioTower.Rotate(-90, 0, 0);
    //                            ++radioTowerCounter;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}






//    // spawn Zombies
//    IEnumerator SpawnZombies()
//    {
//        yield return new WaitForSeconds(15);
//        if (b_placeZombies)
//        {
//            for (int x = 0; x < MapGenerator.MapWidht * MapScale; x += ZombieGap)
//            {
//                for (int z = 0; z < MapGenerator.MapHeight * MapScale; z += ZombieGap)
//                {
//                    RaycastHit hit;
//                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
//                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
//                    {
//                        if (zombieCounter < 200 && Random.Range(0, 20) == 0)
//                        {
//                            if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionZombies].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionZombies].height)
//                            {
//                                var zombie = Instantiate(Zombie, hit.point, Quaternion.identity);
//                                ++zombieCounter;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }

//}