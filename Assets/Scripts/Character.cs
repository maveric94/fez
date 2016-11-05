using UnityEngine;
using System.Collections;
using System;

public class Character : MonoBehaviour, ICollisionReceiver
{
    [Flags]
    public enum Direction : int
    {
        None    = 0x0,
        Up      = 0x1,
        Down    = 0x2,
        Left    = 0x4,
        Right   = 0x8,
    }

    #region Fields

    [SerializeField] Rigidbody body;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] Collider characterCollider;

    [SerializeField] new CameraController camera;

    int collideCount = 0;
    bool jumpDelay;

    RaycastHit prevHit;

    Direction currentDirection = Direction.None;
    Vector3 startPosition;

    #endregion


    #region Unity lifecycle


    void Start()
    {
        startPosition = body.position;
    }


    void OnEnable()
    {
        CameraController.OnCameraRotated += CameraController_OnCameraRotated;
    }


    void OnDisable()
    {
        CameraController.OnCameraRotated -= CameraController_OnCameraRotated;
    }

    void FixedUpdate()
    {
        if (!camera.Rotating)
        {
            Vector3 direction = camera.transform.right;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                body.position -= Vector3.Scale(direction, new Vector3(speed, 0.0f, speed)) * Time.deltaTime;
                currentDirection &= Direction.Left;
            }
            else
            {
                currentDirection &= ~Direction.Left;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {   
                body.position += Vector3.Scale(direction, new Vector3(speed, 0.0f, speed)) * Time.deltaTime;
                currentDirection &= Direction.Right;
            }
            else
            {
                currentDirection &= ~Direction.Right;   
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (collideCount > 0 && !jumpDelay)
                {
                    body.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
                    jumpDelay = true;
                    StartCoroutine(JumpDelayCoroutine());

                    currentDirection &= Direction.Up;
                }
            }

            UpdateCharacterPosition();

            if (body.position.y < -10.0f)
            {
                body.position = startPosition;
            }
        }
    }


    #endregion


    #region ICollisionReceiver

    public void CollisionEnter(Collision collisionInfo, int monitorID)
    {
        collideCount++;
    }


    public void CollisionExit(Collision collisionInfo, int monitorID)
    {
        collideCount--;
        collideCount = Mathf.Max(collideCount, 0);
    }

    #endregion


    #region Public methods

    public void MoveBy(Vector3 position)
    {
        body.position += position;
    }


    public Vector3 GetPosition()
    {
        return body.position;
    }

    #endregion


    #region Private methods

    IEnumerator JumpDelayCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);

        jumpDelay = false;   

        yield break;
    }


    void UpdateCharacterPosition()
    {
        RaycastHit upperRay, lowerRay;
        Vector3 forward = camera.transform.forward;

        if (Physics.Raycast(body.position + Vector3.Scale(-forward, Vector3.one * 100.0f) + new Vector3(0.0f, -0.51f, 0.0f), forward, out lowerRay))
        {
            bool upperRaycast = Physics.Raycast(body.position + Vector3.Scale(-forward, Vector3.one) + new Vector3(0.0f, 0.51f, 0.0f), forward, out upperRay);
            Debug.Log("Lower ray: " + lowerRay.collider.gameObject.name + " \nUpper ray: " + (upperRay.collider ? upperRay.collider.gameObject.name : "null"));

            if (!upperRaycast)
            {
                TryMoveCharacterToRaycastResult(ref lowerRay);
            }

            if (body.velocity.y < 0.0f)
            {
                TryMoveCharacterToRaycastResult(ref lowerRay);
            }
        }
    }        


    bool TryMoveCharacterToRaycastResult(ref RaycastHit hitInfo, bool force = false)
    {
        Vector3 forward = camera.transform.forward;
        bool inverce = Math.Sign(forward.x) < 0;
        Vector3 diff = GetPosition() - hitInfo.point - Vector3.Scale(new Vector3(0.5f, 0.0f, 0.5f), forward);
        diff = Vector3.Scale(inverce ? diff : -diff, forward);

        if (!Physics.CheckSphere(body.position + diff, 0.4f) || force)
        {                    
            MoveBy(diff);
            return true;
        }
        else
        {
            return false;
        }
    }


    #endregion


    #region  Event handlers

    void CameraController_OnCameraRotated()
    {
        RaycastHit hitInfo;
        Vector3 forward = camera.transform.forward;

        if (Physics.Raycast(camera.transform.position + new Vector3(0.0f, -0.51f, 0.0f), forward, out hitInfo))
        {            
            TryMoveCharacterToRaycastResult(ref hitInfo, true);
        }
    }

    #endregion
}
