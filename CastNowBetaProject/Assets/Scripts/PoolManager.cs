using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //풀링 함수 사용 (instantiate & destroy 사용 방법보다 관리 용이)

    // 프리펩들을 보관할 변수
    public GameObject[] prefabs;

    //풀 담당을 하는 리스트들
    List<GameObject>[] pools;


    private void Awake()
    {
        // 프리팹 보관 변수와 풀 담당 리스트는 비례
        pools = new List<GameObject>[prefabs.Length]; //배열 초기화

        for(int index=0; index < pools.Length; index++) //순회
        {
            pools[index] = new List<GameObject>(); //리스트 초기화
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 놀고 있는(비활성화) 게임오브젝트 접근
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //발견 시 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            //발견 못할 시 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        
        return select;
    }
}
