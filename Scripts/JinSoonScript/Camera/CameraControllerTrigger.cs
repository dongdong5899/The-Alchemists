using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

[Serializable]
public class CustomInspectorObj
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.4f;
}

public class CameraControllerTrigger : MonoBehaviour
{
    public CustomInspectorObj inspectorObj;
    private Collider2D _collider;

    public Action<Player> OnAfterSwapCameraToLeft;
    public Action<Player> OnAfterSwapCameraToRight;

    public UnityEvent OnSwapCameraEvent;

    //카메라 막 연속으로 움직일 때도 얘 사용하면 됨
    public UnityEvent OnAfterPanDirection;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (inspectorObj.panCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(inspectorObj.panDistance, inspectorObj.panTime, inspectorObj.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (inspectorObj.panCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(inspectorObj.panDistance, inspectorObj.panTime, inspectorObj.panDirection, true);
            }

            Vector2 exitDirection =
                (other.transform.position - _collider.transform.position).normalized;

            if (inspectorObj.swapCameras)
            {
                if (exitDirection.x > 0)
                {
                    if (inspectorObj.cameraOnRight != null)
                        CameraManager.Instance.ChangeCam(inspectorObj.cameraOnRight);

                    OnAfterSwapCameraToRight?.Invoke(player);
                }
                else
                {
                    if (inspectorObj.cameraOnLeft != null)
                        CameraManager.Instance.ChangeCam(inspectorObj.cameraOnLeft);

                    OnAfterSwapCameraToLeft?.Invoke(player);
                }
                OnSwapCameraEvent?.Invoke();
            }
        }

    }
}
