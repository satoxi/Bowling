using UnityEngine;
using System.Collections.Generic;
using System;

public class Platform : MonoBehaviour
{
    public Action<PinData> HandlePinDown = delegate { };

    public void CheckPins()
    {
        for (int i = 0; i < _pins.Count; i++)
        {
            _pins[i].CheckIsDown(_transform.up);
        }    
    }

    public void Move()
    {
        _transform.position += new Vector3(_velocity, 0, 0) * Time.deltaTime;
    }

    public void Reset(int[,] roundData)
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

        for (int i = 0; i < roundData.GetLength(0); i++)
        {
            for (int k = 0; k < roundData.GetLength(1); k++)
            {
                if (roundData[i,k] != -1)
                {
                    CreatePin(i, k, roundData[i, k]);
                }
            }
        }

        _transform.position = new Vector3(-40, 0, _transform.position.z);
    }

    private void CreatePin(int row, int column, int type)
    {
        PinData data = GetPinData(type);
        Vector3 position = _originPoint.position + new Vector3(column * 7, 0, row * 6);
        GameObject pinObject = GameObject.Instantiate(data.Prefab);
        pinObject.transform.SetParent(_originPoint.parent);
        pinObject.transform.position = position;

        Pin pin = pinObject.GetComponent<Pin>();
        pin.Init(data);
        pin.OnDown += OnPinDown;
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

    private void OnPinDown(PinData data)
    {
        HandlePinDown(data);
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
