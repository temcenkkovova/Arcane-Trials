using UnityEngine;
using UnityEngine.UI;

public class ArenaSealProgressUI : MonoBehaviour
{
  public ArenaSealProgress arenaSealProgress;
  public Image emptyRingImage;
  public Image fillRingImage;

  void OnEnable()
  {
    if (arenaSealProgress == null) return;
    arenaSealProgress.OnCurrentKillsChanged += ChangeProgress;
  }
  void OnDisable()
  {
    if (arenaSealProgress == null) return;
    arenaSealProgress.OnCurrentKillsChanged -= ChangeProgress;
  }

  private void ChangeProgress(float kills)
  {
    float percentage = kills / arenaSealProgress.requiredKills;
    fillRingImage.fillAmount = percentage;
  }

}