using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{

    public Vector2Int position;

	public static bool CheckPosition(Vector2Int position)
    {
        Collider[] colliders = GameObject.FindObjectsOfType<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].position == position)
            {
                return false;
            }
        }
        return true;
	}
}
