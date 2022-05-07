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

	public float moveSpeed = 5f;

	private void Update()
	{
		if (GameController.instance.isContinue)
		{
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
	}
}
