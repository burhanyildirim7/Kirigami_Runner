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
		else if (other.CompareTag("katla"))
		{
			//GetComponent<Collider>().enabled = false;
			//StartCoroutine(OpenCollider());
			if (GetComponent<Paper>().type == 0)
			{
				//GetComponent<Collider>().enabled = false;
				//StartCoroutine(OpenCollider());
				Debug.Log("katlandi 1 ");
				GetComponent<Paper>().type = 1;
				GetComponent<Paper>().paperObject1.GetComponent<Animator>().SetTrigger("katla1");
				GameController.instance.SetScore(1);
			}
			else if (GetComponent<Paper>().type == 1)
			{
				//GetComponent<Collider>().enabled = false;
				//StartCoroutine(OpenCollider());
				Debug.Log("katlandi 2 ");
				GetComponent<Paper>().type = 2;
				GetComponent<Paper>().paperObject1.GetComponent<Animator>().SetTrigger("katla2");
				GameController.instance.SetScore(1);
			}
			else if (GetComponent<Paper>().type == 2)
			{
				//GetComponent<Collider>().enabled = false;
				//StartCoroutine(OpenCollider());
				Debug.Log("katlandi 3 ");
				GetComponent<Paper>().type = 3;
				GetComponent<Paper>().paperObject1.GetComponent<Animator>().SetTrigger("katla3");
				GameController.instance.SetScore(1);
			}
		}
		else if (other.CompareTag("kes"))
		{
			if (!GetComponent<Paper>().kesildiMi && GetComponent<Paper>().type ==3)
			{
				Debug.Log("kesildi");
				GetComponent<Paper>().kesildiMi = true;
				GameController.instance.SetScore(1);
			}

		}
		else if (other.CompareTag("sim"))
		{
			if (!GetComponent<Paper>().simlendiMi && GetComponent<Paper>().kesildiMi)
			{
				Debug.Log("simlendi");
				GetComponent<Paper>().simlendiMi = true;
				GameController.instance.SetScore(1);
			}
		}
		else if (other.CompareTag("sus") && GetComponent<Paper>().kesildiMi)
		{
			if (!GetComponent<Paper>().suslendiMi)
			{
				Debug.Log("suslendi");
				GetComponent<Paper>().suslendiMi = true;
				GameController.instance.SetScore(1);
			}
		}

	}

	IEnumerator OpenCollider()
	{
		yield return new WaitForSeconds(5f);
		GetComponent<Collider>().enabled = true;
	}
}
