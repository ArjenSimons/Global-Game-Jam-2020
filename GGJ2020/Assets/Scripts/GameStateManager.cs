﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool GameOver { get; private set; }

    private readonly int gameOverSceneIndex = 1;
    [SerializeField] private Player winner;

    public readonly float cameraOffset = 20f;

    [SerializeField]
    private AudioManager audioManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        ListenToProgressManager();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)
            || Input.GetButtonDown("A-Button1") || Input.GetButtonDown("A-Button2"))
            {
                GameOver = false;
                SceneManager.LoadSceneAsync(0);
                SceneManager.sceneLoaded += OnGameSceneLoaded;
            }
        }
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
        ListenToProgressManager();
    }

    private void ListenToProgressManager()
    {
        ProgressManager progressManager = FindObjectOfType<ProgressManager>();
        if (progressManager != null)
        {
            progressManager.onPlayerFinish.AddListener(OnGameEnd);
        }
    }

    private void OnGameEnd(Player player)
    {
        GameOver = true;
        this.winner = player;
        SceneManager.LoadSceneAsync(gameOverSceneIndex);
        SceneManager.sceneLoaded += OnGameEndSceneLoaded;
        audioManager.Play("finishedgame");
    }

    private void OnGameEndSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnGameEndSceneLoaded;
        Text endText = FindObjectOfType<Text>();
        print(endText);
        endText.text = $"Player {(int)winner + 1} wins!";
    }
}
