using UnityEngine;

public class ProjectOnPlanet : MonoBehaviour
{
    public enum PlanetShape {Sphere, Cylinder}
    [SerializeField]PlanetShape _shape;
    [SerializeField]Transform _planetTransform;

    void Awake()
    {
        Project();
    }

    void Project ()
    {
        switch (_shape)
        {
            case PlanetShape.Sphere:
                float radius = _planetTransform.lossyScale.x / 2;
                Vector3 dir = (transform.position - _planetTransform.position).normalized;

                SetPosAndRotation(radius * dir + _planetTransform.position, dir);
                break;
            case PlanetShape.Cylinder:
                radius = _planetTransform.lossyScale.x / 2;

                float height = Vector3.Dot(transform.position - _planetTransform.position, _planetTransform.up);

                dir = (transform.position - _planetTransform.position).normalized;
                dir -= Vector3.Dot(dir, _planetTransform.up) * _planetTransform.up;
                dir.Normalize();

                SetPosAndRotation(radius * dir + (height * _planetTransform.up) + _planetTransform.position, dir);
                break;
        }
    }

    void SetPosAndRotation (Vector3 pos, Vector3 upDir)
    {
        transform.position = pos;
        transform.up = upDir;
    }
}
