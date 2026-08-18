
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Arena/Boss")]
public class ArenaBossConfig : ScriptableObject
{
  public BossBootstrap bossPrefab;
  public Sprite bossIcon;
  public AudioClip bossAudio;
  public AudioClip segmentAudio;
  public Vector3 spawnPos;
  public Quaternion spawnBossRotation;
}