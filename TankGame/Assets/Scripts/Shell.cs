using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    //the time in seconds before the shell is removed
    public float m_MaxLifeTime = 2f;
    // the amount of damage done if the explosion is centred on a tank
    public float m_MaxDamage = 34f;
    // the maximum distance away from the explosion tanks cna be and are still affected
    public float m_ExplosionRadius = 5f;
    // the amount of force added to a tank at the centre of the explosion
    public float m_ExplosionForce = 100f;

    //reference to the particls that will play on explosion
    public ParticleSystem m_ExplosionPaticles;

    // Start is called before the first frame update
    public void Start()
    {
        // if it isn;t destrpyed by then, destroy the shell after its lifetime
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        //find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

       // onlu tanks will have rigidbody scripts
       if (targetRigidbody != null)
        {
            // add an explosion force
            targetRigidbody.AddExplosionForce(m_ExplosionForce,
                transform.position, m_ExplosionRadius);

            // find the tankhealth script associated with the rigidbody
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (targetHealth != null)
            {
                // calculate the amount of damage the target should take
                // based on its distance from the shell.
                float damage = CalculateDamage(targetRigidbody.position);

                // deal this damage to the tank
                targetHealth.TakeDamage(damage);
            }
        }

       

        //unparent the particles from the shell
        m_ExplosionPaticles.transform.parent = null;

        //play the partucle system
        m_ExplosionPaticles.Play();

        //Once the particles have finished, destroy the gameObject they are on
        Destroy(m_ExplosionPaticles.gameObject, m_ExplosionPaticles.main.duration);

        //destroy the shell
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // create a vector from the shell to the target
        Vector3 explosionToTarget = targetPosition - transform.position;

        // calculate the distance from the shell to the target
        float explosionDistance = explosionToTarget.magnitude;

        // calculate the proportion of the maximm distance (The explosionRadius)
        // the target is away
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // calculate damage as this proportion of the maximum possible damage
        float damage = relativeDistance * m_MaxDamage;

        // make sure that the minimum damage is always 0
        damage = Mathf.Max(0f, damage);

        return damage;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
