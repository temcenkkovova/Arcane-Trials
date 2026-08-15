public class PlayerHealth : Health
{
  private PlayerAnimationsController playerAnimationsController;



  void Start()
  {
    playerAnimationsController = GetComponent<PlayerAnimationsController>();
  }
  protected override void Die()
  {
    base.Die();
    playerAnimationsController.PlayDeadAnimation();

  }
}