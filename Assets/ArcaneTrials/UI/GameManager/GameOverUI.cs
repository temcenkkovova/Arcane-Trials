using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
  public GameObject gmPanel;

  void Awake()
  {
    gmPanel.SetActive(false);

  }
  void Start()
  {

  }

  private void ShowGmPanel(GameState gameState)
  {
    bool state = gameState == GameState.GameOver;
    gmPanel.SetActive(state);
  }

  void OnEnable()
  {
    GameStateController.Instance.OnGameStateChanged += ShowGmPanel;
  }
  void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= ShowGmPanel;
  }
}