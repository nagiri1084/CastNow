using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�÷��̾� �̵�

    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");

        Vector3 playerDir = this.inputVec;
        spriter.flipX = playerDir.x > 0 ; //�÷��̾� �������� �ٶ󺸸� �i�ư���
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime; //�밢�� �̵� �ӵ� ����
        rigid.MovePosition(rigid.position + nextVec);
    }
}
