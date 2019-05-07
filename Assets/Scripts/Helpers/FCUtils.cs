using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCUtils : FarmulatorElement
{
    public static Vector3 RoundToGrid(Vector3 position)
    {
        float y = position.y;
        float z, x;

        position = position / app.model.gridSize;
        z = Mathf.Round(position.z) * app.model.gridSize;
        x = Mathf.Round(position.x) * app.model.gridSize;

        return new Vector3(x, y, z);
    }

    public static Vector3 DistanceByAngle(float angle, float distance)
    {
        while (angle > 360) angle -= 360;
        while (angle < 0) angle += 360;

        int yPositive = 1, xPositive = 1;
        if (angle > 180f)
        {
            angle = 360f - angle;
            yPositive = -1;
        }
        if(angle > 90f)
        {
            xPositive = -1;
        }
        float horizontalPercentage = Math.Abs(angle - 90f) / 90f;

        float xDist, zDist;
        xDist = distance * horizontalPercentage * xPositive;
        zDist = (distance - Math.Abs(xDist)) * yPositive;

        return new Vector3(xDist, 0f, zDist);
    }
}
