using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterPaketiMovement : MonoBehaviour
{
	public static KarakterPaketiMovement instance;
	private void Awake()
	{
		if (instance == null) instance = this;
	}

	public float moveSpeed = 0f;

	private void Update()
	{

            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
	}
}
