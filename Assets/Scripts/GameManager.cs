using Core.Singleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<Player> playersInGame;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        playersInGame = FindObjectsOfType<Player>().ToList();
    }

    public void CheckWinState()
    {
        foreach (var player in playersInGame)
        {
            if (player.IsDead)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }

}
