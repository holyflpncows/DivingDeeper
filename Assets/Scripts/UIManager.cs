using System;
using UnityEngine;
using static SubmarineMovement;

public class UIManager : MonoBehaviour
{
    private GameObject[] _pauseObjects;
    private const float Tolerance = 0.01f;
    private void OnEnable() => OnHit += ShowPaused;

    private void OnDisable() => OnHit -= ShowPaused;

    // Use this for initialization
    private void Start()
    {
        Time.timeScale = 1;
        _pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePaused();
    }

    // Update is called once per frame
    private void Update()
    {
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


    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
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

    //shows objects with ShowOnPause tag
    private void ShowPaused()
    {
        Time.timeScale = 0;
        foreach (var g in _pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    private void HidePaused()
    {
        foreach (var g in _pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}