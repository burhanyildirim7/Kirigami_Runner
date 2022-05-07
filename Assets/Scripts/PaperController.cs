using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperController : MonoBehaviour
{
	public static PaperController instance;

	private void Awake()
	{
		if (instance == null) instance = this;
	}

	public List<GameObject> papers = new();
	public float movementDelay = 0.25f;

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
            MoveListElements();
		}

		if (Input.GetMouseButtonUp(0))
		{
            MoveOrigin();
		}
	}
	public void StackPaper(GameObject other, int index)
    {
        other.transform.parent = transform;
        Vector3 newPos = papers[index].transform.localPosition;
        newPos.z += 1;
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
            papers[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
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
}
