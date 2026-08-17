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
  private DodgeState dodgeState;
  public DodgeState Dodge => dodgeState;
  private CombatState combatState;
  public CombatState Combat => combatState;
  private PlayerInputController playerInputController;
  private PlayerDodge playerDodge;
  private PlayerAnimationsController playerAnimationsController;
  private PlayerAttackManager attackManager;
  private PlayerHealth playerHealth;


  void Awake()
  {
    InitComponents();

  }
  void Start()
  {
    if (playerConfig == null) return;
    playerStats = new PlayerStats(playerConfig);
    locomotionState = new LocomotionState(playerMovement, playerInputController, playerRotation, fsmController, this, playerDodge);
    dodgeState = new DodgeState(playerMovement, playerDodge, playerAnimationsController, fsmController, this);
    combatState = new CombatState(playerAnimationsController, fsmController, this, playerInputController, attackManager);
    fsmController.InitState(Locomotion);
    playerMovement.Init(playerStats);
    playerRotation.Init(playerStats);
    playerHealth.Init(playerStats.MaxHealth);
  }


  void InitComponents()
  {

    playerMovement = GetComponent<PlayerMovement>();
    playerRotation = GetComponent<PlayerRotation>();
    fsmController = GetComponent<PlayerFSMController>();
    playerInputController = GetComponent<PlayerInputController>();
    playerDodge = GetComponent<PlayerDodge>();
    playerAnimationsController = GetComponent<PlayerAnimationsController>();
    attackManager = GetComponent<PlayerAttackManager>();
    playerHealth = GetComponent<PlayerHealth>();
  }

}
