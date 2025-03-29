using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CameraControllerTrigger))]
public class CameraControllerEditor : Editor
{
    private CameraControllerTrigger _trigger;

    private void OnEnable()
    {
        _trigger = target as CameraControllerTrigger;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CustomInspectorObj inspectorObj = _trigger.inspectorObj;
        if (inspectorObj.swapCameras)
        {
            inspectorObj.cameraOnLeft =
                EditorGUILayout.ObjectField(
                    "Camera on left",
                    inspectorObj.cameraOnLeft,
                    typeof(CinemachineVirtualCamera),
                    true) as CinemachineVirtualCamera;
            inspectorObj.cameraOnRight =
                EditorGUILayout.ObjectField(
                    "Camera on right",
                    inspectorObj.cameraOnRight,
                    typeof(CinemachineVirtualCamera),
                    true) as CinemachineVirtualCamera;
        }

        if (inspectorObj.panCameraOnContact)
        {
            inspectorObj.panDirection =
                (PanDirection)EditorGUILayout.EnumPopup("Camera moving direction", inspectorObj.panDirection);

            inspectorObj.panDistance = EditorGUILayout.FloatField("Pan Distance", inspectorObj.panDistance);
            inspectorObj.panTime = EditorGUILayout.FloatField("Pan Time", inspectorObj.panTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_trigger);
        }
    }
}
