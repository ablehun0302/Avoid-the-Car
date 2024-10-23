using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] GameObject titleCanvas;
    [SerializeField] GameObject mainGroup;
    [SerializeField] GameObject infoGroup;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;

    public void MainButton()
    {
        GameManager.Instance.GameReset();
        titleCanvas.SetActive(true);    //켜기
        mainGroup.SetActive(true);      //켜기
        infoGroup.SetActive(false);
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void InfoButton()
    {
        mainGroup.SetActive(false);
        infoGroup.SetActive(true);
    }

    public void StartButton()
    {
        GameManager.Instance.GameStart();
    }

    public void RestartButton()
    {
        GameManager.Instance.GameReset();
        GameManager.Instance.GameStart();
    }
}