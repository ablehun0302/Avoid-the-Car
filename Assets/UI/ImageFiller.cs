using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImageFiller : MonoBehaviour
{
    PlayerMovement player;
    Image cooldownImage;

    void Start()
    {
        player = PlayerMovement.Instance;
        cooldownImage = GetComponent<Image>();
    }

    void Update()
    {
        if (player.Timer == 0) { cooldownImage.fillAmount = 1; }
        else { cooldownImage.fillAmount = player.Timer / 5; }
    }
}
