using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmulatorModel : FarmulatorElement
{
    public static FarmulatorModel farmulatorModel;

    private void Awake()
    {
        if (farmulatorModel == null)
        {
            DontDestroyOnLoad(gameObject);
            farmulatorModel = this;
        }
        else if(farmulatorModel != this)
        {
            Destroy(gameObject);
        }
    }

    //Nested Models
    public PlayerModel player;

    //data
    public float cameraAngle;
    public int day;
}
