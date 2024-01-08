using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class ConveyorBeltManager : MonoBehaviour
{
    public static ConveyorBeltManager Instance;

    public Action OnPathfinderDone;
    public Action<Vector2, Vector2> OnIsFinalBelt;

    private List<ConveyorBeltInteract> conveyorBelts = new List<ConveyorBeltInteract>();
    private List<Waypoint> waypoints = new List<Waypoint>();
    private List<ConveyorBeltInteract> waypointBelts = new List<ConveyorBeltInteract>();

    private Pathfinder pathfinder;
    
    
    private void Awake()
    {
        Instance = this;
        OnIsFinalBelt += (Vector2 position, Vector2 directionVector) =>
        {
            Waypoint newWaypoint = new Waypoint(directionVector, this.transform, position, false, true);
            newWaypoint.CreateWaypoint();
            waypoints.Add(newWaypoint);
        };

    }

    private void Start()
    {
        
        foreach (Tile tile in TileManager.Instance.GetTiles())
        {
            if (tile.GetCounter() is ConveyorBeltInteract)
            {
                conveyorBelts.Add(tile.GetCounter() as ConveyorBeltInteract);
            }

        }
        foreach (ConveyorBeltInteract belt in conveyorBelts)
        {
            Waypoint newWaypoint = new Waypoint(belt.GetDirectionVector(), this.transform, belt.transform.position, belt.IsStartingPoint(), false);
            newWaypoint.CreateWaypoint();
            waypoints.Add(newWaypoint);

        }
        pathfinder = new Pathfinder(waypoints);
        OnPathfinderDone?.Invoke();

    }

    public List<Waypoint> GetPathFromPosition(Vector2 position)
    {
        return pathfinder.GetPathFromPosition(position);
    }

}
