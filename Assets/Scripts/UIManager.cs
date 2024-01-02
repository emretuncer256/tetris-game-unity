using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject pausePanel;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePausePanel();
    }

    public void TogglePausePanel()
    {
        if (_gameManager.IsGameOver) return;
        if (!pausePanel) return;

        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);
        SoundManager.instance.PlayFX(0);
        Time.timeScale = (isPaused) ? 0 : 1;
    }

    public void ReplayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}