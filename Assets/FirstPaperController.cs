using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPaperController : MonoBehaviour
{
	public static FirstPaperController instance;
	public GameObject kagitKiz, kagitOrumcek, kagitKare1, kagitKare2, kagitKartane1,KagitKarTane2;
	public GameObject kiz, orumcek, kare1, kare2, kartane1, kartane2;
	public GameObject kizKes1, kizKes2, kizKes3;
	public GameObject orumcekKes1, orumcekKes2, orumcekKes3;
	public GameObject kare1Kes1, kare1Kes2, kare1Kes3;
	public GameObject kare2Kes1, kare2Kes2, kare2Kes3;
	public GameObject kartane1Kes1, kartane1Kes2, kartane1Kes3;
	public GameObject kartane2Kes1, kartane2Kes2, kartane2Kes3;
	[HideInInspector]public Paper paper;
	public GameObject kizTam, orumcekTam, kare1Tam, kare2Tam, kartane1Tam, kartane2Tam;
	public GameObject myParentObject;
	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	private void Start()
	{
		 paper = GetComponent<Paper>();
	}

	public void ChangePaper()
	{
		if(paper.paperObject1 != null)Destroy(paper.paperObject1);
		if (LevelController.instance.kizMi)
		{
			GameObject first = Instantiate(kagitKiz, transform);
			transform.GetComponent<Paper>().paperObject1 = first;
		}
		else if (LevelController.instance.orumcekMi)
		{
			GameObject first = Instantiate(kagitOrumcek, transform);
			transform.GetComponent<Paper>().paperObject1 = first;
		}
		else if (LevelController.instance.kare1Mi)
		{
			GameObject first = Instantiate(kagitKare1, transform);
			transform.GetComponent<Paper>().paperObject1 = first;
		}
		else if (LevelController.instance.kare2Mi)
		{
			GameObject first = Instantiate(kagitKare2, transform);
			transform.GetComponent<Paper>().paperObject1 = first;
		}
		else if (LevelController.instance.kartanesi1Mi)
		{
			GameObject first = Instantiate(kagitKartane1, transform);
			transform.GetComponent<Paper>().paperObject1 = first;
		}
		else if (LevelController.instance.kartanesi2Mi)
		{
			GameObject first = Instantiate(KagitKarTane2, transform);
			transform.GetComponent<Paper>().paperObject1 = first;
		}

		//if (LevelController.instance.kagitType == 1)
		//{
		//	GameObject first = Instantiate(kagitDuz,transform);
		//	transform.GetComponent<Paper>().paperObject1 = first;
		//	//transform.GetChild(0).gameObject.SetActive(true);
		//	//transform.GetChild(1).gameObject.SetActive(false);
		//	//transform.GetChild(2).gameObject.SetActive(false);
		//	//transform.GetComponent<Paper>().paperObject1 = transform.GetChild(0).gameObject;
		//}
		//else if(LevelController.instance.kagitType == 2)
		//{
		//	GameObject first = Instantiate(kagitKare1, transform);
		//	transform.GetComponent<Paper>().paperObject1 = first;
		//	//transform.GetChild(0).gameObject.SetActive(false);
		//	//transform.GetChild(1).gameObject.SetActive(true);
		//	//transform.GetChild(2).gameObject.SetActive(false);
		//	//transform.GetComponent<Paper>().paperObject1 = transform.GetChild(1).gameObject;
		//}
		//else if (LevelController.instance.kagitType == 3)
		//{
		//	GameObject first = Instantiate(kagitKare2, transform);
		//	transform.GetComponent<Paper>().paperObject1 = first;
		//	//transform.GetChild(0).gameObject.SetActive(false);
		//	//transform.GetChild(1).gameObject.SetActive(false);
		//	//transform.GetChild(2).gameObject.SetActive(true);
		//	//transform.GetComponent<Paper>().paperObject1 = transform.GetChild(2).gameObject;
		//}
	}

	public void ChangeCharacter()
	{
		if (LevelController.instance.kizMi)
		{
			paper.paperObject2 = kiz;
			paper.paperObject3 = kizTam;
			paper.KesParca1 = kizKes1;
			paper.KesParca2 = kizKes2;
			paper.KesParca3 = kizKes3;
		}
		else if (LevelController.instance.orumcekMi)
		{
			paper.paperObject2 = orumcek;
			paper.paperObject3 = orumcekTam;
			paper.KesParca1 = orumcekKes1;
			paper.KesParca2 = orumcekKes2;
			paper.KesParca3 = orumcekKes3;
		}
		else if (LevelController.instance.kare1Mi)
		{
			paper.paperObject2 = kare1;
			paper.paperObject3 = kare1Tam;
			paper.KesParca1 = kare1Kes1;
			paper.KesParca2 = kare1Kes2;
			paper.KesParca3 = kare1Kes3;
		}
		else if (LevelController.instance.kare2Mi)
		{
			paper.paperObject2 = kare2;
			paper.paperObject3 = kare2Tam;
			paper.KesParca1 = kare2Kes1;
			paper.KesParca2 = kare2Kes2;
			paper.KesParca3 = kare2Kes3;
		}
		else if (LevelController.instance.kartanesi1Mi)
		{
			paper.paperObject2 = kartane1;
			paper.paperObject3 = kartane1Tam;
			paper.KesParca1 = kartane1Kes1;
			paper.KesParca2 = kartane1Kes2;
			paper.KesParca3 = kartane1Kes3;
		}
		else if (LevelController.instance.kartanesi2Mi)
		{
			paper.paperObject2 = kartane2;
			paper.paperObject3 = kartane2Tam;
			paper.KesParca1 = kartane2Kes1;
			paper.KesParca2 = kartane2Kes2;
			paper.KesParca3 = kartane2Kes3;
		}

	}

	void DeactivateAll()
	{
		kiz.SetActive(false);
		orumcek.SetActive(false);
		kare1.SetActive(false);
		kare2.SetActive(false);
		kartane1.SetActive(false);
		kartane2.SetActive(false);
	}

	public void StartingEvents()
	{
		Destroy(paper.paperObject1);
		//Destroy(paper.paperObject2);
		//Destroy(paper.paperObject3);
		ChangePaper();
		transform.localScale = Vector3.one;
		kiz.SetActive(false);
		orumcek.SetActive(false);
		kare1.SetActive(false);
		kare2.SetActive(false);
		kartane1.SetActive(false);
		kartane2.SetActive(false);
		kizTam.SetActive(false);
		orumcekTam.SetActive(false);
		kare1Tam.SetActive(false);
		kare2Tam.SetActive(false);
		kartane1Tam.SetActive(false);
		kartane2Tam.SetActive(false);
		paper.paperObject1.SetActive(true);
		paper.paperObject2.SetActive(false);
		paper.paperObject3.SetActive(false);
		paper.KesParca1.SetActive(true);
		paper.KesParca2.SetActive(true);
		paper.KesParca3.SetActive(true);
		paper.kesildiMi = false;
		paper.acildiMi = false;
		paper.kes = 0;
		paper.type = 0;
		transform.rotation = Quaternion.Euler(0,0,0);
		transform.parent = myParentObject.transform;
		GetComponent<Collider>().enabled = true;
		//paper.paperObject1.GetComponent<Animator>().ResetTrigger("Idle");
		//paper.paperObject1.GetComponent<Animator>().ResetTrigger("katla1");
		//paper.paperObject1.GetComponent<Animator>().ResetTrigger("katla2");
		//paper.paperObject1.GetComponent<Animator>().ResetTrigger("katla3");
		//paper.paperObject1.GetComponent<Animator>().SetTrigger("Idle");
	}
}
