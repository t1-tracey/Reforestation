using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{

    // Store prefabs for plants
    public GameObject seed;
    public GameObject sapling;
    public GameObject smallTree;
    public GameObject bigTree;
    public GameObject treeLog;
    public GameObject treeDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    // Spawn seed, in location of player crosshair (randomised, to distance of radius)

    //TODO: Check distance between plant and crosshair
    // Or should i use a collider/trigger
    public void CheckDistanceToCrosshair()
    {
        // Vector3.Distance(plant.transform.position, player.transform.position)
    }

    public void SpawnPlant()
    {

    }
}
