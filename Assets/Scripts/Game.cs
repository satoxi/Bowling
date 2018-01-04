using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	private void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			MoveBowling();
		}
	}

	private void MoveBowling()
	{
		_bowling.Move();
	}

	[SerializeField]
	private Bowling _bowling;
}
