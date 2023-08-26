using System;
using System.Globalization;
using System.Linq;
using Parts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SubmarineMovement;
using static Titanic;
using static UpgradeButton;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    private GameObject[] _pauseObjects;
    private GameObject[] _wonObjects;
    private GameObject[] _partStatsObjects;
    private GameObject[] _currentGameInfoObjects;
    private GameObject[] _subStatsObjects;
    private GameObject _titanic;
    private const float Tolerance = 0.01f;
    public GameObject eel;

    public float spawnRate = 10f;
    private float spawnTimer = 0f;

    private bool _atBottom = false;

    public float timePassed = 0f;
    private GameObject _spawnBox;
    private double Depth => Math.Floor(timePassed * 25);

    private void OnEnable()
    {
        TitanicArrived += AtBottom;
        YouAreDead += ShowPaused;
        YouWin += ShowWon;
        PartsStatsEnter += ShowPartStats;
        PartsStatsExit += HidePartStats;
    }

    private void OnDisable()
    {
        TitanicArrived -= AtBottom;
        YouAreDead -= ShowPaused;
        YouWin -= ShowWon;
        PartsStatsEnter -= ShowPartStats;
        PartsStatsExit -= HidePartStats;
    }

    private void AtBottom(object sender, EventArgs args)
    {
        _atBottom = true;
    }

    // Use this for initialization
    private void Start()
    {
        Time.timeScale = 1;
        _pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        _wonObjects = GameObject.FindGameObjectsWithTag("ShowOnWin");
        _partStatsObjects = GameObject.FindGameObjectsWithTag("PartStats");
        _currentGameInfoObjects = GameObject.FindGameObjectsWithTag("CurrentGameInfo");
        _subStatsObjects = GameObject.FindGameObjectsWithTag("SubStats");
        _titanic = GameObject.Find("titanic");
        _spawnBox = GameObject.Find("BottomBoundary");
        _titanic.SetActive(false);
        HidePaused();
        HideWon();
        HidePartStats();

        spawnTimer = spawnRate;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_atBottom)
        {
            timePassed += Time.deltaTime;
            spawnTimer += Time.deltaTime;
        }

        if (Time.timeScale != 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainLevel"))
        {
            Submarine.Instance.TakeDepthDamage(Depth);
            SetUiText(_currentGameInfoObjects, "CurrentDepth", Depth + "m");
            SetUiText(_currentGameInfoObjects, "HullIntegrity",
                ((float)Submarine.Instance.health / (float)Submarine.Instance.startingHealth).ToString("P0"));

            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0f;
                Vector3 spawnPoint = new Vector3(Random.Range(-4f, 4f), -5, -5);
                var eelSpawn = Instantiate(eel, spawnPoint, Quaternion.identity);
                var eelSize = Random.Range(0.1f, 0.7f);
                eelSpawn.transform.localScale = new Vector3(eelSize, eelSize, 1);
            }

            if (Depth >= 3500 && !_titanic.activeSelf)
            {
                _titanic.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Loadout"))
        {
            SetUiText(_subStatsObjects, "Ego", PlayerAttributes.Instance.ego.ToString());
            SetUiText(_subStatsObjects, "Cash",
                PlayerAttributes.Instance.cashMoney.ToString("C", CultureInfo.GetCultureInfo("en-GB")));
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
    private void ShowWon() => ShowGameObjects(_wonObjects);

    private void HidePaused() => HideGameObjects(_pauseObjects);
    private void HideWon() => HideGameObjects(_wonObjects);
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
        costText.SetText($"Cost: {part.cost.ToString("C", CultureInfo.GetCultureInfo("en-GB"))}");

        var durability = _partStatsObjects.First(p => p.gameObject.name == "Durability");
        var durabilityText = durability.GetComponent<TMP_Text>();
        durabilityText.SetText($"Durability: {part.PerceivedStatInflated(part.durability):N0}");

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
        Submarine.Instance.health = Submarine.Instance.GetDurability + 200;
        Submarine.Instance.startingHealth = Submarine.Instance.health;
        SceneManager.LoadScene(level);
    }

    public void ClickTest()
    {
        Debug.Log("Clicked");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Win"))
            _atBottom = true;
    }
}