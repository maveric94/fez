using UnityEngine;
using System.Collections;

public static class StuffUtility
{
	public static void DestroyAllChildren<T>(Transform parent) where T : UnityEngine.Component
	{
		T [] curChildrens = parent.GetComponentsInChildren<T>();

		int childCount = curChildrens.Length;

		if (Application.isPlaying)
		{     
			for (int count = 0; count < childCount; count++) 
			{
				GameObject.DestroyObject(curChildrens[count].gameObject);
			}
		}
		else
		{
			for (int count = 0; count < childCount; count++) 
			{
				GameObject.DestroyImmediate(curChildrens[count].gameObject);
			}
		}
	}


    public static void DestroyAllChildren(Transform parent)
    {
        if (Application.isPlaying)
        {           
            int childCount = parent.childCount;

            for (int count = 0; count < childCount; count++) 
            {
                GameObject.DestroyObject(parent.GetChild(count).gameObject);
            }
        }
        else
        {
            int childCount = parent.childCount;

            for (int count = 0; count < childCount; count++) 
            {
                GameObject.DestroyImmediate(parent.GetChild(0).gameObject);
            }
        }
    }


    public static Vector2 GetAverageContactPoint(ContactPoint2D[] contacts)
    {
        Vector2 collisionPoint = Vector2.zero;

        for (int count = 0; count < contacts.Length; count++)
        {
            collisionPoint += contacts[count].point;
        }

        return collisionPoint * (1.0f / contacts.Length);
    }


    public static Vector2 Rotate(Vector2 anchor, Vector2 point, float angle)
    {
        Vector2 result;
        result.x = (Mathf.Cos(angle * Mathf.Deg2Rad) * point.x) - (Mathf.Sin(angle * Mathf.Deg2Rad) * point.y);
        result.y = (Mathf.Sin(angle * Mathf.Deg2Rad) * point.x) + (Mathf.Cos(angle * Mathf.Deg2Rad) * point.y);

        return result + anchor;
    }


    public static Vector3 Rotate(Vector3 anchor, Vector3 point, float angle)
    {
        Vector3 result = new Vector3();
        result.x = (Mathf.Cos(angle * Mathf.Deg2Rad) * point.x) - (Mathf.Sin(angle * Mathf.Deg2Rad) * point.y);
        result.y = (Mathf.Sin(angle * Mathf.Deg2Rad) * point.x) + (Mathf.Cos(angle * Mathf.Deg2Rad) * point.y);

        return result + anchor;
    }


    public static Vector2 Clamp(Vector2 point, Vector2 min, Vector2 max)
    {
        return new Vector2(Mathf.Clamp(point.x, min.x, max.x), Mathf.Clamp(point.y, min.y, max.y));
    }


	public static float Cross(Vector2 v1, Vector2 v2)
	{
		return v1.x * v2.y - v1.y * v2.x;
	}


	public static float Dot(Vector2 v1, Vector2 v2)
	{
		return v1.x * v2.x + v1.y * v2.y;
	}        


	public static float Angle(Vector2 v1, Vector2 v2)
	{
		float angle = Mathf.Atan2(Cross(v1, v2), Dot(v1, v2));

		if (Mathf.Abs(angle) < float.Epsilon)
		{
			return 0.0f;
		}

		return Mathf.Rad2Deg * angle;
	}


	public static float AngleAbs(Vector2 v1, Vector2 v2)
	{
		float angle = Mathf.Acos(Dot(v1.normalized, v2.normalized));

		if (Mathf.Abs(angle) < float.Epsilon)
		{
			return 0.0f;
		}

		return Mathf.Rad2Deg * angle;
	}
}
