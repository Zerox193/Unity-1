using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{

    // prefab of a shell
    public Rigidbody m_Shell;
    // a child of the tank where the shells are spawned
    public Transform m_FireTransform;

    // the force given to the shell when firing
    public float m_LaunchForce = 30f;

    public float m_ShootDelay = 1f;

    private bool m_CanShoot;
    private float m_ShootTimer;

    private void Awake()
    {
        m_CanShoot = false;
        m_ShootTimer = m_ShootDelay;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_ShootDelay;
                Fire();
            }
        }
    }

    private void Fire()

    {
        // create an insance of the shell and store a reference to its rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell,
        m_FireTransform.position, m_FireTransform.rotation);

        // set the shells velocity to the launch force in the fire
        //positions forward direction
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanShoot = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanShoot = false;
        }
    }
}
