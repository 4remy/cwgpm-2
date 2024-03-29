﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject mainCanvas;
    public string mainMenu;
    public bool usingPausePanel;
    public string soundEffectToPlay;

    private bool busyUI;

    private void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        usingPausePanel = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !busyUI)
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            AudioManager.Instance.Play(soundEffectToPlay);
            pausePanel.SetActive(true);
            mainCanvas.SetActive(false);
            Time.timeScale = 0f;
            usingPausePanel = true;
        }
        else
        {
            AudioManager.Instance.Play(soundEffectToPlay);
            inventoryPanel.SetActive(false);
            pausePanel.SetActive(false);
            mainCanvas.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    // Called by UI button
    public void Save() => GameSaveManager.Instance?.SaveScriptables();
    // Called by UI button
    public void ResetSave() => GameSaveManager.Instance?.ResetScriptables();

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            mainCanvas.SetActive(false);
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            mainCanvas.SetActive(true);
            pausePanel.SetActive(false);
        }
    }

    public void UIBusyRecieved()
    {
        busyUI = true;
        Debug.Log("UI busy signal recieved");
        Debug.Log("busy UI" + busyUI);
    }

    public void UIFreeRecieved()
    {
        busyUI = false;
        Debug.Log("UI free signal recieved");
        Debug.Log("busy UI" + busyUI);
    }
}
