using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public Camera mainCamera; // 메인 카메라 참조

    public Vector3 startPoint;
    private bool isDrawing = false;
    public float drawProgress = 0f; // 원 그리기 진행 상황
    public float drawSpeed = 1f; // 원 그리기 속도 (1이면 1초에 한 바퀴)
    public int circleSize = 100;
    public float radius = 1f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 버튼을 누르면
        {
            //startPoint = GetMouseWorldPosition();
            isDrawing = true;
            drawProgress = 0f; // 그리기 진행 상황 초기화
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 떼면
        {
            isDrawing = false;
            circleRenderer.positionCount = 0; // 이전 그리기 초기화
        }

        if (isDrawing)
        {
            //Vector3 currentPoint = GetMouseWorldPosition();
            //float radius = Vector3.Distance(startPoint, currentPoint);
            Draw(circleSize, radius, drawProgress);
            drawProgress += Time.deltaTime * drawSpeed; // 그리기 진행 업데이트

            if (drawProgress >= 1f)
            {
                drawProgress = 1f; // 그리기가 완료되면 진행을 1로 고정
                GameManager.Instance.mcManager.ChangedByCircle();
                circleRenderer.positionCount = 0; //초기화
            }
        }
    }

    void Draw(int steps, float radius, float progress)
    {
        int currentSteps = Mathf.FloorToInt(steps * progress);
        circleRenderer.positionCount = currentSteps;

        for (int currentStep = 0; currentStep < currentSteps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            // 원의 각도 계산.
            // (circumferenceProgress * 2 * Mathf.PI)는 원 전체에 대한 현재 위치의 라디안 값 계산,
            // - (Mathf.PI / 2)는 90도를 빼서 0도 위치에서 시작하도록 각도 조정.
            float currentRadian = (circumferenceProgress * 2 * Mathf.PI) + (Mathf.PI / 2) ;

            // 각도에 따른 cos, sin 값
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            // cos, sin 값에 따른 실제 x, y 값
            float x = startPoint.x + (xScaled * radius);
            float y = startPoint.y + (yScaled * radius);

            // 현재 점의 위치를 Vector3로 생성. z값은 0으로 고정.
            Vector3 currentPosition = new Vector3(x, y, 0);
            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    //Vector3 GetMouseWorldPosition()
    //{
    //    Vector3 mousePosition = Input.mousePosition;
    //    mousePosition.z = Mathf.Abs(mainCamera.transform.position.z); // 카메라의 z 위치를 사용하여 깊이 설정
    //    return mainCamera.ScreenToWorldPoint(mousePosition);
    //}
}
