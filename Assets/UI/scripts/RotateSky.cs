using UnityEngine;

public class RotateSky : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] private float _rotateSpeed = 1.2f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotateSpeed);
    }
}
