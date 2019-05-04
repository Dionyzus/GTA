﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour {

    Waypoint[] waypoints;

    int currentWayPointIndex = -1;
    public event System.Action<Waypoint> OnWaypointChanged;

    void Awake()
    {
        waypoints = GetWaypoints();
    }
    public void SetNextWaypoint()
    {
        currentWayPointIndex++;

        if (currentWayPointIndex == waypoints.Length)
        {
            currentWayPointIndex = 0;
        }
        if (OnWaypointChanged != null)
        {
            OnWaypointChanged(waypoints[currentWayPointIndex]);
        }
    }
    Waypoint[] GetWaypoints()
    {
        return GetComponentsInChildren<Waypoint>();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 previousWaypoint = Vector3.zero;

        foreach (var waypoint in GetWaypoints())
        {
            Vector3 waypointPosition = waypoint.transform.position;
            Gizmos.DrawWireSphere(waypointPosition, .2f);
            if (previousWaypoint != Vector3.zero)
            {
                Gizmos.DrawLine(previousWaypoint, waypointPosition);
            }
            previousWaypoint = waypointPosition;
        }
    }
}
