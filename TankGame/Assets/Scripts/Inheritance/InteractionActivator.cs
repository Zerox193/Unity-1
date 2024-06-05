using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionActivator : Interaction
{
    public Interaction[] interactions;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Interaction interaction in interactions)
            {
                interaction.Activate();
            }
        }

    }
}
