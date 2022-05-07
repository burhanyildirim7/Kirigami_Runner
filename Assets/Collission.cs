using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collission : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("paper"))
		{
			if (!PaperController.instance.papers.Contains(other.gameObject))
			{
				other.GetComponent<Collider>().isTrigger = false;
				other.gameObject.tag = "Untagged";
				other.gameObject.AddComponent<Collission>();
				other.gameObject.AddComponent<Rigidbody>();
				other.GetComponent<Rigidbody>().isKinematic = true;
				PaperController.instance.StackPaper(other.gameObject, PaperController.instance.papers.Count - 1);
			}
		}
	}
}
