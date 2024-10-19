using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물을 담고있는 부모 오브젝트를 삭제하는 스크립트
/// </summary>
public class ParentRemover : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount == 0) { Destroy(gameObject); }
    }
}
