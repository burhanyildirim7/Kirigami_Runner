using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperController : MonoBehaviour
{
	public static PaperController instance;
    public GameObject tozEfecti;
	private void Awake()
	{
		if (instance == null) instance = this;
	}


    public GameObject kagitTip1, kagitTip2, kagitTip3;
    public int hazirKagitSayisi;
	public List<GameObject> papers = new();
	public float movementDelay = 0.25f;

	private void Update()
	{
		if (Input.GetMouseButton(0) && GameController.instance.isContinue)
		{
            MoveListElements();
		}

		if (Input.GetMouseButtonUp(0) && GameController.instance.isContinue)
		{
            MoveOrigin();
		}
	}
	public void StackPaper(GameObject other, int index)
    {
       
        other.transform.parent = transform;
        Vector3 newPos = papers[index].transform.localPosition;
        float posZ = 1.1f;
        if (LevelController.instance.kagitType == 1) posZ = 1.1f;
        else if (LevelController.instance.kagitType == 2) posZ = 2.5f;
        else if (LevelController.instance.kagitType == 3) posZ = 3.2f;
        newPos.z += posZ;
        other.transform.localPosition = newPos;
        papers.Add(other);
        StartCoroutine(ScalePapers());
        GameController.instance.SetScore(1);
    }

    public IEnumerator ScalePapers()
    {
        for (int i = papers.Count - 1; i >= 0; i--)
        {
            int index = i;
            Vector3 scale = Vector3.one * 1.5f;
            if(index >= 0)papers[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            papers[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            yield return new WaitForSeconds(.05f);
        }
    }
    public void MoveListElements()
    {
        for (int i = 1; i < papers.Count; i++)
        {
            Vector3 pos = papers[i].transform.localPosition;
            pos.x = papers[i - 1].transform.localPosition.x;
            papers[i].transform.DOLocalMove(pos, movementDelay);
        }
    }
    public void MoveOrigin()
    {
        for (int i = 1; i < papers.Count; i++)
        {
            Vector3 pos = papers[i].transform.localPosition;
            pos.x = papers[0].transform.localPosition.x;
            papers[i].transform.DOLocalMove(pos, movementDelay * 3);
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("finish"))
		{
            Destroy(other.gameObject);
            GameController.instance.FinishGame();
		}
	}

    public void SiradakiKagidiAc()
	{
        bool bitti = true;
        foreach(GameObject obj in papers)
		{        
			if (obj.GetComponent<Paper>().kesildiMi)
			{
                bitti = false;
                obj.GetComponent<Paper>().paperObject2.SetActive(false);
                obj.GetComponent<Paper>().paperObject3.SetActive(true);
                hazirKagitSayisi--;
                //if (hazirKagitSayisi == 0) GameController.instance.FinishGame();
                return;
			}
            
        }

        if (bitti) GameController.instance.FinishGame();
	}


   


}
