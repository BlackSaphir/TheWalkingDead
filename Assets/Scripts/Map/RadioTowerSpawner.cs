using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioTowerSpawner : MonoBehaviour
{
    public string StartRegionRadioTower = "Dark Rock";
    public float Distance = 200.0f;
    public Transform RadioTower;
    public LayerMask GroundLayer;
    public float StartHeight = 205;
    public float MapScale = 10;

    private bool placeObjects = true;
    //private float[,] treeNoiseMap;
    private int radioTowerCounter;

    private MapGenerator mapGenerator;

    void Awake()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        //treeNoiseMap = new float[MapGenerator.MapWidht, MapGenerator.MapHeight];
        //StartCoroutine(SpawnPlayer());
        StartCoroutine(SpawnRadioTower());

        radioTowerCounter = 0;

    }


    IEnumerator SpawnRadioTower()
    {
        yield return new WaitForSeconds(6);
        if (placeObjects)
        {
            for (int x = 0; x < MapGenerator.MapWidht * MapScale; --x)
            {
                for (int z = 0; z < MapGenerator.MapHeight * MapScale; --z)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (radioTowerCounter < 1)
                        {
                            if (hit.point.y > MeshGenerator.MaxHeight * mapGenerator[StartRegionRadioTower].height)
                            {
                                var radioTower = Instantiate(RadioTower, hit.point, Quaternion.identity);
                                radioTower.Rotate(-90, 0, 0);
                                ++radioTowerCounter;
                            }
                        }
                    }
                }
            }
        }
    }
}



