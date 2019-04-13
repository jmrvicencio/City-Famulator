using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmulatorController : FarmulatorElement
{
    public PlayerController player;

    private void Start()
    {
        app.model.cameraAngle = Camera.main.transform.eulerAngles.y;
    }
}