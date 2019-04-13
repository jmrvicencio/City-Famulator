using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : FarmulatorElement
{
    public float playerMoveSpeed = 5f;
    public float facingAngle = 0f;

    //A list of the current interactables around the player.
    public List<GameObject> interactableItems;
    public GameObject activeInteractable;
}