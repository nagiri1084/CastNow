using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCManager : MonoBehaviour
{
    public GameObject[] prefabsMC;

    public void ChangedByCircle()
    {
        Debug.Log("ChangedByCircle");
        prefabsMC[0].SetActive(false); //��ư ������ ����
        prefabsMC[1].SetActive(true); //�� ������ ����
    }
}
