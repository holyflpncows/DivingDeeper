using System;
using System.Linq;
using Parts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SubmarineMovement;
using static UpgradeButton;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    private GameObject[] _pauseObjects;
    private GameObject[] _partStatsObjects;
    private GameObject[] _currentGameInfoObjects;
    private GameObject[] _subStatsObjects;
    private const float Tolerance = 0.01f;
    public GameObject eel;

    public float spawnRate = 20f;
    private float spawnTimer = 0f;

    public float timePassed = 0f;
    private GameObject _spawnBox;
    private double Depth => Math.Floor(timePassed * 10);

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
        _subStatsObjects = GameObject.FindGameObjectsWithTag("SubStats");
        _spawnBox = GameObject.Find("BottomBoundary");
        HidePaused();
        HidePartStats();
    }

    // Update is called once per frame
    private void Update()
    {
        timePassed += Time.deltaTime;
        spawnTimer += Time.deltaTime;


        if (Time.timeScale != 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainLevel"))
        {
            Submarine.Instance.TakeDepthDamage(Depth);
            SetUiText(_currentGameInfoObjects, "CurrentDepth", Depth + "m");
            SetUiText(_currentGameInfoObjects, "HullIntegrity", Math.Max(0, Submarine.Instance.health).ToString());

            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0f;
                Vector2 spawnPoint = new Vector2(Random.Range(-4f, 4f), -5);
                var eelSpawn = Instantiate(eel, spawnPoint, Quaternion.identity);
                var eelSize = Random.Range(0.1f, 0.7f);
                eelSpawn.transform.localScale = new Vector3(eelSize, eelSize, 1);
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Loadout"))
        {
            SetUiText(_subStatsObjects, "Ego", PlayerAttributes.Instance.ego.ToString());
            SetUiText(_subStatsObjects, "Cash", PlayerAttributes.Instance.cashMoney.ToString());
            SetUiText(_subStatsObjects, "Durability", Submarine.Instance.GetDisplayDurability.ToString());
            SetUiText(_subStatsObjects, "Drag", Submarine.Instance.GetDisplayDrag.ToString());
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

    private void SetUiText(GameObject[] gameObjects, string objectName, string value)
    {
        var cost = gameObjects.First(p => p.gameObject.name == objectName);
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