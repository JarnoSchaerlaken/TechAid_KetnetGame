using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeNode : InteractableNode
{
    [SerializeField]float _requiredDataPoints;
    [SerializeField]string _nextSceneName;
    [SerializeField]Dialogue _insuficientDataDialogue;

    public override void OnButtonPressed()
    {
        if (ScoreManager.DataPoints >= _requiredDataPoints)
        {
            SceneManager.LoadScene(_nextSceneName);
        }
        else
        {
            _insuficientDataDialogue.StartDialogue();
        }
        base.OnButtonPressed();
    }
}
