using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance; // singleton yapisi icin gerekli ornek ayrintilar icin BeniOku 22. satirdan itibaren bak.


    [HideInInspector]public int score; // ayrintilar icin benioku 9. satirdan itibaren bak

    [HideInInspector] public bool isContinue;  // ayrintilar icin beni oku 19. satirdan itibaren bak

    public int scoreCarpani;



	private void Awake()
	{
        if (instance == null) instance = this;
        else Destroy(this);
	}

	void Start()
    {
        isContinue = false;
    }


    public void SetScore(int eklenecekScore)
	{
        score += eklenecekScore;
        //if(PlayerController.instance.collectibleVarMi) score += eklenecekScore;

    }

    public void ScoreCarp(int katsayi)
	{
        //if (PlayerController.instance.xVarMi) score *= katsayi;
        //else
        score = 1 * score;
        PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") + score);
    }

    public void FinishGame()
	{
        KarakterPaketiMovement.instance.moveSpeed = 0f;
        Debug.Log("FINISH GAME");
        isContinue = false;
        if (scoreCarpani == 0) scoreCarpani = 1;
        ScoreCarp(scoreCarpani);
        UIController.instance.ActivateWinScreen();
        PaperController.instance.distance = 0;
	}

}
