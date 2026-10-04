using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPointGiver : MonoBehaviour
{
    public int MinigameIndex = 0; // The index of the minigame associated with this data point

    public bool GiveDataPoint = false; // for debugging, let's you manually trigger the method from the inspector.
    // Update is called once per frame
    void Update()
    {
        if (GiveDataPoint)
        {
            Debug.Log($"Giving data point for minigame index {MinigameIndex}");
            GiveDataPointToPlayer(true);
            SceneManager.LoadScene(NavigationManager.ActivePlanetName);
        }
    }

    public void GiveDataPointToPlayer(bool awardDatapoint)
    {
        NavigationManager.UpdateDataPoints(awardDatapoint, MinigameIndex);
        GiveDataPoint = false; // Reset the flag after giving the data point, only applicable if done manually from the inspector.
    }
}
