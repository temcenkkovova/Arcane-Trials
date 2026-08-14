public class PlayerHealth : Health
{
  private PlayerAnimationsController playerAnimationsController;

  void Awake()
  {
    playerAnimationsController = GetComponent<PlayerAnimationsController>();
  }

  void Start()
  {

  }
  protected override void Die()
  {
    base.Die();
    playerAnimationsController.PlayDeadAnimation();

  }
}