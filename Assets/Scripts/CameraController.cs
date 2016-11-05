using UnityEngine;
using System;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public static event Action OnCameraRotated;

    #region Fields

    [SerializeField] Rigidbody characterRigidBody;
    [SerializeField] float distanceFromCharacter;
    [SerializeField] float rotationTime;

    bool rotating = false;

    float oldYRotation;
    float newYRotation;
    float currentRotationTime;

    #endregion


    #region Properties

    public bool Rotating
    {
        get { return rotating; }
    }

    #endregion


    #region Unity lifecycle
	
    void LateUpdate()
    {        
        if (!rotating)
        {
            oldYRotation = transform.eulerAngles.y;
            currentRotationTime = 0.0f;

            if (Input.GetKeyDown(KeyCode.A))
            {
                newYRotation = oldYRotation + 90.0f;
                rotating = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                newYRotation = oldYRotation - 90.0f;
                rotating = true;
            }
        }
        else
        {
            currentRotationTime += Time.deltaTime;
            float yRotation;

            if (currentRotationTime >= rotationTime)
            {
                rotating = false;
                yRotation = newYRotation;

                StartCoroutine(CameraRotated());
            }
            else
            {
                yRotation = Mathf.Lerp(oldYRotation, newYRotation, currentRotationTime / rotationTime);
            }

            transform.localEulerAngles = new Vector3(0.0f, yRotation, 0.0f);
        }         

        UpdatePosition();
    }

    #endregion


    #region Public methods

    public void SetCharacterRigidBody(Rigidbody body)
    {
        characterRigidBody = body;
    }

    #endregion

    #region Private methods

    IEnumerator CameraRotated()
    {
        yield return new WaitForEndOfFrame();

        if (OnCameraRotated != null)
        {
            OnCameraRotated();
        }

        yield break;

    }

    void UpdatePosition()
    {
        Vector3 back = Vector3.one - transform.forward;
        transform.position = -Vector3.Scale(transform.forward, new Vector3(distanceFromCharacter, 0.0f, distanceFromCharacter)) + Vector3.Scale(back, characterRigidBody.position);
    }

    #endregion
}
