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
    public GameObject Shadow;
    public float onDamagedTime = 0.2f;

    bool isLive = true; //�� ���� ����

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    SpriteRenderer spriterShadow;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        spriterShadow = Shadow.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position; //�÷��̾�� ���� ��ġ ���� (���� = ��ġ ������ ����ȭ)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //������ ���� �� ��ġ ��
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; //�÷��̾�� �� �浹 �� ������ ƨ�� ����
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x; //�÷��̾� �������� �ٶ󺸸� �i�ư���
        spriterShadow.flipX = target.position.x < rigid.position.x;
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
            StartCoroutine(OnDamaged());

        }
        else
        {
            Dead();
        }
    }

    IEnumerator OnDamaged()
    {
        spriter.enabled = false;
        spriterShadow.enabled = true;
        yield return new WaitForSeconds(onDamagedTime);

        OffDamaged();
    }

    void OffDamaged()
    {
        spriter.enabled = true;
        spriterShadow.enabled = false;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
