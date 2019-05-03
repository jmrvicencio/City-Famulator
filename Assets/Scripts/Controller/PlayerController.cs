using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : FarmulatorElement
{

    private void Update()
    {
        //Input detection for various player inputs
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.E)) {
            IPlayerInteractable actionContext = GetCollisionItem();

            actionContext.PlayerAction();
        }

        //getAxisAngle will get the angle of the axes to allow the
        //player to always be moving in a forward direction.
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            app.model.player.facingAngle = getAxisAngle(ref verticalMovement, ref horizontalMovement);
        }

        //move the player
        app.view.player.MovePlayer((Mathf.Abs(horizontalMovement) + Mathf.Abs(verticalMovement)) * app.model.player.playerMoveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Gets the directional angle of axis inputs and adjusts the intesity
    /// of each axis to prevent exceeding 100% movement.
    /// </summary>
    /// <param name="Vertical Axis"></param>
    /// <param name="Horizontal Axis"></param>
    /// <returns></returns>
    private float getAxisAngle(ref float verticalAxis, ref float horizontalAxis)
    {
        //computes the current angle of the movement inputs relative to the right axis
        float axisAngle = Vector2.Angle(Vector2.right, new Vector2(horizontalAxis, verticalAxis));
        //formula for the percentage of the horizontal axis to be used
        //based on the angle relative to the Y axis.
        float horizontalPercent = Mathf.Abs(axisAngle - 90) / 90;

        //if the angle is on Quadrant IV, converts the Acute angle computed by Vector2.Angle() to
        //an obtuse angle.
        if (verticalAxis < 0)
        {
            axisAngle = 360 - axisAngle;
        }

        horizontalAxis *= horizontalPercent;
        verticalAxis *= 1 - horizontalPercent;

        //changes angles rotation from clockwise to counterclockwise
        return Mathf.Abs(360 - axisAngle) + 180;
    }

    /// <summary>
    /// Add/Removes objects that the player can currently interact with and
    /// decides which item will be the active selection.
    /// </summary>
    /// <param name="interactable Object"></param>
    /// <param name="Add Interactable">Will add interactable object if set to true.
    /// If set to false, object will be removed.</param>
    public void HandleInteractable(GameObject interactable, bool addInteractable)
    {
        PlayerModel player = app.model.player;
        float interactableDistance = -1;

        if (addInteractable)
        {
            player.interactableItems.Add(interactable);
        } else
        {
            player.interactableItems.Remove(interactable);
        }

        if (player.interactableItems.Count != 0)
        {
            foreach(GameObject i in player.interactableItems)
            {
                if(DistanceToPlayer(i) > interactableDistance)
                {
                    app.model.player.activeInteractable = i;
                    interactableDistance = DistanceToPlayer(i);
                }
            }
        } else
        {
            app.model.player.activeInteractable = null;
            interactableDistance = -1;
        }

    }

    /// <summary>
    /// Returns the distance of the player from the passed GameObject
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    private float DistanceToPlayer(GameObject o)
    {
        return Vector3.Distance(o.transform.position, app.view.player.transform.position);
    }
}
