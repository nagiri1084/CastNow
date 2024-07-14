using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 접근 용이를 위해 게임 메모리에 올림(Scene이 하나일 때 싱글톤 사용 대신)
    public PoolManager pool;
    public Player player;
    public DrawCircle drawCircle;
    public DrawStar drawStar;
    public MCManager mcManager;

    private void Awake()
    {
        Instance = this; //초기화
    }
}
