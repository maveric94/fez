using UnityEngine;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour 
{
    #region Fields

    [SerializeField] new CameraController camera;
    [SerializeField] Character character;

    List<LevelBlockBase> levelObjects;

    #endregion


    #region unity lifecycle

    void Awake()
    {
        GetComponentsInChildren<LevelBlockBase>(levelObjects);
    }

    void OnEnable()
    {
        CameraController.OnCameraRotated += CameraController_OnCameraRotated;
    }

    void OnDisable()
    {
        CameraController.OnCameraRotated -= CameraController_OnCameraRotated;
    }

    #endregion



    #region Event handlers

    void CameraController_OnCameraRotated ()
    {
        RaycastHit hitInfo;
        Vector3 forward = camera.transform.forward;

        if (Physics.Raycast(camera.transform.position + new Vector3(0.0f, -0.51f, 0.0f), forward, out hitInfo))
        {            
            bool inverce = Math.Sign(forward.x) < 0;

            Vector3 diff = character.GetPosition() - hitInfo.point - Vector3.Scale(new Vector3(0.5f, 0.0f, 0.5f), forward);

            character.MoveBy(Vector3.Scale(inverce ? diff : -diff, forward));
        }
    }

    #endregion
}
