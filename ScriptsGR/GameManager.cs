using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // ���� ���̸� ���� ���� �޸𸮿� �ø�(Scene�� �ϳ��� �� �̱��� ��� ���)
    public PoolManager pool;
    public Player player;
    public DrawCircle drawCircle;
    public DrawStar drawStar;
    public MCManager mcManager;

    private void Awake()
    {
        Instance = this; //�ʱ�ȭ
    }
}
