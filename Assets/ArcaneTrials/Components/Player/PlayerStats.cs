public class PlayerStats
{
  public float MaxHealth { get; private set; }
  public float MoveSpeed { get; private set; }
  public float RotationSpeed { get; private set; }
  public float Damage { get; private set; }
  public float DashCooldown { get; private set; }
  public float BaseSprintSpeed { get; private set; }

  public PlayerStats(PlayerConfig config)
  {
    MaxHealth = config.baseHealth;
    MoveSpeed = config.baseSpeed;
    RotationSpeed = config.baseSpeedRotation;
    Damage = config.baseDamage;
    DashCooldown = config.dashCooldawn;
    BaseSprintSpeed = config.baseSprintSpeed;
  }
}