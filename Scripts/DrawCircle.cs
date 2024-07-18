using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public Camera mainCamera; // 메인 카메라 참조

    public Vector3 startPoint;
    private int storeStep = 0;
    private bool delay = true;
    private bool isDrawing = false;
    private Vector2 mousePosition;
    private Vector3 currentPosition;
    private float drawThreshold = 1f; // 원을 그리기 위한 최소 거리
    public float drawProgress = 0f; // 원 그리기 진행 상황
    public float drawSpeed = 0.1f; // 원 그리기 속도 (1이면 1초에 한 바퀴)
    public int circleSize = 5;
    public float radius = 1f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        HandClick();
    }

    void HandClick()
    {
        if (Input.GetMouseButton(0)) // 마우스 버튼을 누르면
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDrawing = true;
            //drawProgress = 0f; // 그리기 진행 상황 초기화
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 떼면
        {
            isDrawing = false;
            //circleRenderer.positionCount = 0; // 이전 그리기 초기화
            storeStep = 0;
            drawProgress = 0f;
            delay = true;
        }

        if (isDrawing)
        {
            //Vector3 currentPoint = GetMouseWorldPosition();
            //float radius = Vector3.Distance(startPoint, currentPoint);
            Debug.Log(storeStep + ", " + drawProgress);
            Draw(circleSize, radius, drawProgress);
            if (delay)
            {
                //drawProgress += Time.deltaTime * drawSpeed; // 그리기 진행 업데이트
                drawProgress += Time.deltaTime;
            }
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
        int currentSteps = Mathf.FloorToInt(steps * progress) ;
        if (currentSteps != storeStep) currentSteps = storeStep;
        Debug.Log(currentSteps);
        circleRenderer.positionCount = currentSteps;

        for (int currentStep = storeStep; currentStep < currentSteps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            // 원의 각도 계산.
            // (circumferenceProgress * 2 * Mathf.PI)는 원 전체에 대한 현재 위치의 라디안 값 계산,
            // + (Mathf.PI / 2)는 90도를 더해서 0도 위치에서 시작하도록 각도 조정.
            float currentRadian = (circumferenceProgress * 2 * Mathf.PI) + (Mathf.PI / 2);

            // 각도에 따른 cos, sin 값
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            // cos, sin 값에 따른 실제 x, y 값
            float x = startPoint.x + (xScaled * radius);
            float y = startPoint.y + (yScaled * radius);

            // 현재 점의 위치를 Vector3로 생성. z값은 0으로 고정.
            currentPosition = new Vector3(x, y, 0);

            float distance = Vector2.Distance(currentPosition, mousePosition);
            if (distance < drawThreshold)
            {
                circleRenderer.SetPosition(currentStep, currentPosition);
                delay = true;
            }
            else
            {
                drawProgress = progress;
                delay = false;
                storeStep = currentStep;
                isDrawing = false;
                break;
            }
        }
    }
}