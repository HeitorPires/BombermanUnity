using Core.Singleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<Player> playersInGame;

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
                Invoke(nameof(EndGame), 3f);
            }
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }

}
