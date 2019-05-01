using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentHelper : MonoBehaviour
{
    private static PersistentHelper persistentElement;

    //Allows this elements and it's children to remain between scene trainsitions.
    private void Awake()
    {
        if (persistentElement == null)
        {
            DontDestroyOnLoad(gameObject);
            persistentElement = this;
        }
        else if (persistentElement != this)
        {
            Destroy(gameObject);
        }
    }
}
