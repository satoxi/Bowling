using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pin : MonoBehaviour
{
    public Action<PinData> OnDown = delegate { };

    public void CheckIsDown(Vector3 normal)
    {
        if (_isDown)
        {
            return;
        }

        if (Vector3.Dot(_transform.up, normal) < 0.5f)
        {
            _isDown = true;
            OnDown(_data);
        }
    }

    public void Init(PinData data)
    {
        _data = data;
        _transform = GetComponentInChildren<Rigidbody>().transform;
    }

    private PinData _data;
    private Transform _transform;
    private bool _isDown;
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