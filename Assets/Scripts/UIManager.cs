using System;
using System.Linq;
using Parts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SubmarineMovement;
using static UpgradeButton;

public class UIManager : MonoBehaviour
{
    private GameObject[] _pauseObjects;
    private GameObject[] _partStatsObjects;
    private GameObject[] _currentGameInfoObjects;
    private const float Tolerance = 0.01f;
    public float timePassed = 0f;
    private double Depth =>  Math.Floor(timePassed * 10);

    private void OnEnable()
    {
        YouAreDead += ShowPaused;
        PartsStatsEnter += ShowPartStats;
        PartsStatsExit += HidePartStats;
    }

    private void OnDisable()
    {
        YouAreDead -= ShowPaused;
        PartsStatsEnter -= ShowPartStats;
        PartsStatsExit -= HidePartStats;
    }


    // Use this for initialization
    private void Start()
    {
        Time.timeScale = 1;
        _pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        _partStatsObjects = GameObject.FindGameObjectsWithTag("PartStats");
        _currentGameInfoObjects = GameObject.FindGameObjectsWithTag("CurrentGameInfo");
        HidePaused();
        HidePartStats();
    }

    // Update is called once per frame
    private void Update()
    {
        timePassed += Time.deltaTime;

        if (Time.timeScale != 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainLevel"))
        {
            Submarine.Instance.TakeDepthDamage(Depth);
            SetUiText("CurrentDepth", Depth + "m");
            SetUiText("HullIntegrity", Math.Max(0 ,Submarine.Instance.health).ToString());
        }

        var ctrl = Input.GetKey(KeyCode.LeftControl)
                   || Input.GetKey(KeyCode.RightControl);
        if (Input.GetKeyDown(KeyCode.Q) && ctrl)
            Quit();

        //uses the p button to pause and unpause the game
        if (!Input.GetKeyDown(KeyCode.P)) return;
        if (Math.Abs(Time.timeScale - 1) < Tolerance)
        {
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Debug.Log("high");
            Time.timeScale = 1;
            HidePaused();
        }
    }

    private void SetUiText(string objectName, string value)
    {
        var cost = _currentGameInfoObjects.First(p => p.gameObject.name == objectName);
        var costText = cost.GetComponent<TMP_Text>();
        costText.SetText(value);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //Reloads the Level
    public void Reload()
    {
        SceneManager.LoadScene("MainLevel");
    }

    //controls the pausing of the scene
    public void PauseControl()
    {
        switch (Time.timeScale)
        {
            case 1:
                Time.timeScale = 0;
                ShowPaused();
                break;
            case 0:
                Time.timeScale = 1;
                HidePaused();
                break;
        }
    }

    private void ShowPaused() => ShowGameObjects(_pauseObjects);

    private void HidePaused() => HideGameObjects(_pauseObjects);
    private void HidePartStats() => HideGameObjects(_partStatsObjects);

    private void HidePartStats(Part part)
    {
        HideGameObjects(_partStatsObjects);
    }

    private void ShowPartStats(Part part)
    {
        var partName = _partStatsObjects.First(p => p.gameObject.name == "PartName");
        var partNameText = partName.GetComponent<TMP_Text>();
        partNameText.SetText(part.displayName);

        var cost = _partStatsObjects.First(p => p.gameObject.name == "Cost");
        var costText = cost.GetComponent<TMP_Text>();
        costText.SetText($"Cost: {part.cost}");

        var durability = _partStatsObjects.First(p => p.gameObject.name == "Durability");
        var durabilityText = durability.GetComponent<TMP_Text>();
        durabilityText.SetText($"Durability: {part.PerceivedStatInflated(part.durability)}");

        var weight = _partStatsObjects.First(p => p.gameObject.name == "Weight");
        var weightText = weight.GetComponent<TMP_Text>();
        weightText.SetText($"Weight: {part.PerceivedStatDeflated(part.weight)}");


        var drag = _partStatsObjects.First(p => p.gameObject.name == "Drag");
        var dragText = drag.GetComponent<TMP_Text>();
        dragText.SetText($"Drag: {part.PerceivedStatDeflated(part.drag)}");

        ShowGameObjects(_partStatsObjects);
    }

    //shows objects with ShowOnPause tag
    private void ShowGameObjects(GameObject[] gameObjects)
    {
        Time.timeScale = 0;
        foreach (var g in gameObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    private void HideGameObjects(GameObject[] gameObjects)
    {
        foreach (var g in gameObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        Debug.Log(level);
        SceneManager.LoadScene(level);
    }

    public void ClickTest()
    {
        Debug.Log("Clicked");
    }
}