using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlantManager : MonoBehaviour
{

    // Store prefabs for plants
    public GameObject seed;
    public GameObject sapling;
    public GameObject smallTree;
    public GameObject bigTree;
    public GameObject treeLog;
    public GameObject treeDead;

    public GameObject player;

    private float radius;

    public float timeBetweenPlantSpawn = 0.2f;
    private bool canSpawn = true;

    public float spawnHeight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        SphereCollider sc;
        sc = player.GetComponent<SphereCollider>();
        radius = sc.radius;
    }

    // Update is called once per frame
    void Update()
    {


        SpawnPlant();
    }

    public void ChangePlantStage(GameObject oldPlant)
    {


        // place new plant in position of old plant, quaternion.identity
        Vector3 spawnPosition = oldPlant.transform.position;

        if (oldPlant.GetComponent<Plant>().IsPlantSeed())
        {
            GameObject GO = Instantiate(sapling, spawnPosition, Quaternion.identity) as GameObject;
            GO.GetComponent<Plant>().SetPlantStageToSapling();
            oldPlant.GetComponent<Plant>().DestroyPlant();
        }
        else if (oldPlant.GetComponent<Plant>().IsPlantSapling())
        {
            GameObject GO = Instantiate(smallTree, spawnPosition, Quaternion.identity) as GameObject;
            GO.GetComponent<Plant>().SetPlantStageToSmallTree();
            oldPlant.GetComponent<Plant>().DestroyPlant();
        }
        else if (oldPlant.GetComponent<Plant>().IsPlantSmallTree())
        {
            GameObject GO = Instantiate(bigTree, spawnPosition, Quaternion.identity) as GameObject;
            GO.GetComponent<Plant>().SetPlantStageToBigTree();
            oldPlant.GetComponent<Plant>().DestroyPlant();
        }
        else if (oldPlant.GetComponent<Plant>().IsTreeLog() || oldPlant.GetComponent<Plant>().IsTreeStump())
        {
            oldPlant.GetComponent<Plant>().DestroyPlant();
        }
        
    }

    public void SpawnPlant()
    {


        if (XCI.GetButton(XboxButton.A))
        {
            Debug.Log("A is pressed");

            if (canSpawn == true)
            {
                Vector3 spawnPosition = ChooseRandomSpawnLocation();

                GameObject GO = Instantiate(seed, spawnPosition, Quaternion.identity) as GameObject;
                GO.GetComponent<Plant>().SetPlantStageToSeed();

                canSpawn = false;

                Invoke("ResetSpawnTime", timeBetweenPlantSpawn);
            }


        }


    }

    // Maybe deprecate in favour of Random.insideUnitSphere
    public Vector3 ChooseRandomSpawnLocation()
    {
        //Pick random location within radius for the seed to spawn
        Vector3 spawnPosition;
        Vector3 spawnDisplacement;

        // Use pythagorean theorem
        float magnitude = Random.Range(0.0f, radius);
        float horizontal = Random.Range(0.0f, magnitude);

        float vertical = Mathf.Sqrt((magnitude * magnitude) - (horizontal * horizontal));

        // make vertical component randomly +-
        if (Random.Range(0,2) == 1)
        {
            vertical *= -1;
        }


        spawnDisplacement = new Vector3(horizontal, 0, vertical);
        // spawnDisplacement = Random.insideUnitSphere * radius;

        spawnPosition = new Vector3(player.transform.position.x, spawnHeight, player.transform.position.z) + spawnDisplacement;

        return spawnPosition;
    }



    public void ResetSpawnTime()
    {
        canSpawn = true;
    }
}
