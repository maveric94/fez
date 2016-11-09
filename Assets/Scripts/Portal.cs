using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour, ICollisionReceiver
{
    [SerializeField] Transform exit;

    public void CollisionEnter(Collision collisionInfo, int monitorID)
    {
        Character character = collisionInfo.gameObject.GetComponentInParent<Character>();
        if (character != null)
        {
            character.MoveBy(exit.position - character.GetPosition());
        }
    }


    public void CollisionExit(Collision collisionInfo, int monitorID)
    {
        
    }
}
