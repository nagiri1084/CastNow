using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //Ǯ�� �Լ� ��� (instantiate & destroy ��� ������� ���� ����)

    // ��������� ������ ����
    public GameObject[] prefabs;

    //Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;


    private void Awake()
    {
        // ������ ���� ������ Ǯ ��� ����Ʈ�� ���
        pools = new List<GameObject>[prefabs.Length]; //�迭 �ʱ�ȭ

        for(int index=0; index < pools.Length; index++) //��ȸ
        {
            pools[index] = new List<GameObject>(); //����Ʈ �ʱ�ȭ
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ) ���ӿ�����Ʈ ����
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //�߰� �� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            //�߰� ���� �� ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        
        return select;
    }
}
