public class WeaponStats
{
  public float Level { get; private set; }
  public float Damage { get; private set; }
  public float Speed { get; private set; }
  public float CritChance { get; private set; }
  public float CritMultiplier { get; private set; }
  public WeaponStats(WeaponConfig config)
  {
    Level = config.level;
    Damage = config.damage;
    Speed = config.speed;
    CritChance = config.critChance;
    CritMultiplier = config.critMultiplier;
  }

  public void LevelUp()
  {
    Level++;
    Damage *= 1.1f;
    Speed *= 0.95f;
  }
}