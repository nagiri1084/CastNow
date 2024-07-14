using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public Camera mainCamera; // ���� ī�޶� ����

    public Vector3 startPoint;
    private bool isDrawing = false;
    public float drawProgress = 0f; // �� �׸��� ���� ��Ȳ
    public float drawSpeed = 1f; // �� �׸��� �ӵ� (1�̸� 1�ʿ� �� ����)
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
        if (Input.GetMouseButtonDown(0)) // ���콺 ��ư�� ������
        {
            //startPoint = GetMouseWorldPosition();
            isDrawing = true;
            drawProgress = 0f; // �׸��� ���� ��Ȳ �ʱ�ȭ
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� ����
        {
            isDrawing = false;
            circleRenderer.positionCount = 0; // ���� �׸��� �ʱ�ȭ
        }

        if (isDrawing)
        {
            //Vector3 currentPoint = GetMouseWorldPosition();
            //float radius = Vector3.Distance(startPoint, currentPoint);
            Draw(circleSize, radius, drawProgress);
            drawProgress += Time.deltaTime * drawSpeed; // �׸��� ���� ������Ʈ

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
        int currentSteps = Mathf.FloorToInt(steps * progress);
        circleRenderer.positionCount = currentSteps;

        for (int currentStep = 0; currentStep < currentSteps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            // ���� ���� ���.
            // (circumferenceProgress * 2 * Mathf.PI)�� �� ��ü�� ���� ���� ��ġ�� ���� �� ���,
            // - (Mathf.PI / 2)�� 90���� ���� 0�� ��ġ���� �����ϵ��� ���� ����.
            float currentRadian = (circumferenceProgress * 2 * Mathf.PI) + (Mathf.PI / 2) ;

            // ������ ���� cos, sin ��
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            // cos, sin ���� ���� ���� x, y ��
            float x = startPoint.x + (xScaled * radius);
            float y = startPoint.y + (yScaled * radius);

            // ���� ���� ��ġ�� Vector3�� ����. z���� 0���� ����.
            Vector3 currentPosition = new Vector3(x, y, 0);
            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    //Vector3 GetMouseWorldPosition()
    //{
    //    Vector3 mousePosition = Input.mousePosition;
    //    mousePosition.z = Mathf.Abs(mainCamera.transform.position.z); // ī�޶��� z ��ġ�� ����Ͽ� ���� ����
    //    return mainCamera.ScreenToWorldPoint(mousePosition);
    //}
}
