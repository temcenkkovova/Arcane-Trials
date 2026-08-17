
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Arena/Boss")]
public class ArenaBossConfig : ScriptableObject
{
  public ArenaBoss bossPrefab;
  public Sprite bossIcon;
  public AudioClip bossAudio;
  public AudioClip segmentAudio;
}