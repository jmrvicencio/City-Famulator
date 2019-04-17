using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    private void Start()
    {
        EventManager.TriggerEvent("test");
    }
}
