using UnityEngine;

public class FarmulatorController : FarmulatorElement
{
    public PlayerController player;
    public InteractionController interaction;

    private void Start()
    {
        app.model.cameraAngle = Camera.main.transform.eulerAngles.y;
    }

    public void AddDay(int days = 1)
    {
        app.model.day++;
    }
}