using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public Vector3 previousRotationDirection = Vector3.forward;
    public float turnSpeed = 60;

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
    }


    // Translate the player camera along the X and Z axes
    private void MovePlayer()
    {
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
        float axisZ = XCI.GetAxis(XboxAxis.LeftStickY, controller);

        Vector3 movement = new Vector3(axisX, 0, axisZ);
        //Debug.Log(movement*movementSpeed);

        rigidBody.AddForce(movement * movementSpeed);

        // Ensure player can't go faster than the max speed
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }


    }

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
        rigidBody.AddTorque(directionVector * turnSpeed);
    }
}
