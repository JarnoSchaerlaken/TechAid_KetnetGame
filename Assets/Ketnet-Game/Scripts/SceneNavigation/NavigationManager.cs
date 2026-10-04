using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;

    private static PathNode _currentNode;

    private static List<bool> _dataPoints = new List<bool>() { false, false };

    public static string ActivePlanetName = "";

    [SerializeField]
    private  GameObject _enterButtton;

    [SerializeField]
    private GameObject _nextPlanetButton;

    [SerializeField]
    private GameObject _returnToLastPlanetButton;

    [SerializeField]
    private Canvas _ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_ui);

        ActivePlanetName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentNode == null)
        {
            _enterButtton.SetActive(false);
            _nextPlanetButton.SetActive(false);
            _returnToLastPlanetButton.SetActive(false);
            return;
        }
        if (_currentNode.tag == "MinigameNode")
        {
            _enterButtton.SetActive(true);
            _nextPlanetButton.SetActive(false);
            _returnToLastPlanetButton.SetActive(false);
        }
        else if (_currentNode.tag == "EndNode")
        {
            _nextPlanetButton.SetActive(true);
            _enterButtton.SetActive(false);
            _returnToLastPlanetButton.SetActive(false);
        }
        else if (_currentNode.tag == "StartNode" && _currentNode.NextSceneName != "")
        {
            _returnToLastPlanetButton.SetActive(true);
            _enterButtton.SetActive(false);
            _nextPlanetButton.SetActive(false);
        }
        else
        {
            _enterButtton.SetActive(false);
            _nextPlanetButton.SetActive(false);
            _returnToLastPlanetButton.SetActive(false);
        }
    }

    public static void SetCurrentNode(PathNode node)
    {
        _currentNode = node;
    }

    public static void UpdateDataPoints(bool dataPoint, int minigameIndex)
    {
        _dataPoints[minigameIndex] = dataPoint; // can be expanded upon if there needs to be more than 1 data point per minigame.
    }

    public static int GetActiveDataPoint()
    {
        return _dataPoints.Where(x => x == true).Count();
    }

    public static void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_currentNode.NextSceneName);
        _currentNode = null;
    }

    public static void LoadNextPlanet()
    {
        if (GetActiveDataPoint() >= _currentNode.GetComponentInParent<PlanetRequirementChecker>().RequiredDataPoints)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_currentNode.NextSceneName);
            ActivePlanetName = _currentNode.NextSceneName;
            _currentNode = null;
        }
    }

    public static void ReturnToLastPlanet()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_currentNode.NextSceneName);
        ActivePlanetName = _currentNode.NextSceneName;
        _currentNode = null;
    }
}
