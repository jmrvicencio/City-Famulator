using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : FarmulatorElement
{
    public float playerMoveSpeed = 5f;
    public float facingAngle = 0f;
    [HideInInspector]
    public bool buildPossible = true;

    //A list of the current interactables around the player.
    public List<GameObject> actionContexts;
    public GameObject activeActionContext;

    //Current Held Item
    public enum HeldItem { Build, Plant, Tool, Nothing }
    public HeldItem heldItem = HeldItem.Plant;
    [HideInInspector]
    public HeldItem currentHeldItem = HeldItem.Nothing;
}