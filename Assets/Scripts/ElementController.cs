﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ElementController : MonoBehaviour
{

    public Element selectedElement;
    public GameObject player;

    public GameObject cloud;
    public GameObject sunlight;
    public GameObject hourglass;

    public GameObject rainParticleSystem;
    public GameObject sunlightParticleSystem;
    public GameObject timeParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        selectedElement = Element.Seed;
    }

    // Update is called once per frame
    void Update()
    {

        HideOrShowRain();
        HideOrShowSunlight();
        HideOrShowTime();

        DisplayParticles();

    }

    // Hide or show cloud above player
    public void HideOrShowRain()
    {
        if (IsSelectedElementRain())
        {
            cloud.SetActive(true);
        }
        else
        {
            cloud.SetActive(false);
        }

    }

    // Hide or show sunlight above player
    public void HideOrShowSunlight()
    {
        if (IsSelectedElementSunlight())
        {
            sunlight.SetActive(true);
        }
        else
        {
            sunlight.SetActive(false);
        }
    }

    // Hide or show hourglass/time object above player
    public void HideOrShowTime()
    {
        if (IsSelectedElementTime())
        {
            // hourglass.SetActive(true);
        }
        else
        {
            hourglass.SetActive(false);
        }
    }

    public void EnableRainParticles()
    {
        rainParticleSystem.SetActive(true);
    }

    public void DisableRainParticles()
    {
        rainParticleSystem.SetActive(false);
    }

    public void EnableSunlightParticles()
    {
        sunlightParticleSystem.SetActive(true);
    }

    public void DisableSunlightParticles()
    {
        sunlightParticleSystem.SetActive(false);
    }

    public void EnableTimeParticles()
    {
        //timeParticleSystem.SetActive(true);
    }

    public void DisableTimeParticles()
    {
        timeParticleSystem.SetActive(false);
    }

    // Display particle effects depending on selected element
    public void DisplayParticles()
    {
        if (XCI.GetAxis(XboxAxis.RightTrigger) > 0.1f)
        {
            if (IsSelectedElementRain())
            {

                EnableRainParticles();

            }
            else
            {
                DisableRainParticles();
            }
            if (IsSelectedElementSunlight())
            {
                EnableSunlightParticles();

            }
            else
            {
                DisableSunlightParticles();
            }
            if (IsSelectedElementTime())
            {
                EnableTimeParticles();
            }
            else
            {
                DisableTimeParticles();
            }
        }
        else
        {
            DisableRainParticles();
            DisableSunlightParticles();
            DisableTimeParticles();
        }
    }

    // React when button is pushed while rain is selected
    // Send rain message to plants

    // React when button is pushed while sunlight is selected
    // Send sun message to plants

    // React when button is pushed while time speed is selected
    // Send timeSpeed message to plants
    // React when UI summoning time speed is selected

    // Setters and getters
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

    public bool IsSelectedElementSeed()
    {
        if (selectedElement == Element.Seed)
            return true;
        return false;
    }

}

public enum Element
{
    Seed,
    Rain,
    Sunlight,
    Time,
    None
}