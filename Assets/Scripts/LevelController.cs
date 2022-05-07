using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElephantSDK;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public int levelNo, tempLevelNo, totalLevelNo; // totallevelno tum leveller bitip random level gelmeye baslayinca kullaniliyor
    public List<GameObject> levels = new List<GameObject>();
    private GameObject currentLevelObj;
    public GameObject firstPaper;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        totalLevelNo = PlayerPrefs.GetInt("level");
        if (totalLevelNo == 0)
        {
            totalLevelNo = 1;
            levelNo = 1;
        }
        UIController.instance.SetLevelText(totalLevelNo);
        LevelStartingEvents();
    }

    public void IncreaseLevelNo()
    {
        tempLevelNo = levelNo;
        totalLevelNo++;
        PlayerPrefs.SetInt("level", totalLevelNo);
        UIController.instance.SetLevelText(totalLevelNo);
    }

    public void LevelStartingEvents()
    {
        if (totalLevelNo > levels.Count)
        {
            levelNo = Random.Range(1, levels.Count + 1);
            if (levelNo == tempLevelNo) levelNo = Random.Range(1, levels.Count + 1);
        }
        else
        {
            levelNo = totalLevelNo;
        }
        UIController.instance.SetLevelText(totalLevelNo);
        currentLevelObj = Instantiate(levels[levelNo - 1], Vector3.zero, Quaternion.identity);
        Elephant.LevelStarted(totalLevelNo);

    }

    public void NextLevelEvents()
    {
        KarakterPaketiMovement.instance.transform.position = Vector3.zero;
        int count = PaperController.instance.papers.Count;
		for (int i = 1; i < count; i++)
		{
            GameObject obj = PaperController.instance.papers[i];
            Destroy(obj);
		}
        PaperController.instance.papers.Clear();
        PaperController.instance.papers.Add(firstPaper);
        firstPaper.transform.position = new Vector3(0,1,0);
		Elephant.LevelCompleted(totalLevelNo);
        Destroy(currentLevelObj);
        IncreaseLevelNo();
        LevelStartingEvents();
    }

    public void LevelRestartEvents()
    {
        UIController.instance.SetLevelText(totalLevelNo);
        currentLevelObj = Instantiate(levels[levelNo - 1], Vector3.zero, Quaternion.identity);
        Elephant.LevelStarted(totalLevelNo);
    }

    public void RestartLevelEvents()
    {
        KarakterPaketiMovement.instance.transform.position = Vector3.zero;
        int count = PaperController.instance.papers.Count;
        for (int i = 1; i < count; i++)
        {
            GameObject obj = PaperController.instance.papers[i];
            Destroy(obj);
        }
        PaperController.instance.papers.Clear();
        PaperController.instance.papers.Add(firstPaper);
        firstPaper.transform.position = new Vector3(0, 1, 0);
        Elephant.LevelFailed(totalLevelNo);
        Destroy(currentLevelObj);
        LevelRestartEvents();
    }
}
