using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;

    private PathNode _currentNode;

    private static List<bool> _dataPoints = new List<bool>() { false, false };

    public static string ActivePlanetName = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);

        ActivePlanetName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void SetCurrentNode(PathNode node)
    {
        _currentNode = node;
    }

    public static void UpdateDataPoints(bool dataPoint, int minigameIndex)
    {
        _dataPoints[minigameIndex] = dataPoint; // can be expanded upon if there needs to be more than 1 data point per minigame.
    }
}
