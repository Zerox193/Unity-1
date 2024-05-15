using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    //prefab of the shell
    public Rigidbody m_Shell;
    // a child of the tank where the shells are spawned
    public Transform m_FireTransform;
    //the force given to the shell when firing
    public float m_LaunchForce = 30f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Later on, well check with the game manager to make
        // sure the game isnt over

        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }

    }

    private void Fire()
    {
        // create an instant of the shell and store a reference to its rigid body
        Rigidbody shellInstance = Instantiate(m_Shell,
            m_FireTransform.position, m_FireTransform.rotation);

        // set the shells velocity to the launch force in the fire
        //positions forward direction
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;

    }

}
