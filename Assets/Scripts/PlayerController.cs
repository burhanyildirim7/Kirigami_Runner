using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int collectibleDegeri;
    public bool xVarMi = true;
    public bool collectibleVarMi = true;
    public List<GameObject> papers = new();
    public float movementDelay = 0.25f;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        StartingEvents();
    }

    public void StackPaper(GameObject other, int index)
	{
        other.transform.parent = transform;
        Vector3 newPos = papers[index].transform.localPosition;
        newPos.z += 1;
        other.transform.localPosition = newPos;
        papers.Add(other);
        StartCoroutine(ScalePapers());
	}

    public IEnumerator ScalePapers()
	{
		for (int i = papers.Count-1; i > 0; i--)
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
        Debug.Log("çalıştı");
        for (int i = 1; i < papers.Count; i++)
		{           
            Vector3 pos = papers[i].transform.localPosition;
            pos.x = papers[i - 1].transform.localPosition.x;
            papers[i].transform.DOLocalMove(pos,movementDelay);
		}
	}


    public void MoveOrigin()
	{
        for (int i = 1; i < papers.Count; i++)
        {
            Vector3 pos = papers[i].transform.localPosition;
            pos.x = papers[0].transform.localPosition.x;
            papers[i].transform.DOLocalMove(pos, movementDelay*3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("collectible"))
        {
            // COLLECTIBLE CARPINCA YAPILACAKLAR...
            GameController.instance.SetScore(collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku

        }
        else if (other.CompareTag("engel"))
        {
            // ENGELELRE CARPINCA YAPILACAKLAR....
            GameController.instance.SetScore(-collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
            if (GameController.instance.score < 0) // SKOR SIFIRIN ALTINA DUSTUYSE
			{
                // FAİL EVENTLERİ BURAYA YAZILACAK..
                GameController.instance.isContinue = false; // çarptığı anda oyuncunun yerinde durması ilerlememesi için
                UIController.instance.ActivateLooseScreen(); // Bu fonksiyon direk çağrılada bilir veya herhangi bir effect veya animasyon bitiminde de çağrılabilir..
                // oyuncu fail durumunda bu fonksiyon çağrılacak.. 
			}


        }
        else if (other.CompareTag("finish")) 
        {
            // finishe collider eklenecek levellerde...
            // FINISH NOKTASINA GELINCE YAPILACAKLAR... Totalscore artırma, x işlemleri, efektler v.s. v.s.
            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(7);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
            // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
            UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
            // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..

            
        }//else if (other.CompareTag("paper"))
        //{
        //    if (!papers.Contains(other.gameObject))
        //    {
        //        other.GetComponent<Collider>().isTrigger = false;
        //        other.gameObject.tag = "Untagged";
        //        other.gameObject.AddComponent<PaperController>();
        //        other.gameObject.AddComponent<Rigidbody>();
        //        other.GetComponent<Rigidbody>().isKinematic = true;
        //        StackPaper(other.gameObject, papers.Count - 1);
        //    }
        //}

    }


    public void StartingEvents()
    {

        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance.score = 0;
        transform.position = new Vector3(0, transform.position.y, 0);
        GetComponent<Collider>().enabled = true;

    }

}
