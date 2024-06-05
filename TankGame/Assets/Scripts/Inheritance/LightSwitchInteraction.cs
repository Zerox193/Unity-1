using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteraction : Interaction
{
    public Light lightSwitch;

    public override void Activate()
    {
        lightSwitch.enabled = !lightSwitch.enabled;
    }
}
