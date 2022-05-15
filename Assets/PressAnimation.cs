using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnimation : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;

	private void Start()
	{
		//Press();
	}

	public void Press()
	{
		StopCoroutine(PressNow());
		StartCoroutine(PressNow());
	}

	private IEnumerator PressNow()
	{
		float value = 2f;
		float speed = 10f;
		while (value > 0)
		{
			Debug.Log(value);
			meshRenderer.SetBlendShapeWeight(0, value);
			value += speed;
			if (value > 110) speed = -speed;
			yield return new WaitForSeconds(.005f);
		}
	}
	
}
