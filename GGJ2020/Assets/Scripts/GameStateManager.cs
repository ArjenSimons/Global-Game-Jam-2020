using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool GameOver { get; private set; }

    private void Awake()
    {
        Instance = this;
        ProgressManager progressManager = FindObjectOfType<ProgressManager>();
        if(progressManager != null)
        {
            progressManager.onPlayerFinish.AddListener(OnGameEnd);
        }
    }

    private void OnGameEnd(Player player)
    {
        GameOver = true;
    }
}
