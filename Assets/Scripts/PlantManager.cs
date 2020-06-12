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

    public void ChangePlantStage(Plant oldPlant, PlantStage newPlantStage, bool canRespondToWater, bool canRespondToSunlight, bool canRespondToTime)
    {

        Plant plant = new Plant();

        // place new plant in position of old plant, quaternion.identity
        plant.transform.position = oldPlant.transform.position;

        oldPlant.gameObject.SetActive(false);
        Instantiate(plant, oldPlant.transform.position, Quaternion.identity);

        // Instanstiate
        // Disable old plant

        plant.SetPlantStage(newPlantStage);

        plant.SetCanRespondToWater(canRespondToWater);
        plant.SetCanRespondToSunlight(canRespondToSunlight);
        plant.SetCanRespondToTime(canRespondToTime);

        oldPlant.DestroyPlant();
    }

    public void SpawnPlant()
    {


        if (XCI.GetButton(XboxButton.A))
        {
            Debug.Log("A is pressed");

            if (canSpawn == true)
            {
                Vector3 spawnPosition = ChooseRandomSpawnLocation();

                GameObject GO = Instantiate(bigTree, spawnPosition, Quaternion.identity) as GameObject;
                GO.GetComponent<Plant>().SetPlantStageToBigTree();

                canSpawn = false;

                Invoke("ResetSpawnTime", timeBetweenPlantSpawn);
            }


        }


    }

    // Deprecated in favour of Random.insideUnitSphere
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
