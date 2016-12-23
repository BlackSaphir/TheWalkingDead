using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioTowerSpawner : MonoBehaviour
{
    //public string StartRegionRadioTower = "Snow";
    public float Distance = 500.0f;
    public Transform RadioTower;
    public Transform Base;
    public Transform RescuePlatform;
    public LayerMask GroundLayer;
    public float StartHeight = 305;
    public float MapScale = 10;

    private bool b_spawnRadioTower = true;
    //private float[,] treeNoiseMap;
    private int radioTowerCounter;

    private MapGenerator mapGenerator;

    void Awake()
    {
        //treeNoiseMap = new float[MapGenerator.MapWidht, MapGenerator.MapHeight];
        StartCoroutine(SpawnRadioTower());

        radioTowerCounter = 0;

    }

    //spawn RadioTower
    IEnumerator SpawnRadioTower()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        yield return new WaitForSeconds(6);
        if (b_spawnRadioTower)
        {
            for (int z = 0; Mathf.Abs(z) < MapGenerator.MapWidht * MapScale; z--)
            {
                for (int x = 0; x < MapGenerator.MapHeight * MapScale; ++x)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (radioTowerCounter < 1)
                        {
                            if (hit.point.y == MeshGenerator.MaxHeight /** mapGenerator[StartRegionRadioTower].height*/)
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



