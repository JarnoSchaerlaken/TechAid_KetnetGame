using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform _cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _cam.rotation;
    }
}
