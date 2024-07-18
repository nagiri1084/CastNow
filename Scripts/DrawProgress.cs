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
            // ���콺 ��ġ�� z ���� ī�޶�� �����ϰ� ����
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            originPos = GameManager.Instance.player.transform.position;

            // magicMapPos�� z ���� originPos�� �����ϰ� ����
            magicMapPos = magicMapStartBtn.transform.position;
           
            //Debug.Log(mousePos + "," + originPos + "," + magicMapPos);

            //�� �� ���� ����
            Vector3 vectorA = originPos - mousePos;
            Vector3 vectorB = originPos - magicMapPos;

            //�� ���� ���� ���� ���
            float angle = CalculateAngleBetweenVectors(vectorA, vectorB);
            Debug.Log(angle + " degrees");
        }
    }

    float CalculateAngleBetweenVectors(Vector3 vectorA, Vector3 vectorB)
    {
        // ���� A�� B�� ����
        float dotProduct = Vector3.Dot(vectorA, vectorB);

        // ���� A�� B�� ũ��
        float magnitudeA = vectorA.magnitude;
        float magnitudeB = vectorB.magnitude;

        // �ڻ��� �� �� ���
        float cosTheta = dotProduct / (magnitudeA * magnitudeB);

        // �� �� ��� (���ڻ��� �Լ� ���)
        float angleInRadians = Mathf.Acos(cosTheta);

        // ������ ������ ��ȯ
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // ���� A�� B�� ����
        Vector3 crossProduct = Vector3.Cross(vectorA, vectorB);

        // ������ z ���� �����̸� ������ ������ ��ȯ
        if (crossProduct.z < 0)
        {
            angleInDegrees = -angleInDegrees;
        }

        return angleInDegrees;
    }
}
