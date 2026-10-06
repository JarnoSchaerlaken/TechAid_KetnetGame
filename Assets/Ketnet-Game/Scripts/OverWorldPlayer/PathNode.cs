using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathNode : ProjectOnPlanet
{
    public List<PathNode> Conections;
    public UnityEvent OnPlayerLandOnNode;
    public UnityEvent OnPlayerLeaveNode;

    public string NextSceneName;
}
