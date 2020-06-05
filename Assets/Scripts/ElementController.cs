using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{

    public Element selectedElement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // React when button is pushed while rain is selected
    // Send rain message to plants

    // React when button is pushed while sunlight is selected
    // Send sun message to plants

    // React when button is pushed while time speed is selected
    // Send timeSpeed message to plants
    // React when UI summoning time speed is selected

    //TODO: Check distance between plant and crosshair
    public void CheckDistanceToCrosshair()
    {
        // Vector3.Distance(plant.transform.position, player.transform.position)
    }

    public void SwitchElement()
    {

    }
}

public enum Element
{
    Rain,
    Sunlight,
    Time
}