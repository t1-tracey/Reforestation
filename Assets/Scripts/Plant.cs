using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public PlantStage plantStage;

    public bool canRespondToWater;
    public bool canRespondToSunlight;
    public bool canRespondToTime;

    public bool isWatered;
    public bool hasReceivedSunlight;

    //


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
        return plantStage;
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

    public void SetPlantStage(PlantStage newPlantStage)
    {
        plantStage = newPlantStage;
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

    public void SetPlantStageToSeed()
    {
        SetPlantStage(PlantStage.Seed);

        // See table in GDD
        SetCanRespondToWater(true);
        SetCanRespondToSunlight(true);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab


    }

    public void SetPlantStageToSapling()
    {
        SetPlantStage(PlantStage.Sapling);

        SetCanRespondToWater(true);
        SetCanRespondToSunlight(true);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab
    }

    public void SetPlantStageToSmallTree()
    {
        SetPlantStage(PlantStage.SmallTree);

        SetCanRespondToWater(true);
        SetCanRespondToSunlight(true);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab
    }

    public void SetPlantStageToBigTree()
    {
        SetPlantStage(PlantStage.BigTree);

        SetCanRespondToWater(false);
        SetCanRespondToSunlight(false);
        SetCanRespondToTime(false);

        //TODO: set mesh / prefab
    }

    public void SetTreeStageToTreeLog()
    {
        SetPlantStage(PlantStage.TreeLog);

        SetCanRespondToWater(false);
        SetCanRespondToSunlight(false);
        SetCanRespondToTime(true);

        //TODO: set mesh / prefab
    }

    public void SetTreeStageToTreeStump()
    {
        SetPlantStage(PlantStage.TreeStump);

        SetCanRespondToWater(false);
        SetCanRespondToSunlight(false);
        SetCanRespondToTime(true);

        //TODO: set mesh / prefab
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

                DestroyPlant();
            }
            else if (IsPlantSapling())
            {
                SetPlantStageToSmallTree();

                DestroyPlant();
            }
            else if (IsPlantSmallTree())
            {
                SetPlantStageToBigTree();

                DestroyPlant();
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

    // Connect internal element messages to element controller
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElementController")
        {
            //TODO: CHECK IF ACTION IS BEING PRESSED / ACTIVATED

            /*if (false)
            {
                if (other.gameObject.GetComponent<ElementController>().IsSelectedElementRain())
                {
                    GiveWater();
                }

                if (other.gameObject.GetComponent<ElementController>().IsSelectedElementSunlight())
                {
                    GiveSunlight();
                }

                if (other.gameObject.GetComponent<ElementController>().IsSelectedElementTime())
                {
                    SpeedUpTime();
                }

            }*/


        }
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