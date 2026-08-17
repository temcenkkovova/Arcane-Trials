using UnityEngine;

public class ArenaAudio : MonoBehaviour
{
  private ArenaSealProgress arenaSealProgress;
  private AudioClip segmentAudio;
  private AudioClip bossAudio;
  public float volume = 1f;
  public float pitchMin = 1f;
  public float pitchMax = 1.5f;

  public void Init(ArenaBossConfig bossConfig)
  {
    segmentAudio = bossConfig.segmentAudio;
    bossAudio = bossConfig.bossAudio;
  }

  void Awake()
  {
    arenaSealProgress = GetComponent<ArenaSealProgress>();
  }

  void OnEnable()
  {
    if (arenaSealProgress == null) return;
    arenaSealProgress.OnCurrentKillsChanged += PlaySegmentAudio;
    arenaSealProgress.OnCompletedProgress += PlayBossAudio;
  }

  void OnDisable()
  {
    if (arenaSealProgress == null) return;
    arenaSealProgress.OnCurrentKillsChanged -= PlaySegmentAudio;
    arenaSealProgress.OnCompletedProgress -= PlayBossAudio;
  }
  private void PlayBossAudio()
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(), bossAudio, volume, pitch);
  }

  private void PlaySegmentAudio(float value)
  {
    float pitch = Random.Range(pitchMin, pitchMax);
    AudioService.Instance.PlayAt(new Vector3(), segmentAudio, volume, pitch);
  }
}