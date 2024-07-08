using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    //public RuntimeAnimatorController[] animCam;
    public Rigidbody2D target;

    bool isLive = true; //적 생존 여부

    Rigidbody2D rigid;
    SpriteRenderer spriter;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position; //플레이어와 적의 위치 차이 (방향 = 위치 차이의 정규화)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //다음에 가야 할 위치 양
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; //플레이어와 적 충돌 시 물리적 튕김 제거
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x; //플레이어 방향으로 바라보며 쫒아가기
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;
        if(health > 0)
        {
            //Live
        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
