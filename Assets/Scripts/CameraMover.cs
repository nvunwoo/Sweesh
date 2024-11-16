using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 targetPosition;
    public float smoothTime = 0.3f;  // 감속 시간

    private Vector3 velocity = Vector3.zero;

    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );

            // 목표 위치에 거의 도달했을 때 이동을 멈춤
            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.01f)
            {
                mainCamera.transform.position = targetPosition;
                shouldMove = false;
            }
        }
    }

    public void StartMoveCamera()
    {
        shouldMove = true;
    }
}
