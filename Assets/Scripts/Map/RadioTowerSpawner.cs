using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioTowerSpawner : MonoBehaviour
{
    public string StartRegionBase = "Beach";
    public string StartRegionBattery = "Beach";
    public string EndRegionBattery = "Snow";
    public float Distance = 500.0f;
    public Transform RadioTower;
    public Transform Base;
    public Transform BatteryBarrel;
    public Transform CableBarrel;
    public Transform EmptyBarrel1;
    public Transform EmptyBarrel2;
    public Transform EmptyBarrel3;
    public Transform KeyBarrel;
    public Transform LightBarrel;
    public Transform PetrolBarrel;
    public Transform RepairBarrel;
    public Transform MobileRadioBarrel;
    public LayerMask GroundLayer;
    public float StartHeight = 305;
    public float MapScale = 10;
    public int EmptyBarrelGap1 = 30;
    public int EmptyBarrelGap2 = 60;
    public int PetrolBarrelGap = 100;

    private bool b_spawnRadioTower = true;
    private bool b_spawnBase = true;
    private bool b_spawnSinglebarrel = true;
    private bool b_spawnEmptyBarrel1 = true;
    private bool b_spawnEmptyBarrel2 = true;
    private bool b_spawnPetrolBarrel = true;
    private bool b_spawnLightBarrel = true;
    private bool b_spawnCableBarrel = true;
    private bool b_spawnBatteryBarrel = true;
    private bool b_spawnKeyBarrel = true;
    private bool b_spawnRepairBarrel = true;
    private bool b_spawnmobileBarrel = true;

    //private float[,] treeNoiseMap;

    private int radioTowerCounter;
    private int baseCounter;
    private int batteryBarrelCounter;
    private int cableBarrelCounter;
    private int emptyBarrelCounter1;
    private int emptyBarrelCounter2;
    private int emptyBarrelCounter3;
    private int keyBarrelCounter;
    private int lightBarrelCounter;
    private int petroBarrelCounter;
    private int repairBarrelCounter;
    private int mobileRadioCounter;


    private MapGenerator mapGenerator;

    void Awake()
    {
        //treeNoiseMap = new float[MapGenerator.MapWidht, MapGenerator.MapHeight];

        radioTowerCounter = 0;
        baseCounter = 0;
        batteryBarrelCounter = 0;
        cableBarrelCounter = 0;
        emptyBarrelCounter1 = 0;
        emptyBarrelCounter2 = 0;
        emptyBarrelCounter3 = 0;
        keyBarrelCounter = 0;
        lightBarrelCounter = 0;
        petroBarrelCounter = 0;
        repairBarrelCounter = 0;
        mobileRadioCounter = 0;

        StartCoroutine(SpawnRadioTower());
        StartCoroutine(SpawnBase());
        StartCoroutine(SpawnEmptyBarrel1());
        StartCoroutine(SpawnEmptyBarrel2());
        StartCoroutine(SpawnPetrolBarrel());
        StartCoroutine(SpawnCableBarrel());
        StartCoroutine(SpawnLightBarrel());
        StartCoroutine(SpawnKeyBarrel());
        StartCoroutine(SpawnRepairBarrel());
        StartCoroutine(SpawnMobileBarrel());
        StartCoroutine(SpawnBatteryBarrel());

        mapGenerator = FindObjectOfType<MapGenerator>();


    }

    //spawn RadioTower
    IEnumerator SpawnRadioTower()
    {
        yield return new WaitForSeconds(9);
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
                            if (hit.point.y == MeshGenerator.MaxHeight)
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



    IEnumerator SpawnBase()
    {
        yield return new WaitForSeconds(8);
        if (b_spawnBase)
        {
            for (int z = 0; Mathf.Abs(z) < MapGenerator.MapWidht * MapScale; z--)
            {
                for (int x = 0; x < MapGenerator.MapHeight * MapScale; ++x)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (baseCounter < 1)
                        {
                            if (hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height /*&& hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBase].height*/ )
                            {
                                var playerBase = Instantiate(Base, hit.point, Quaternion.identity);
                                ++baseCounter;
                            }
                        }
                    }
                }
            }
        }
    }



    // spawn Barrels
    IEnumerator SpawnBatteryBarrel()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnBatteryBarrel)
        {
            RaycastHit hit;
            Vector3 position = transform.position + new Vector3(Random.Range(-700, 700), StartHeight, Random.Range(-700, 700));
            if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
            {
                if (batteryBarrelCounter < 1)
                {
                    if (hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                    {
                        var playerBase = Instantiate(BatteryBarrel, hit.point, Quaternion.identity);
                        ++batteryBarrelCounter;
                    }
                }
            }
        }
    }



    IEnumerator SpawnCableBarrel()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnCableBarrel)
        {
            RaycastHit hit;
            Vector3 position = transform.position + new Vector3(Random.Range(-700, 700), StartHeight, Random.Range(-700, 700));
            if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
            {
                if (cableBarrelCounter < 1)
                {
                    if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                    {
                        var playerBase = Instantiate(CableBarrel, hit.point, Quaternion.identity);
                        ++cableBarrelCounter;

                    }
                }
            }
        }
    }

    IEnumerator SpawnKeyBarrel()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnKeyBarrel)
        {
            RaycastHit hit;
            Vector3 position = transform.position + new Vector3(Random.Range(-700, 700), StartHeight, Random.Range(-700, 700));
            if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
            {
               
                    if (keyBarrelCounter < 1)
                    {
                        if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                        {
                            var playerBase = Instantiate(KeyBarrel, hit.point, Quaternion.identity);

                            ++keyBarrelCounter;
                        }
                    }
                
            }
        }
    }


    IEnumerator SpawnMobileBarrel()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnRepairBarrel)
        {
            RaycastHit hit;
            Vector3 position = transform.position + new Vector3(Random.Range(-700, 700), StartHeight, Random.Range(-700, 700));
            if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
            {
                if (mobileRadioCounter < 1)
                {
                    if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                    {
                        var playerBase = Instantiate(MobileRadioBarrel, hit.point, Quaternion.identity);

                        ++mobileRadioCounter;
                    }
                }
            }
        }
    }



    IEnumerator SpawnRepairBarrel()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnRepairBarrel)
        {
            RaycastHit hit;
            Vector3 position = transform.position + new Vector3(Random.Range(-700, 700), StartHeight, Random.Range(-700, 700));
            if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
            {
                if (repairBarrelCounter < 1)
                {
                    if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                    {
                        var playerBase = Instantiate(RepairBarrel, hit.point, Quaternion.identity);
                        ++repairBarrelCounter;

                    }
                }
            }
        }
    }





    IEnumerator SpawnLightBarrel()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnLightBarrel)
        {
            RaycastHit hit;
            Vector3 position = transform.position + new Vector3(Random.Range(-700, 700), StartHeight, Random.Range(-700, 700));
            if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
            {
                if (lightBarrelCounter < 1)
                {
                    if (hit.point.y >= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                    {
                        var playerBase = Instantiate(LightBarrel, hit.point, Quaternion.identity);
                        ++lightBarrelCounter;
                    }
                }
            }
        }
    }


    IEnumerator SpawnEmptyBarrel1()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnEmptyBarrel1)
        {
            for (int z = 0; Mathf.Abs(z) < MapGenerator.MapWidht * MapScale; z -= EmptyBarrelGap1)
            {
                for (int x = 0; x < MapGenerator.MapHeight * MapScale; x += EmptyBarrelGap1)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (emptyBarrelCounter1 < 5)
                        {
                            if (hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                            {
                                var emptyBarrel = Instantiate(EmptyBarrel1, hit.point, Quaternion.identity);
                                ++emptyBarrelCounter1;
                            }
                        }
                    }
                }
            }
        }
    }



    IEnumerator SpawnEmptyBarrel2()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnEmptyBarrel2)
        {
            for (int z = 0; Mathf.Abs(z) < MapGenerator.MapWidht * MapScale; z -= EmptyBarrelGap2)
            {
                for (int x = 0; x < MapGenerator.MapHeight * MapScale; x += EmptyBarrelGap2)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (emptyBarrelCounter2 < 5)
                        {
                            if (hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                            {
                                var emptyBarrel2 = Instantiate(EmptyBarrel2, hit.point, Quaternion.identity);
                                ++emptyBarrelCounter2;
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator SpawnPetrolBarrel()
    {
        yield return new WaitForSeconds(6);
        if (b_spawnPetrolBarrel)
        {
            for (int z = 0; Mathf.Abs(z) < MapGenerator.MapWidht * MapScale; z -= PetrolBarrelGap)
            {
                for (int x = 0; x < MapGenerator.MapHeight * MapScale; x += PetrolBarrelGap)
                {
                    RaycastHit hit;
                    Vector3 position = transform.position + new Vector3(x, StartHeight, z);
                    if (Physics.Raycast(position, Vector3.down, out hit, Distance, GroundLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (petroBarrelCounter < 5)
                        {
                            if (hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[StartRegionBattery].height && hit.point.y <= MeshGenerator.MaxHeight * mapGenerator[EndRegionBattery].height)
                            {
                                var petrolbarrel = Instantiate(PetrolBarrel, hit.point, Quaternion.identity);
                                ++petroBarrelCounter;
                            }
                        }
                    }
                }
            }
        }
    }

}




