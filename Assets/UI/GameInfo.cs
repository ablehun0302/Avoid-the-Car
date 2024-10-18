using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] GameObject titleMainGroup;
    [SerializeField] GameObject titleInfoGroup;

    public void InfoButton()
    {
        titleMainGroup.SetActive(false);
        titleInfoGroup.SetActive(true);
    }
}
