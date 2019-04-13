using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContextView : FarmulatorElement
{
    public void OnTriggerEnter(Collider other)
    {
        app.controller.player.HandleInteractable(other.gameObject,true);
    }

    public void OnTriggerExit(Collider other)
    {
        app.controller.player.HandleInteractable(other.gameObject, false);
    }
}
