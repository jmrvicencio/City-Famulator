using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableTestObject", order = 52)]
public class TestSO : ScriptableObject
{
    [System.Serializable]
    public class TestSOObject
    {
        [SerializeField]
        public string ObjectString;
        [SerializeField]
        public int ObjectInt;
    }

    //The Custom Class
    [SerializeField]
    public List<TestSOObject> myList = new List<TestSOObject>(1);
}

