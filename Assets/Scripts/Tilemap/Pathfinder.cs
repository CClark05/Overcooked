using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;

public class Waypoint
{
    private Vector2 directionVector;
    private Transform parent;
    private bool isStartingWaypoint;
    private bool isEndingWaypoint;
    private Vector2 position;

    public Waypoint(Vector2 directionVector, Transform parent, Vector2 position, bool isStartingWaypoint, bool isEndingWaypoint)
    {
        this.directionVector = directionVector;
        this.parent = parent;
        this.position = position;
        this.isStartingWaypoint = isStartingWaypoint;
        this.isEndingWaypoint = isEndingWaypoint;
    }
    public void CreateWaypoint()
    {
        GameObject newWaypoint = new GameObject("waypoint");
        newWaypoint.transform.parent = parent;
        newWaypoint.transform.position = position;
    }
    public bool IsStartingWaypoint()
    {
        return isStartingWaypoint;
    }
    public bool IsEndingWaypoint()
    {
        return isEndingWaypoint;
    }
    public Vector2 GetPosition()
    {
        return position;
    }
    public Vector2 GetDirectionVector()
    {
        return directionVector;
    }
}

public class Pathfinder 
{
    private List<Waypoint> waypoints;
    private List<Waypoint> startingWaypoints = new List<Waypoint>();
    private List<List<Waypoint>> waypointPaths = new List<List<Waypoint>>();
    public Pathfinder(List<Waypoint> waypoints)
    {
        this.waypoints = waypoints;

        foreach (Waypoint waypoint in waypoints)
        {
            if (waypoint.IsStartingWaypoint())
            {
                startingWaypoints.Add(waypoint);
            }
        }

        foreach (Waypoint startingWaypoint in startingWaypoints)
        {
            waypointPaths.Add(GetWaypointPath(startingWaypoint));
        }

    }
    public List<Waypoint> GetPathFromPosition(Vector2 position)
    {
        List<Waypoint> entirePath = new List<Waypoint>();
        List<Waypoint> actualPath = new List<Waypoint>();
        int index = 0;
        foreach (List<Waypoint> path in waypointPaths)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (Vector2.Distance(path[i].GetPosition(), position) < 0.01f)
                {
                    entirePath = path;
                    index = i;
                    break;
                }
            }
        }
        if (entirePath.Count == 0) return null;

        for(int i = index; i<entirePath.Count; i++)
        {
            actualPath.Add(entirePath[i]);
        }

        return actualPath;
    }


    public List<Waypoint> GetWaypointPath(Waypoint startingWaypoint)
    {
        List<Waypoint> waypointPath = new List<Waypoint>();
        Waypoint currentWaypoint = startingWaypoint;
        waypointPath.Add(currentWaypoint);
        Vector2 direction = startingWaypoint.GetDirectionVector();

        int safetyCounter = 0; 
        const int maxIterations = 1000; 

        while (safetyCounter < maxIterations)
        {
            if (currentWaypoint.IsEndingWaypoint())
            {
                return waypointPath;
            }
            foreach (Waypoint waypoint in waypoints)
            {
                if (CheckNextWaypoint(waypoint, currentWaypoint, direction) || CheckIfNextWaypointIsEnd(waypoint, currentWaypoint, direction))
                {
                    waypointPath.Add(waypoint);
                    direction = waypoint.GetDirectionVector();
                    currentWaypoint = waypoint;
                    break; 
                }
            }
            safetyCounter++;
        }
        Debug.LogWarning("Pathfinder: Max iterations reached, possibly no end waypoint found.");
        return waypointPath;
    }

    private bool CheckNextWaypoint(Waypoint waypoint, Waypoint startingWaypoint, Vector2 Direction)
    {
        if (Vector2.Distance(waypoint.GetPosition(), startingWaypoint.GetPosition() + Direction) < 0.01f) 
        {
            return true;
        }
        return false;
        
    }
    private bool CheckIfNextWaypointIsEnd(Waypoint waypoint, Waypoint startingWaypoint, Vector2 Direction)
    {
        if (Vector2.Distance(waypoint.GetPosition(), startingWaypoint.GetPosition() + Direction / 2) < 0.01f)
        {
            return true;
        }
        return false;

    }



}
