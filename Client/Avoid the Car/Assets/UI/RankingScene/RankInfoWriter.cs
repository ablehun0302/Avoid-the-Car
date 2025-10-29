using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankInfoWriter : MonoBehaviour
{
    [SerializeField] GameObject rankingGroup;

    void Start()
    {
        List<List<string>> rankList = BackendRank.Instance.RankListGet();

        foreach (List<string> rankInfo in rankList)
        {
            GameObject obj = Instantiate(rankingGroup, transform);

            for (int i = 0; i < rankInfo.Count; i++)
            {
                obj.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = rankInfo[i];
            }
        }
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
}
