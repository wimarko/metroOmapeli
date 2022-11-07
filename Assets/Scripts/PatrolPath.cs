using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    //[SerializeField] int chosenColor;
    [SerializeField] Color chosenColor = new Color(1,1,0,1);
    const float waypointGizmoRadius = 0.3f;



    private void OnDrawGizmos()
    {
        Gizmos.color = chosenColor;

        for (int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextIndex(i);
            Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
        }
  
    }

    private int GetNextIndex(int i)
    {
        if ( i +1 >= transform.childCount) //Looppaa ympäri
        {
            return 0;
        }

        return i + 1;
    }

    private Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
