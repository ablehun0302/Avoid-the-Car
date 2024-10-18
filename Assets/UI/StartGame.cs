using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] GameObject obstacleSpawner;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject titleCanvas;

    void Start()
    {
        player = PlayerMovement.Instance;
    }

    public void StartButton()
    {
        player.enabled = true;
        obstacleSpawner.SetActive(true);
        inGameCanvas.SetActive(true);
        titleCanvas.SetActive(false);
    }
}
