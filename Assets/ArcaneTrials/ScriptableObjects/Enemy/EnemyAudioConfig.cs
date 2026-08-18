using UnityEngine;
[CreateAssetMenu(menuName = "Audio/Enemy")]

public class EnemyAudioConfig : ScriptableObject
{
  public AudioClip[] hitClips;
  public AudioClip[] deathClips;

  public float volume = 1f;
  public float pitchMin = 0.95f;
  public float pitchMax = 1.05f;
}