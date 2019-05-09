using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : FarmulatorElement
{
    public GameObject actionContext;
    [HideInInspector]
    private GameObject buildItemGameObject;
    private GameObject buildItemOutline;
    [SerializeField]
    private Material buildItemMaterial;
    [SerializeField]
    private Material invalidItemMaterial;

    public void Start()
    {
        buildItemOutline = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(buildItemOutline.GetComponent<BoxCollider>());
        buildItemOutline.GetComponent<Renderer>().material = buildItemMaterial;
        buildItemOutline.SetActive(false);
        buildItemOutline.name = "build outline";
        buildItemOutline.transform.parent = GameObject.Find("Objects").transform;
    }

    public void Update()
    {
        if (app.model.player.heldItem == PlayerModel.HeldItem.Build) buildItemOutline.SetActive(true);
        else buildItemOutline.SetActive(false);
    }

    public void MovePlayer(float deltaForward)
    {
        //face the player in the direction the player has inputted
        //relative to the camera angle
        transform.eulerAngles = new Vector3(0f, (app.model.player.facingAngle + 90) + app.view.camera.transform.eulerAngles.y, 0f);
        //move player forward based on deltaForward
        transform.Translate(0, 0, deltaForward);
        //For future reference when fixing movement, use CharacterController for movement
    }

    public void SetBuildOutlineItem(GameObject buildItem)
    {
        buildItemOutline.GetComponent<MeshFilter>().mesh = buildItem.GetComponent<MeshFilter>().sharedMesh;
    }

    public void SetOutlinePosition(Vector3 position)
    {
        buildItemOutline.transform.position = FCUtils.CoordsToGrid(position);
        if(app.model.tileData.ContainsKey(new Vector3Int(position)))
        {
            app.model.player.buildPossible = false;
            buildItemOutline.GetComponent<Renderer>().material = invalidItemMaterial;
        }
        else
        {
            app.model.player.buildPossible = true;
            buildItemOutline.GetComponent<Renderer>().material = buildItemMaterial;
        }
    }

    public Transform getOutlinePosition()
    {
        return buildItemOutline.transform;
    }
}