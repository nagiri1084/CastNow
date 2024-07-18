using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProgress : MonoBehaviour
{
    public GameObject magicMapStartBtn;
    private Vector3 mousePos;
    private Vector3 originPos;
    private Vector3 magicMapPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치의 z 값을 카메라와 동일하게 설정
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            originPos = GameManager.Instance.player.transform.position;

            // magicMapPos의 z 값을 originPos와 동일하게 설정
            magicMapPos = magicMapStartBtn.transform.position;
           
            //Debug.Log(mousePos + "," + originPos + "," + magicMapPos);

            //두 점 사이 벡터
            Vector3 vectorA = originPos - mousePos;
            Vector3 vectorB = originPos - magicMapPos;

            //두 벡터 사이 각도 계산
            float angle = CalculateAngleBetweenVectors(vectorA, vectorB);
            Debug.Log(angle + " degrees");
        }
    }

    float CalculateAngleBetweenVectors(Vector3 vectorA, Vector3 vectorB)
    {
        // 벡터 A와 B의 내적
        float dotProduct = Vector3.Dot(vectorA, vectorB);

        // 벡터 A와 B의 크기
        float magnitudeA = vectorA.magnitude;
        float magnitudeB = vectorB.magnitude;

        // 코사인 θ 값 계산
        float cosTheta = dotProduct / (magnitudeA * magnitudeB);

        // θ 값 계산 (역코사인 함수 사용)
        float angleInRadians = Mathf.Acos(cosTheta);

        // 라디안을 각도로 변환
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // 벡터 A와 B의 외적
        Vector3 crossProduct = Vector3.Cross(vectorA, vectorB);

        // 외적의 z 값이 음수이면 각도를 음수로 변환
        if (crossProduct.z < 0)
        {
            angleInDegrees = -angleInDegrees;
        }

        return angleInDegrees;
    }
}
