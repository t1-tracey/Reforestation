using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{

    public Element selectedElement;
    public GameObject player;

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

    public void SetSelectedElement(Element newElement)
    {
        selectedElement = newElement;
    }

    public bool IsSelectedElementRain()
    {
        if (selectedElement == Element.Rain)
            return true;
        return false;
    }

    public bool IsSelectedElementSunlight()
    {
        if (selectedElement == Element.Sunlight)
            return true;
        return false;
    }

    public bool IsSelectedElementTime()
    {
        if (selectedElement == Element.Time)
            return true;
        return false;
    }

}

public enum Element
{
    Rain,
    Sunlight,
    Time
}