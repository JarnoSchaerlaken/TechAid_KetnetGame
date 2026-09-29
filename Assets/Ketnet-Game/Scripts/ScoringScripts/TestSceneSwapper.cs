using UnityEngine;
using UnityEngine.InputSystem;

public class TestSceneSwapper : MonoBehaviour
{
    [SerializeField]
    private int _otherSceneIndex;

    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_otherSceneIndex);
        }
    }
}
