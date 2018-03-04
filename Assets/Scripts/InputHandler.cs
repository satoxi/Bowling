using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputHandler
{
    public Action OnSlideUp = delegate { };

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastPosition = Input.mousePosition;
            _currentPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _currentPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            float delta = _currentPosition.y - _lastPosition.y;
            if (delta > 100f)
            {
                OnSlideUp();
            }
        }
    }

    private Vector3 _lastPosition;
    private Vector3 _currentPosition;
}
