using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
					GetComponent<Paper>().TickObj.SetActive(true);
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
				GetComponent<Paper>().simlendiMi = true;
				GameController.instance.SetScore(1);
			}
		}
		else if (other.CompareTag("sus") && GetComponent<Paper>().kesildiMi)
		{
			if (!GetComponent<Paper>().suslendiMi)
			{
				GetComponent<Paper>().suslendiMi = true;
				GameController.instance.SetScore(1);
			}
		}
		else if (other.CompareTag("xs") && GetComponent<Paper>().kesildiMi && !GetComponent<Paper>().acildiMi)
		{
			PaperController.instance.papers.Remove(gameObject);
			ControlPapers();
			other.GetComponent<Collider>().enabled = false;
			
			PaperAc(other.transform.position);
			GetComponent<Paper>().acildiMi = true;			
			PaperController.instance.hazirKagitSayisi--;
			if (PaperController.instance.hazirKagitSayisi == 0) GameController.instance.FinishGame();
			
		}
		else if (other.CompareTag("x10") && GetComponent<Paper>().kesildiMi && !GetComponent<Paper>().acildiMi)
		{
			other.GetComponent<Collider>().enabled = false;
			PaperController.instance.papers.Remove(gameObject);
			ControlPapers();
			KarakterPaketiMovement.instance.moveSpeed = 0;;
			
			PaperAc(other.transform.position);
			GetComponent<Paper>().acildiMi = true;
			PaperController.instance.hazirKagitSayisi--;
			StartCoroutine( AcVeGonder(other.transform.position));		

		}
		else if (other.CompareTag("ENGEL"))
		{
			Instantiate(PaperController.instance.tozEfecti, transform.position, Quaternion.identity);
			if(GetComponent<FirstPaperController>())
			{
				// OYUN KAYBEDILIYOR...
				GameController.instance.isContinue = false;
				KarakterPaketiMovement.instance.moveSpeed = 0f;
				UIController.instance.ActivateLooseScreen();
				return;
			}
			else
			{
				float z = transform.localPosition.z;
				int count = 0;
				foreach(GameObject obj in PaperController.instance.papers)
				{
					if (obj.transform.localPosition.z >= z) count++;
				}
				int adet = PaperController.instance.papers.Count - 1;
				for (int i = 0; i < count; i++)
				{
					GameObject obj = PaperController.instance.papers[adet-i];
					PaperController.instance.papers.Remove(obj);
					Destroy(obj);
				}
				
			}		
		}
		else if (other.CompareTag("firstFinish"))
		{		
			GameController.instance.isContinue = false;
			Paper tempPaper = GetComponent<Paper>();
			tempPaper.TickObj.SetActive(false);
			if (tempPaper.kesildiMi == true)
			{
				if (tempPaper.acildiMi == false)
				{
					GameController.instance.isContinue = false;
					transform.localPosition = new Vector3(0,1,transform.localPosition.z);
				}
			}
			else
			{
				transform.tag = "Untagged";
				GetComponent<Collider>().enabled = false;
				PaperController.instance.papers.Remove(gameObject);
				int rnd = Random.Range(0,2);
				float posX = 25;
				if (rnd == 0) posX = -25;
				Vector3 jumpPos = new Vector3(transform.position.x + posX, transform.position.y, transform.position.z+15); ;
				transform.DOJump(jumpPos, 20, 1, 4.1f).OnComplete(() => {
					if (!transform.GetComponent<FirstPaperController>()) Destroy(transform.gameObject);
				});
				
				transform.DOShakeRotation(4);
				;
			}
			
		}
	}

	IEnumerator AcVeGonder(Vector3 firstPos)
	{
		
		for (int i = PaperController.instance.papers.Count-1; i >= 0; i--)
		{
			PaperController.instance.papers[i].transform.DOMove(firstPos + new Vector3(0,2,0),.5f)
				.OnComplete(() => {
					if(!PaperController.instance.papers[i].GetComponent<FirstPaperController>())
					PaperController.instance.papers[i].transform.parent = PaperController.instance.copKutusu;
					Paper tempPaper = PaperController.instance.papers[i].GetComponent<Paper>();
					tempPaper.paperObject2.SetActive(false);
					tempPaper.paperObject3.SetActive(true);
					Vector3 jumpPos = new Vector3(0, 30, transform.position.z + 60);
					PaperController.instance.papers[i].transform.DOJump(jumpPos,-15,1,10f);
					PaperController.instance.papers[i].transform.DORotate(new Vector3(-45,0,0),5f);
					Vector3 tempScale = transform.localScale;
					PaperController.instance.papers[i].transform.DOScale(tempScale, .3f).SetEase(Ease.OutBounce);
					Instantiate(tempPaper.confetiEffect, tempPaper.transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
					Instantiate(tempPaper.confetiEffect, tempPaper.transform.position + new Vector3(3, 0, 0), Quaternion.identity);

				});
			yield return new WaitForSeconds(.6f);
		}
		GameController.instance.FinishGame();

	}


	void PaperAc(Vector3 position)
	{

		Paper tempPaper = GetComponent<Paper>();
		tempPaper.TickObj.SetActive(false);
		tempPaper.paperObject2.SetActive(false);
		tempPaper.paperObject3.SetActive(true);
		Vector3 tempScale = transform.localScale;
		transform.DOScale(tempScale * 3f, .3f).SetEase(Ease.OutBounce);
		float posY = 2;
		if (LevelController.instance.kizMi || LevelController.instance.orumcekMi) posY = 2.2f;
		else if (LevelController.instance.kare1Mi || LevelController.instance.kare2Mi) posY = 3.2f;
		else if (LevelController.instance.kartanesi1Mi || LevelController.instance.kartanesi2Mi) posY = 3.7f;
		transform.position = position + new Vector3(0,posY,0);
		transform.rotation = Quaternion.Euler(-90,0, 0);
		Instantiate(tempPaper.confetiEffect, tempPaper.transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
		Instantiate(tempPaper.confetiEffect, tempPaper.transform.position + new Vector3(3, 0, 0), Quaternion.identity);
		if (!GetComponent<FirstPaperController>()) transform.parent = PaperController.instance.copKutusu;
	}

	void ControlPapers()
	{
		PaperController.instance.hazirKagitSayisi = 0;
		foreach (GameObject obj in PaperController.instance.papers)
		{
			if(obj.GetComponent<Paper>().kesildiMi == true)
			{
				if (obj.GetComponent<Paper>().acildiMi == false)
				{
					GetComponent<Paper>().TickObj.SetActive(false);
					PaperController.instance.hazirKagitSayisi++;
					GameController.instance.scoreCarpani++;
				}
			}
			
		}

		if(PaperController.instance.hazirKagitSayisi == 0)
		{
			GameController.instance.FinishGame();
		}	
	}

	void DestroyPapers(int index)
	{
		int adet = PaperController.instance.papers.Count - index + 1;
		for (int i = 0; i < adet; i++)
		{
			GameObject obj = PaperController.instance.papers[PaperController.instance.papers.Count - 1];
			PaperController.instance.papers.Remove(obj);
			Destroy(obj);
			//Destroy(PaperController.instance.papers[PaperController.instance.papers.Count - 1]);
			//PaperController.instance.papers.RemoveAt(PaperController.instance.papers.Count - 1);
		}

	}


	IEnumerator RemoveDestroyedObjects(GameObject obj)
	{
		yield return new WaitForSeconds(.1f);
		PaperController.instance.papers.Remove(obj);
	}

	IEnumerator OpenPapers()
	{
		yield return new WaitForSeconds(.2f);
		float distance = 1;
		foreach(GameObject obj in PaperController.instance.papers)
		{
			Paper tempPaper = obj.GetComponent<Paper>();
			if (tempPaper.kesildiMi)
			{
				tempPaper.paperObject2.SetActive(false);
				tempPaper.paperObject3.SetActive(true);
				Vector3 tempScale = obj.transform.localScale;
				obj.transform.DOScale(tempScale * 2.5f, .3f);
				obj.transform.localPosition = new Vector3(obj.transform.localPosition.x,obj.transform.localPosition.y,obj.transform.localPosition.z + distance );
				distance += 2;
				yield return new WaitForSeconds(.1f);
			}
			else{
				Destroy(obj);
			}
		}
	}
}
