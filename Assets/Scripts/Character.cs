using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour, ICollisionReceiver
{
    #region Fields

    [SerializeField] Rigidbody body;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [SerializeField] new CameraController camera;

    int collideCount = 0;

    #endregion


    #region Unity lifecycle

    void FixedUpdate()
    {
        if (!camera.Rotating)
        {
            Vector3 direction = camera.transform.right;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                body.position -= Vector3.Scale(direction, new Vector3(speed, 0.0f, speed)) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {   
                body.position += Vector3.Scale(direction, new Vector3(speed, 0.0f, speed)) * Time.deltaTime;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (collideCount > 0)
                {
                    body.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
                }
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
}
