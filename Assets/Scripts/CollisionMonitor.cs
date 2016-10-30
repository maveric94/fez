using UnityEngine;
using System.Collections;

public interface ICollisionReceiver
{
    void CollisionEnter(Collision collisionInfo, int monitorID);
    void CollisionExit(Collision collisionInfo, int monitorID);
}


public class CollisionMonitor : MonoBehaviour 
{
    [SerializeField] GameObject target;
    [SerializeField] int monitorID;


    ICollisionReceiver receiver = null;


    void Awake()
    {
        receiver = target.GetComponent<ICollisionReceiver>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (receiver != null)
        {
            receiver.CollisionEnter(collisionInfo, monitorID);
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (receiver != null)
        {
            receiver.CollisionExit(collisionInfo, monitorID);
        }
    }

}
