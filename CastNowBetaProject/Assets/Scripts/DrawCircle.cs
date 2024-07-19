using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public Camera mainCamera; // ���� ī�޶� ����

    public Vector3 startPoint;
    private int storeStep = 0;
    private bool delay = true;
    private bool isDrawing = false;
    private Vector2 mousePosition;
    private Vector3 currentPosition;
    private float drawThreshold = 1f; // ���� �׸��� ���� �ּ� �Ÿ�
    public float drawProgress = 0f; // �� �׸��� ���� ��Ȳ
    public float drawSpeed = 0.1f; // �� �׸��� �ӵ� (1�̸� 1�ʿ� �� ����)
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
        if (Input.GetMouseButton(0)) // ���콺 ��ư�� ������
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDrawing = true;
            //drawProgress = 0f; // �׸��� ���� ��Ȳ �ʱ�ȭ
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� ����
        {
            isDrawing = false;
            //circleRenderer.positionCount = 0; // ���� �׸��� �ʱ�ȭ
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
                //drawProgress += Time.deltaTime * drawSpeed; // �׸��� ���� ������Ʈ
                drawProgress += Time.deltaTime;
            }
            if (drawProgress >= 1f)
            {
                drawProgress = 1f; // �׸��Ⱑ �Ϸ�Ǹ� ������ 1�� ����
                GameManager.Instance.mcManager.ChangedByCircle();
                circleRenderer.positionCount = 0; //�ʱ�ȭ
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

            // ���� ���� ���.
            // (circumferenceProgress * 2 * Mathf.PI)�� �� ��ü�� ���� ���� ��ġ�� ���� �� ���,
            // + (Mathf.PI / 2)�� 90���� ���ؼ� 0�� ��ġ���� �����ϵ��� ���� ����.
            float currentRadian = (circumferenceProgress * 2 * Mathf.PI) + (Mathf.PI / 2);

            // ������ ���� cos, sin ��
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            // cos, sin ���� ���� ���� x, y ��
            float x = startPoint.x + (xScaled * radius);
            float y = startPoint.y + (yScaled * radius);

            // ���� ���� ��ġ�� Vector3�� ����. z���� 0���� ����.
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