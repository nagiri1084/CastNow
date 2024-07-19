using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //무기 관리
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float createTime;

    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * createTime * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if(timer > createTime)
                {
                    timer = 0f;
                    Fire();
                }
                break;

        }
    }


    private void Init()
    {
        switch (id)
        {
            //case 0:
            //    speed = 150;
            //    Batch();
            //    break;

            default:
                createTime = 0.5f;
                break;
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
