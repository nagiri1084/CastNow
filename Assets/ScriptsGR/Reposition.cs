using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    //맵 무한 생성을 위한 맵 재배치
    //적이 플레이어와 너무 멀어졌을 때 적 재배치
    //플레리어 Area(영역)에서 벗어났을 때 재배치

    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어 pos 저장
        Vector3 myPos = transform.position; //나 자신의 pos 저장
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        //플레이어가 어느 방향으로 가고 있는지 파악
        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        //현재 나의 태그 파악
        switch (transform.tag)
        {
            case "Ground":
                Debug.Log("RePosition Ground");
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 56);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 56);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Debug.Log("RePosition Enemy");
                    transform.Translate(playerDir * 28 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }

}
