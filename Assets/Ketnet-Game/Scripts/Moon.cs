using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField]float _turnSpeed;
    [SerializeField]Vector3 _pivotAxis;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(_turnSpeed * Time.deltaTime, _pivotAxis.normalized);
    }
}
