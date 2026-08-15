using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public PlayerHealth playerHealth;


  void Awake()
  {
    if (playerHealth == null) return;
    playerHealth.OnDead += HandleGameOver;
  }

  private void HandleGameOver()
  {
    GameStateController.Instance.GameOver();
  }
  void OnDisable()
  {
    if (playerHealth == null) return;
    playerHealth.OnDead -= HandleGameOver;
  }

  public void RestartGame()
  {

    GameStateController.Instance.SetState(GameState.Gameplay);
    SceneManager.LoadScene("Start");


  }
}