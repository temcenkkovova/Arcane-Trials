using UnityEngine;

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
}