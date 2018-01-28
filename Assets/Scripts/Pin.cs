using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public void Init(PinData data)
    {
//        _data = data;
    }

//    private PinData _data;
}

public enum PinType
{
    White = 0,
    Red = 1,
    Golden = 2,
}

[System.Serializable]
public class PinData
{
    public PinType Type;
    public int Score;
    public GameObject Prefab;
}