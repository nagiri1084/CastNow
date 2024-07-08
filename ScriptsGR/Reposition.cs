using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    //�� ���� ������ ���� �� ���ġ
    //���� �÷��̾�� �ʹ� �־����� �� �� ���ġ
    //�÷����� Area(����)���� ����� �� ���ġ

    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾� pos ����
        Vector3 myPos = transform.position; //�� �ڽ��� pos ����
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        //�÷��̾ ��� �������� ���� �ִ��� �ľ�
        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        //���� ���� �±� �ľ�
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
