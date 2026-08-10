using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
  public PlayerConfig playerConfig;
  private PlayerMovement playerMovement;
  private PlayerStats playerStats;
  private PlayerRotation playerRotation;

  void Awake()
  {
    InitComponents();

  }
  void Start()
  {
    if (playerConfig == null) return;
    playerStats = new PlayerStats(playerConfig);

    if (playerMovement == null) return;
    playerMovement.Init(playerStats);
    playerRotation.Init(playerStats);
  }


  void InitComponents()
  {

    playerMovement = GetComponent<PlayerMovement>();
    playerRotation = GetComponent<PlayerRotation>();
  }

}
