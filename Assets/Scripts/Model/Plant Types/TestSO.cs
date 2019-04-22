using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableTestObject", order = 52)]
[System.Serializable]
public class TestSO : ScriptableObject
{
    [SerializeField]
    private string stringField;

    [SerializeField]
    private string stringProperty;
    [SerializeField]
    private int intField;

    [SerializeField]
    private List<TestSOObject> things;

    [SerializeField]
    public List<TestSOObject> myList = new List<TestSOObject>(0);

    public string StringField { get; set; }
    public string StringProperty { get; set; }
    public int IntField
    {
        get { return intField; }
        set
        {
            if(value > 0)
            {
                intField = value;
            }
            else
            {
                intField = 1;
            }
        }
    }

    //public List<int> Things { get; set; }
}

[System.Serializable]
public class TestSOObject
{
    [SerializeField]
    public string ObjectString { get; set; }

    [SerializeField]
    public int ObjectInt { get; set; }
}