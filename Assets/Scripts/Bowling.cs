using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
	public void Move()
	{
		_rigidbody.AddForce(new Vector3(0, 0, 3000f));
	}

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _rigidbody.transform.localPosition = Vector3.zero;
        transform.localPosition = _position;
    }

	[SerializeField]
	private Rigidbody _rigidbody;
    [SerializeField]
    private Vector3 _position;
}
