using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : FarmulatorElement
{

    public GameObject actionContextCollider;

    public void MovePlayer(float deltaForward)
    {
        //face the player in the direction the player has inputted
        //relative to the camera angle
        transform.eulerAngles = new Vector3(0f, (app.model.player.facingAngle - 90) + app.model.cameraAngle, 0f);
        //move player forward based on deltaForward
        transform.Translate(0, 0, deltaForward);
    }
}