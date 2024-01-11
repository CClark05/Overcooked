using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Target
{
    public enum TargetNames
    {
        CustomerPickup,
        ExitStore,
        EatFood,
    }
    public TargetNames targetName { get; private set; }
    public Transform transform { get; private set; }
    public Target(TargetNames targetName, Transform transform)
    {
        this.targetName = targetName;
        this.transform = transform;
    }
}
public class PathfindingTarget : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Target.TargetNames targetName;
    Target target;
    public static List<Target> targets = new List<Target>();
    private void Awake()
    {
        target = new Target(targetName, targetPosition);
        if (targets.Contains(target)) return;
        targets.Add(target);
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


}
