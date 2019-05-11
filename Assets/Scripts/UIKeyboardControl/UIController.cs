using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private enum NavDirection { Up, Down, Left, Right};
    public float navDistanceOffset = 10f;
    private GameObject activeObject;
    private GameObject potentialGameObject;
    float closestDistanceSqr;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            navigateUI(NavDirection.Up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            navigateUI(NavDirection.Right);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            navigateUI(NavDirection.Down);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            navigateUI(NavDirection.Left);
        }
    }

    private void navigateUI(NavDirection direction)
    {
        closestDistanceSqr = Mathf.Infinity;

        if (activeObject == null)
        {
            activeObject = GameObject.Find("Selection Items").transform.GetChild(0).gameObject;
            activeObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            activeObject.GetComponent<Image>().color = Color.white;
            GameObject closestItem = activeObject;
            foreach(Transform child in activeObject.transform.parent.transform)
            {
                switch (direction)
                {
                    case NavDirection.Up:
                        if (child.transform.position.y > activeObject.transform.position.y + navDistanceOffset)
                        {
                            checkDistance(child.gameObject);
                        }
                        break;

                    case NavDirection.Down:
                        if (child.transform.position.y < activeObject.transform.position.y - navDistanceOffset)
                        {
                            checkDistance(child.gameObject);
                        }
                        break;

                    case NavDirection.Left:
                        if (child.transform.position.x < activeObject.transform.position.x - navDistanceOffset)
                        {
                            checkDistance(child.gameObject);
                        }
                        break;

                    case NavDirection.Right:
                        if (child.transform.position.x > activeObject.transform.position.x + navDistanceOffset)
                        {
                            checkDistance(child.gameObject);
                        }
                        break;
                }
            }
            activeObject = potentialGameObject;
            activeObject.GetComponent<Image>().color = Color.red;
        }
    }

    private void checkDistance(GameObject item)
    {
        float sqrDistance = Vector3.SqrMagnitude(activeObject.transform.position - item.transform.position);
        if(sqrDistance < closestDistanceSqr)
        {
            closestDistanceSqr = sqrDistance;
            potentialGameObject = item;
        }
    }
}
