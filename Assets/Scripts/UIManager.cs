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
    private const float Tolerance = 0.01f;

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
        HidePaused();
        HidePartStats();
    }

    // Update is called once per frame
    private void Update()
    {
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

    private void  ShowPartStats(Part part)
    {
        var cost = _partStatsObjects.First(p => p.gameObject.name == "Cost");
        var costText = cost.GetComponent<TMP_Text>();
        costText.SetText($"Cost: {part.cost}");
        
        var durability = _partStatsObjects.First(p => p.gameObject.name == "Durability");
        var durabilityText = durability.GetComponent<TMP_Text>();
        durabilityText.SetText($"Durability: {part.PerceivedDurability}");
        
        var partName = _partStatsObjects.First(p => p.gameObject.name == "PartName");
        var partNameText = partName.GetComponent<TMP_Text>();
        partNameText.SetText(part.displayName);
        
        var weight = _partStatsObjects.First(p => p.gameObject.name == "Weight");
        var weightText = weight.GetComponent<TMP_Text>();
        weightText.SetText($"Weight: {part.weight}");
        

        var drag = _partStatsObjects.First(p => p.gameObject.name == "Drag");
        var dragText = drag.GetComponent<TMP_Text>();
        dragText.SetText($"Drag: {part.drag}");
        
        ShowGameObjects(_partStatsObjects);
        
    }

    //shows objects with ShowOnPause tag
    private void ShowGameObjects(GameObject[] gameObjects)
    {
        Debug.Log("show");
        Time.timeScale = 0;
        foreach (var g in gameObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    private void HideGameObjects(GameObject[] gameObjects)
    {
        Debug.Log("hide");
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