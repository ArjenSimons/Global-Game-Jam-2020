using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool GameOver { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnGameEnd()
    {
        GameOver = true;
    }
}
