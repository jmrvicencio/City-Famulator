using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInteractable
{
    void PlayerAction(Item item);
}

//PlayerController => when it collides && action button is pressed:
//                        check if its a plantable tile
//                        check what item im holding
//                            if its a plant then plant it (if the the tile is empty)
//                            if its a tool, check which tool: do action

