using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnimation : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
	public GameObject effect;

	private void Start()
	{
		//Press();
	}

	public void Press(Vector3 effectPosition)
	{
		StopCoroutine(PressNow(Vector3.zero));
		StartCoroutine(PressNow(effectPosition));
	}

	private IEnumerator PressNow(Vector3 effectPosition)
	{
		float value = 2f;
		float speed = 10f;
		while (value > 0)
		{
			Debug.Log(value);
			meshRenderer.SetBlendShapeWeight(0, value);
			value += speed;
			if (value > 110)
			{
				speed = -speed;
				Instantiate(effect, effectPosition, Quaternion.identity);
			}
			yield return new WaitForSeconds(.005f);
		}
	}
	
}
