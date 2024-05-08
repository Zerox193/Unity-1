using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    //the tank will stop moving towards the player once it reaches this distance
    public float m_CloseDistance = 8f;
    //the tank's turret object
    public Transform m_turret;

    //a reference to the player - this will be set when the enemy is loaded
    private GameObject m_Player;
    //a reference to the nav mesh agent component
    private NavMeshAgent m_NavAgent;
    //a reference to the rigidbody component
    private Rigidbody m_RigidBody;

    //will be est to truw when this tank should follow the player
    private bool m_Follow;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Follow = false;
    }

    private void OnEnable()
    {
        //When the tank is turned on, make sure ut us not kinematic
        m_RigidBody.isKinematic = false;
    }

    private void OnDisable()
    {
        //when the tank is turned off, set it to kinematic so it stops moving
        m_RigidBody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
