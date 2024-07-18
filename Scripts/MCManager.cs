using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCManager : MonoBehaviour
{
    public GameObject[] prefabsMC;

    public void ChangedByCircle()
    {
        Debug.Log("ChangedByCircle");
        prefabsMC[0].SetActive(false); //버튼 마법진 제거
        prefabsMC[1].SetActive(true); //원 마법진 생성
    }
}
