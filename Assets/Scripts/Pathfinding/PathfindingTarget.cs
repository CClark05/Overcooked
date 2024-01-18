using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Pathfinding;

public class Target
{
    public enum TargetNames
    {
        CustomerPickup,
        ExitStore,
        EatFood,
        RandomTarget
    }
    public TargetNames targetName { get; private set; }
    public Vector3 position { get; private set; }
    public Target(TargetNames targetName, Vector3 position)
    {
        this.targetName = targetName;
        this.position = position;
    }
}
public class PathfindingTarget : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Target.TargetNames targetName;
    Target target;
    public static List<Target> targets = new List<Target>();
    private void Awake()
    {
        AddTarget(targetTransform.position, targetName);
    }

    private void AddTarget(Vector3 position, Target.TargetNames name)
    {
        Target newTarget = new Target(name, position);
        if (targets.Contains(target)) return;
        targets.Add(newTarget);
    }
    private static void AddTargetStatic(Target target)
    {
        if (targets.Contains(target)) return;
        targets.Add(target);
    }
    public static Target GetRandomTarget(float range, Vector2 origin)
    {
        GraphNode randomNode;
        do
        {
            var grid = AstarPath.active.data.gridGraph;
            randomNode = grid.nodes[UnityEngine.Random.Range(0, grid.nodes.Length)];
        } while (!randomNode.Walkable || Vector2.Distance((Vector3)randomNode.position, origin) > range);


        Vector3 position = (Vector3)randomNode.position;
        Target newTarget = new Target(Target.TargetNames.RandomTarget, position);
        AddTargetStatic(newTarget);
        return newTarget;
    }
    public static Target FindTargetFromName(Target.TargetNames name)
    {
        foreach(Target target in targets)
        {
            if(name == target.targetName)
            {
                return target;
            }
        }
        Debug.LogError("Target name not valid");
        return null;
    }
    public static Target FindTargetFromPosition(Vector2 position)
    {
        foreach (Target target in targets)
        {
            if(Vector2.Distance(position, target.position) < 0.1f)
            {
                return target;
            }
        }
        Debug.LogError("Target psoition not valid");
        return null;
    }


}
