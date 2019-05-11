using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Item", order = 52)]
[System.Serializable]
public class TileItem : ScriptableObject
{
    [SerializeField]
    public string Name;
    ConstEnums.TileType TileType;
    public GameObject model;
}