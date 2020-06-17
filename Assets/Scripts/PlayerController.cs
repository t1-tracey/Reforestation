using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{

    // Store player rigidbody and Xbox controller
    public Rigidbody rigidBody;
    public XboxController controller;

    // How fast the player can move
    public float movementSpeed = 60;
    public float maxSpeed = 5;

    // Store rotation parameters
    public Vector3 previousRotationDirection = Vector3.forward;
    public float turnSpeed = 60;

    public ElementController elementController;
    public PlantManager plantManager;

    private bool xPressed;
    private bool yPressed;
    private bool bPressed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        //Debug.Log("fixed update");
        MovePlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        CheckActionButton();
    }


    // Translate the player camera along the X and Z axes
    private void MovePlayer()
    {
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
        float axisZ = XCI.GetAxis(XboxAxis.LeftStickY, controller);

        Vector3 movement = new Vector3(axisX, 0, axisZ);
        //Debug.Log(movement*movementSpeed);

        // rigidBody.velocity = movement * movementSpeed;
        rigidBody.AddForce(movement * movementSpeed);

        // Ensure player can't go faster than the max speed
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }

        if (movement.magnitude < 0.1f)
        {
            rigidBody.velocity = Vector3.zero;
        }


    }

    // Orbit camera around player crosshair
    private void RotatePlayer()
    {
        float rotateAxisX = XCI.GetAxis(XboxAxis.RightStickX, controller);
        float rotateAxisZ = XCI.GetAxis(XboxAxis.RightStickY, controller);

        Vector3 directionVector = new Vector3(rotateAxisX, 0, rotateAxisZ);

        if (directionVector.magnitude < 0.1f)
        {
            directionVector = previousRotationDirection;
        }

        directionVector = directionVector.normalized;
        previousRotationDirection = directionVector;

        //rigidBody.AddTorque(directionVector * turnSpeed * Time.deltaTime);

        // rigidBody.rotation = Quaternion.Euler(directionVector * turnSpeed);
        rigidBody.AddTorque(directionVector * turnSpeed);
    }

    // 

    private void CheckActionButton()
    {
        if (XCI.GetButtonDown(XboxButton.X))
        {
            elementController.SetSelectedElement(Element.Rain);
        }

        if (XCI.GetButtonDown(XboxButton.Y))
        {
            elementController.SetSelectedElement(Element.Sunlight);
        }

        if (XCI.GetButtonDown(XboxButton.B))
        {
            elementController.SetSelectedElement(Element.Time);
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Plant")
        {

            if (XCI.GetAxis(XboxAxis.RightTrigger) > 0.1f)
            {
                if (elementController.IsSelectedElementRain())
                {
                    // Debug.Log("X pressed");
                    // Check selected element controller

                    other.GetComponent<Plant>().GiveWater();
                }
                if (elementController.IsSelectedElementSunlight())
                {
                    // Check selected element controller

                    other.GetComponent<Plant>().GiveSunlight();
                }
                if (elementController.IsSelectedElementTime())
                {
                    // Check selected element controller

                    if (other.GetComponent<Plant>().GetCanRespondToTime())
                    {
                        plantManager.ChangePlantStage(other.gameObject);
                    }

                }
            }

            


        }
    }

}
