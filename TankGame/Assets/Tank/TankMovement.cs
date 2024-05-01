using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float m_Speed = 20f;             //how fast the tank moves foward and back
    public float m_RotationSpeed = 180f;    //how fast the tank turn in degrees per second

    private Rigidbody m_Rigidbody;

    private float m_FowardInputValue;       // the current value of the movement input
    private float m_TurnInputValue;         // the current value of the turn input

    private void Awake()
    {

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //when the tank is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;

        //also reset the input value
        m_FowardInputValue = 0;
        m_TurnInputValue = 0;
    }

    private void OnDisable()
    {
        //when the tank is tuned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }

    private void Update()
    {
        m_FowardInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");

    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        //create a vectir ub the direction the tank is facing with a magnitude
        //based on the input, speed and time between frames
        Vector3 wantedVelocity = transform.forward * m_FowardInputValue * m_Speed;

        //apply the wantedVelocity minus the current rigidbody velocity to apply a change
        // in the velocity on the tank.
        //this ignores the mass of the tank
        m_Rigidbody.AddForce(wantedVelocity - m_Rigidbody.velocity, ForceMode.VelocityChange);
    }
    private void Turn()
    {
        // determining the number of degrees to be turned based on the input
        // speed and time between frames
        float turnValue = m_TurnInputValue * m_RotationSpeed * Time.deltaTime;

        // make this into a rotation around the y-axis
        Quaternion turnRotation = Quaternion.Euler(0f, turnValue, 0f);

        // apply this rotation to the rigidbody's rotation
        m_Rigidbody.MoveRotation(transform.rotation * turnRotation);
    }

}
