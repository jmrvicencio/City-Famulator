using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UILayouter : EditorWindow
{
    [MenuItem("Window/UI Layouter")]
    public static void ShowWindow()
    {
        GetWindow<UILayouter>("UI Layouter");
    }

    public bool displayConnections = false;
    public int Testing;
    float gridSize = 0f;
    int columns = 1, rows = 1;
    float height = 1f, width = 1f;

    private void OnEnable()
    {
    }

    private void OnGUI()
    {
        displayConnections = EditorGUILayout.Toggle("Show Connections", displayConnections);

        gridSize = EditorGUILayout.FloatField("Grid Size",gridSize);
        height = EditorGUILayout.FloatField("Tile Size", height);
        width = height;
        columns = EditorGUILayout.IntField("columns", columns);
        rows = EditorGUILayout.IntField("Rows", rows);


        if (GUILayout.Button("Align Children to Grid"))
        {
            GameObject currentItem = Selection.activeGameObject;
            int currentIndex = 0;
            foreach (Transform child in currentItem.transform)
            {
                int currentRow = currentIndex / columns;
                int currentColumn = currentIndex - (currentRow * columns);
                child.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)currentColumn * gridSize,(float)-currentRow * gridSize);
                currentIndex++;
            }
            currentItem.GetComponent<RectTransform>().sizeDelta = new Vector2((gridSize * (columns - 1)) + width, (gridSize * (rows - 1)) + height);
        }
        
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        if (GUILayout.Button("Test Button"))
        {
            Debug.Log("Arrange Children Items");
            GameObject currentItem = Selection.activeGameObject;
            currentItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }
    }

    private void Update()
    {
        if (displayConnections)
        {
            Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(100, 100, 0), Color.blue);
        }
    }
}
