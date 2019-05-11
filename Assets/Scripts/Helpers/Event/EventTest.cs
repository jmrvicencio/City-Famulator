using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    private UnityAction someListener;

    //private void Awake()
    //{
    //    someListener = new UnityAction(SomeFunction);
    //}

    private void OnEnable()
    {
        EventManager.StartListening("test", SomeFunction);
    }

    private void OnDisable()
    {
        EventManager.StopListening("test", SomeFunction);
    }

    private void SomeFunction()
    {
        Debug.Log("Some function was called");
    }
}
