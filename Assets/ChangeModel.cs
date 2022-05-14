using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : MonoBehaviour
{

	public void ChangeMyModel()
	{
		transform.parent.GetComponent<Paper>().paperObject1.SetActive(false);
		transform.parent.GetComponent<Paper>().paperObject2.SetActive(true);
	}
}
