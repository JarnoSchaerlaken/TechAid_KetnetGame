using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    [SerializeField]Transform _planet;
    public List<PathNode> Conections;

    public string NextSceneName;

    void Awake()
    {
        float height = _planet.lossyScale.x / 2;
        Vector3 dir = (transform.position - _planet.position).normalized;

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(dir, Vector3.back), dir);
        transform.position = height * dir;
    }

    void Update()
    {
        
    }
}
