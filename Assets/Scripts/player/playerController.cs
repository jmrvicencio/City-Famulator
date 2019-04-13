using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    

    // nestors jian
    public float playerMoveSpeed = 1f;
    private float axisAngle = 0f;

    public Text debugTextVert, debugTextHor, debugAngle;

    // Update is called once per frame
    void Update()
    {

        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");

        //getAxisAngle will get the angle of the axes to allow the player to always be moving in a forward direction.
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            axisAngle = getAxisAngle(ref verticalMovement, ref horizontalMovement);
        }

        setDebugText(verticalMovement, horizontalMovement, axisAngle);

        //move the character based on the angle and direction of the camera
        float cameraFacingAngle = Camera.main.transform.eulerAngles.y - 90;
        float forwardMovement = (Mathf.Abs(horizontalMovement) + Mathf.Abs(verticalMovement)) * playerMoveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0f, axisAngle + cameraFacingAngle, 0f);
        transform.Translate(0, 0, forwardMovement);
    }
    
    /// <summary>
    /// Gets the directional angle of axis inputs and adjusts the intesity
    /// of each axis to prevent exceeding 100% movement.
    /// </summary>
    /// <param name="Vertical Axis"></param>
    /// <param name="Horizontal Axis"></param>
    /// <returns></returns>
    private float getAxisAngle(ref float vertAxis, ref float horAxis)
    {
        float axisAngle = Vector2.Angle(Vector2.right, new Vector2(horAxis,vertAxis));
        float horizontalPercent = Mathf.Abs(axisAngle-90) / 90;
        if(vertAxis < 0)
        {
            axisAngle = 360 - axisAngle;
        }

        horAxis *= horizontalPercent;
        vertAxis *= 1 - horizontalPercent;

        return Mathf.Abs(360 - axisAngle) + 180;
    }

    void setDebugText(float vertAxis, float horAxis, float axisAngle)
    {
        if(debugTextVert)
        debugTextVert.text = "Vertical Axis: " + vertAxis;

        if(debugTextHor)
        debugTextHor.text = "Horizontal Axis: " + horAxis;

        if(debugAngle)
        debugAngle.text = "Axis Angle: " + axisAngle;
    }

}
