using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class PathfindingMovement : MonoBehaviour
{
    private IAstarAI ai;
    [SerializeField] private Target target;
    public List<(Action, Target.TargetNames)> OnReachedDestinationList = new List<(Action, Target.TargetNames)>();

    private void Awake()
    {
        ai = GetComponent<IAstarAI>();
    }
    private void Update()
    {
        if (target == null) return;
        if ((Vector2.Distance(transform.position, target.position) < 0.1f))
        {
            Target _target = target;
            target = null;
            foreach ((Action action, Target.TargetNames name) in OnReachedDestinationList)
            {
                if(_target.targetName == name)
                {
                    action?.Invoke();
                    return;
                }
            }
        }
    }
    public void SetTarget(Target.TargetNames name, Action OnReachedDestination)
    {
        target = PathfindingTarget.FindTargetFromName(name);
        ai.destination = target.position;
        var tuple = (OnReachedDestination, name);
        if (OnReachedDestinationList.Contains(tuple)) return;
        OnReachedDestinationList.Add(tuple);
    }

    public void SetTarget(Vector2 position, Action OnReachedDestination)
    {
        target = PathfindingTarget.FindTargetFromPosition(position);
        ai.destination = target.position;
        var tuple = (OnReachedDestination, target.targetName);
        if (OnReachedDestinationList.Contains(tuple)) return;
        OnReachedDestinationList.Add(tuple);
    }

    public void SetTarget(Target target, Action OnReachedDestination)
    {
        this.target = target;
        ai.destination = target.position;
        var tuple = (OnReachedDestination, target.targetName);
        if (OnReachedDestinationList.Contains(tuple)) return;
        OnReachedDestinationList.Add(tuple);
    }

    public void RemoveTarget()
    {
        this.target = null;
        ai.canMove = false;
    }






}
