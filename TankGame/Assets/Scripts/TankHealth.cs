using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    // the amount of health each tank starts with
    public float m_StartingHealth = 100f;
    // a prefab that will be instantiated in awake, then used whenever
    // the tank dies
    public GameObject m_ExplosionPrefab;

    private float m_CurrentHealth;
    private bool m_Dead;
    // The particle system that will play when the tank is destroyed
    private ParticleSystem m_ExplosionParticles;

    private void Awake()
    {
        //instantiate the explosion prefab and get a reference to
        // the particle system on it
        m_ExplosionParticles =
            Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        //disable the prefab so it can be activated whem it's required
        m_ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // when the tank is enabled, reset the tanks health and whether
        // or not its dead
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }

    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done
        m_CurrentHealth -= amount;

        // if the current health is at or below zero and it has not yet
        // been registered, call OnDeath
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // set the flag so that this function is only called once
        m_Dead = true;

        // move the instantiated explosion prefab to the tanks position
        // and turn it on
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // play the particle system of the tank exploding
        m_ExplosionParticles.Play();

        // turn the tank off
        gameObject.SetActive(false);
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
