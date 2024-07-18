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
            // 마우스 위치의 z 값을 카메라와 동일하게 설정
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.name == "MC_Basic_0")
            {
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

                //각도 퍼센트로 변환
                percent = AngleToPercent(angle);
                Debug.Log(percent);

                //그리기 애니메이션 시작
                DrawCircle(percent);
            }
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
        angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // 벡터 A와 B의 외적
        Vector3 crossProduct = Vector3.Cross(vectorA, vectorB);

        // 외적의 z 값이 음수이면 각도를 음수로 변환
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

        //현재 애니메이션 상태 가져오기
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //애니메이션 진행 상황 퍼센트로 계산
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
                //애니메이션 퍼센트가 마우스 터치 각도를 넘어가면 멈추기
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
