﻿using UnityEngine;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    public void Move()
    {
        _transform.position += new Vector3(_velocity, 0, 0) * Time.deltaTime;
    }

    public void Reset()
    {
        if (_pins == null)
        {
            _pins = new List<Pin>();
        }

        for (int i = 0; i < _pins.Count; i++)
        {
            Destroy(_pins[i].gameObject);
        }
        _pins.Clear();
            
        int[,] roundData = new int[3, 5]
            {{-1, -1, -1, 0, -1}, 
            {-1, 0, -1, -1, -1}, 
            {-1, -1, 1, -1, -1}};

        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                if (roundData[i,k] != -1)
                {
                    CreatePin(i, k, roundData[i, k]);
                }
            }
        }

        _transform.position = new Vector3(-25, 0, _transform.position.z);
    }

    private void CreatePin(int row, int column, int type)
    {
        PinData data = GetPinData(type);
        Vector3 position = _originPoint.position + new Vector3(column * 5, 0, row * 5);
        GameObject pinObject = GameObject.Instantiate(data.Prefab);
        pinObject.transform.SetParent(_originPoint.parent);
        pinObject.transform.position = position;

        Pin pin = pinObject.GetComponent<Pin>();
        pin.Init(data);
        _pins.Add(pin);
    }

    private PinData GetPinData(int type)
    {
        for (int i = 0; i < _pinDataList.Count; i++)
        {
            if (_pinDataList[i].Type == (PinType)type)
            {
                return _pinDataList[i];
            }
        }
        return null;
    }

    [SerializeField]
    private List<PinData> _pinDataList;
    [SerializeField]
    private Transform _originPoint;
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    private float _velocity;

    private List<Pin> _pins;
}