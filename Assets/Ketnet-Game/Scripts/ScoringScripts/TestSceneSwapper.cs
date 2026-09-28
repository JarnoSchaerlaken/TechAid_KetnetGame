using UnityEngine;
using UnityEngine.InputSystem;

public class TestSceneSwapper : MonoBehaviour
{
    [SerializeField]
    private int _otherSceneIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_otherSceneIndex);
        }
    }
}
