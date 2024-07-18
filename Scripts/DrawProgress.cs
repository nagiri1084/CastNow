using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProgress : MonoBehaviour
{
    public Animator animator;
    private AnimatorStateInfo stateInfo;
    //private float animationProgress = 0f;

    public GameObject magicMapStartBtn;
    private Vector3 originPos;
    private Vector3 magicMapPos;
    private float angleInDegrees = 0;
    private float percent = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // ���콺 ��ġ�� z ���� ī�޶�� �����ϰ� ����
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.name == "MC_Basic_0")
            {
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

                //���� �ۼ�Ʈ�� ��ȯ
                percent = AngleToPercent(angle);
                Debug.Log(percent);

                //�׸��� �ִϸ��̼� ����
                DrawCircle(percent);
            }
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
        angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // ���� A�� B�� ����
        Vector3 crossProduct = Vector3.Cross(vectorA, vectorB);

        // ������ z ���� �����̸� ������ ������ ��ȯ
        if (crossProduct.z < 0)
        {
            angleInDegrees = -angleInDegrees;
        }

        return angleInDegrees;
    }

    float AngleToPercent(float angle)
    { 
        if(angle < 0)
        {
            angle = Mathf.Abs(angle / 180 * 50);
            return angle;
        }
        else
        {
            angle = (1 - angle / 180) * 50 + 50;
            return angle;
        }
    }

    void DrawCircle(float percent)
    {
        animator.gameObject.SetActive(true);

        //���� �ִϸ��̼� ���� ��������
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //�ִϸ��̼� ���� ��Ȳ �ۼ�Ʈ�� ���
        float normalizedTime = stateInfo.normalizedTime % 1;

        if (stateInfo.normalizedTime >= 1f)
        {
            GameManager.Instance.mcManager.ChangedByCircle();
            animator.gameObject.SetActive(false);
        }
        else
        {
            if (normalizedTime >= (percent / 100f) + 0.01f) 
            {
                //�ִϸ��̼� �ۼ�Ʈ�� ���콺 ��ġ ������ �Ѿ�� ���߱�
                Debug.Log(normalizedTime);
                animator.speed = 0f;
            }
            else
            {
                animator.speed = 1f;
            }
        }
    }
}
