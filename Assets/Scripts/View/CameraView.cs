using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : FarmulatorElement
{
    private Vector3 cameraOffset;
    public float cameraDamping = 10f;

    private void Start()
    {
        cameraOffset = app.view.player.transform.position + this.transform.position;
    }

    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, app.view.player.transform.position + cameraOffset, cameraDamping * Time.deltaTime);
    }
}
