using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
  public PlayerConfig playerConfig;
  private PlayerMovement playerMovement;
  private PlayerStats playerStats;
  private PlayerRotation playerRotation;
  private PlayerFSMController fsmController;
  private LocomotionState locomotionState;
  public LocomotionState Locomotion => locomotionState;
  private PlayerInputController playerInputController;

  void Awake()
  {
    InitComponents();

  }
  void Start()
  {
    if (playerConfig == null) return;
    playerStats = new PlayerStats(playerConfig);
    locomotionState = new LocomotionState(playerMovement, playerInputController, playerRotation);
    fsmController.InitState(Locomotion);
    playerMovement.Init(playerStats);
    playerRotation.Init(playerStats);
  }


  void InitComponents()
  {

    playerMovement = GetComponent<PlayerMovement>();
    playerRotation = GetComponent<PlayerRotation>();
    fsmController = GetComponent<PlayerFSMController>();
    playerInputController = GetComponent<PlayerInputController>();
  }

}
