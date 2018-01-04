using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
	public void Move()
	{
		_rigidbody.AddForce(new Vector3(0, 0, 2000f));
	}

	[SerializeField]
	private Rigidbody _rigidbody;
}
