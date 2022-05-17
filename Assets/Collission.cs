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
			if (GetComponent<Paper>().type == 0)
			{
				GetComponent<Paper>().type = 1;
				GetComponent<Paper>().paperObject1.GetComponent<Animator>().SetTrigger("katla1");
				GameController.instance.SetScore(1);
			}
			else if (GetComponent<Paper>().type == 1)
			{
				GetComponent<Paper>().type = 2;
				GetComponent<Paper>().paperObject1.GetComponent<Animator>().SetTrigger("katla2");
				GameController.instance.SetScore(1);
			}
			else if (GetComponent<Paper>().type == 2)
			{
				GetComponent<Paper>().type = 3;
				GetComponent<Paper>().paperObject1.GetComponent<Animator>().SetTrigger("katla3");
				GameController.instance.SetScore(1);

			}
		}
		else if (other.CompareTag("kes"))
		{
			if (!GetComponent<Paper>().kesildiMi && GetComponent<Paper>().type ==3)
			{
				if(GetComponent<Paper>().kes == 0)
				{
					GetComponent<Paper>().kes = 1;
					GetComponent<Paper>().KesParca1.SetActive(false);
					GameController.instance.SetScore(1);
					other.GetComponent<PressAnimation>().Press(transform.position);
				}
				else if (GetComponent<Paper>().kes == 1)
				{
					GetComponent<Paper>().kes = 2;
					GetComponent<Paper>().KesParca2.SetActive(false);
					GameController.instance.SetScore(1);
					other.GetComponent<PressAnimation>().Press(transform.position);
				}
				else if (GetComponent<Paper>().kes == 2)
				{
					GetComponent<Paper>().kes = 3;
					GetComponent<Paper>().kesildiMi = true;
					GetComponent<Paper>().KesParca3.SetActive(false);
					GameController.instance.SetScore(1);
					other.GetComponent<PressAnimation>().Press(transform.position);
					//perController.instance.hazirKagitSayisi++;
					//GameController.instance.scoreCarpani++;
				}
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
		else if (other.CompareTag("xs") && GetComponent<Paper>().kesildiMi && !GetComponent<Paper>().acildiMi)
		{
			other.GetComponent<Collider>().enabled = false;
			GetComponent<Paper>().paperObject2.SetActive(false);
			GetComponent<Paper>().paperObject3.SetActive(true);
			GetComponent<Paper>().acildiMi = true;
			PaperController.instance.hazirKagitSayisi--;
			if (PaperController.instance.hazirKagitSayisi == 0) GameController.instance.FinishGame();
			
		}
		else if (other.CompareTag("ENGEL"))
		{
			Instantiate(PaperController.instance.tozEfecti, transform.position, Quaternion.identity);
			Debug.Log("ENGELE CARPTIK");
			//other.GetComponent<Collider>().enabled = false;
			if(GetComponent<FirstPaperController>())
			{
				// OYUN KAYBEDILIYOR...
				Debug.Log("GameOver");
				GameController.instance.isContinue = false;
				UIController.instance.ActivateLooseScreen();
				return;
			}
			else
			{
				int index = PaperController.instance.papers.IndexOf(transform.gameObject);
				DestroyPapers(index);
				
			}		
			//if (GetComponent<Paper>().kesildiMi)
			//{
			//	if (GameController.instance.scoreCarpani > 0) GameController.instance.scoreCarpani--;
			//	if (PaperController.instance.hazirKagitSayisi > 0) PaperController.instance.hazirKagitSayisi--;
			//}
		}
		else if (other.CompareTag("firstFinish"))
		{
			other.GetComponent<Collider>().enabled = false;
			ControlPapers();
		}
	}

	void ControlPapers()
	{
		foreach (GameObject obj in PaperController.instance.papers)
		{
			if(obj.GetComponent<Paper>().kesildiMi == true)
			{
				if (obj.GetComponent<Paper>().acildiMi == false)
				{
					PaperController.instance.hazirKagitSayisi++;
					GameController.instance.scoreCarpani++;
				}
			}			
		}
	}

	void DestroyPapers(int index)
	{

		while (PaperController.instance.papers.Count > index)
		{
			Destroy(PaperController.instance.papers[PaperController.instance.papers.Count - 1]);
			PaperController.instance.papers.RemoveAt(PaperController.instance.papers.Count-1);			
		}

	}


	IEnumerator RemoveDestroyedObjects(GameObject obj)
	{
		yield return new WaitForSeconds(.1f);
		PaperController.instance.papers.Remove(obj);
	}
}
