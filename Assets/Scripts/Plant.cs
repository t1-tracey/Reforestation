﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public PlantStage currentPlantStage;

    public bool canRespondToWater;
    public bool canRespondToSunlight;
    public bool canRespondToTime;

    public bool isWatered;
    public bool hasReceivedSunlight;

    // Add mesh / prefab properties
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

    // Getters
    public PlantStage GetPlantStage()
    {
        return currentPlantStage;
    }

    public bool IsPlantSeed()
    {
        if (GetPlantStage() == PlantStage.Seed)
        {
            return true;
        }
        return false;
    }

    public bool IsPlantSapling()
    {
        if (GetPlantStage() == PlantStage.Sapling)
        {
            return true;
        }
        return false;
    }

    public bool IsPlantSmallTree()
    {
        if (GetPlantStage() == PlantStage.SmallTree)
        {
            return true;
        }
        return false;
    }

    public bool IsPlantBigTree()
    {
        if (GetPlantStage() == PlantStage.BigTree)
        {
            return true;
        }
        return false;
    }

    public bool IsTreeLog()
    {
        if (GetPlantStage() == PlantStage.TreeLog)
        {
            return true;
        }
        return false;
    }

    public bool IsTreeStump()
    {
        if (GetPlantStage() == PlantStage.TreeStump)
        {
            return true;
        }
        return false;
    }

    // Is Alive message
    public bool IsPlantAlive()
    {
        if (IsPlantSeed() || IsPlantSapling() || IsPlantSmallTree() || IsPlantBigTree())
        {
            return true;
        }
        if (IsTreeLog() || IsTreeStump())
        {
            return false;
        }
        return false;
    }

    public bool GetCanRespondToWater()
    {
        return canRespondToWater;
    }

    public bool GetCanRespondToSunlight()
    {
        return canRespondToSunlight;
    }

    public bool GetCanRespondToTime()
    {
        return canRespondToTime;
    }

    public bool GetIsWatered()
    {
        return isWatered;
    }

    public bool GetHasReceivedSunlight()
    {
        return hasReceivedSunlight;
    }

    // Setters (internals are private)
    public void SetIsWatered(bool watered)
    {
        isWatered = watered;

        if (watered == true)
        {
            SetCanRespondToWater(false);
        }
    }

    public void SetHasReceivedSunlight(bool receivedSunlight)
    {
        hasReceivedSunlight = receivedSunlight;

        if (receivedSunlight == true)
        {
            SetCanRespondToSunlight(false);
        }
    }

    public void SetCanRespondToWater(bool canRespond)
    {
        canRespondToWater = canRespond;
    }

    public void SetCanRespondToSunlight(bool canRespond)
    {
        canRespondToSunlight = canRespond;
    }

    public void SetCanRespondToTime(bool canRespond)
    {
        canRespondToTime = canRespond;
    }

    public bool CheckIfCanRespondToTime()
    {
        if (GetIsWatered() && GetHasReceivedSunlight())
        {
            SetCanRespondToTime(true);
            return true;
        }
        SetCanRespondToTime(false);
        return false;

    }


    // Called when plant change state
    private void ChangeStage()
    {

    }

    public void SetPlantStageToSeed()
    {
        currentPlantStage = PlantStage.Seed;

        // See table in GDD
        SetCanRespondToWater(true);
        SetCanRespondToSunlight(true);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab


    }

    public void SetPlantStageToSapling()
    {
        currentPlantStage = PlantStage.Sapling;

        SetCanRespondToWater(true);
        SetCanRespondToSunlight(true);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab
    }

    public void SetPlantStageToSmallTree()
    {
        currentPlantStage = PlantStage.SmallTree;

        SetCanRespondToWater(true);
        SetCanRespondToSunlight(true);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab
    }

    public void SetPlantStageToBigTree()
    {
        currentPlantStage = PlantStage.BigTree;

        SetCanRespondToWater(false);
        SetCanRespondToSunlight(false);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab
    }

    public void SetTreeStageToTreeLog()
    {
        currentPlantStage = PlantStage.TreeLog;

        SetCanRespondToWater(false);
        SetCanRespondToSunlight(false);
        SetCanRespondToTime(true);

        //TODO: set mesh / prefab
    }

    public void SetTreeStageToTreeStump()
    {
        currentPlantStage = PlantStage.TreeStump;

        SetCanRespondToWater(false);
        SetCanRespondToSunlight(false);
        SetCanRespondToTime(true);

        //TODO: set mesh / prefab
    }

    // Check distance between plant and crosshair
    public void CheckDistanceToCrosshair()
    {
        // Vector3.Distance(plant.transform.position, player.transform.position)
    }

    // Called when water is used on plant
    public void GiveWater()
    {
        if (GetCanRespondToWater())
        {
            SetIsWatered(true);
            // Optional message for water animations, particle effects etc.

            // update can respond to time if needed
            CheckIfCanRespondToTime();
        }


    }

    // Called when sunlight is used on plant
    public void GiveSunlight()
    {
        if (GetCanRespondToSunlight())
        {
            SetHasReceivedSunlight(true);
            // Optional message for sunlight animations, particle effects etc.

            // update can respond to time if needed
            CheckIfCanRespondToTime();

        }
    }

    // Called when time speed up is used on plant
    public void SpeedUpTime()
    {
        if (GetCanRespondToTime())
        {
            if (IsPlantSeed())
            {
                SetPlantStageToSapling();
            }
            else if (IsPlantSapling())
            {
                SetPlantStageToSmallTree();
            }
            else if (IsPlantSmallTree())
            {
                SetPlantStageToBigTree();
            }
            else if (IsTreeLog() || IsTreeStump())
            {
                // Decay and disappear
                DestroyPlant();
            }
        }

    }

    // Destroy plant
    public void DestroyPlant()
    {
        Destroy(this.gameObject);
    }


}

public enum PlantStage
{
    Seed,
    Sapling,
    SmallTree,
    BigTree,
    TreeLog,
    TreeStump
}