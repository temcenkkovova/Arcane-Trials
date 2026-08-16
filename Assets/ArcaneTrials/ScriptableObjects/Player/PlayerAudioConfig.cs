using UnityEngine;
[CreateAssetMenu(menuName = "Audio/Player")]

public class PlayerAudioConfig : ScriptableObject
{
  public AudioClip[] hitClips;
  public AudioClip[] deathClips;
  public AudioClip[] dashClips;
  public float volume = 1f;
  public float pitchMin = 0.95f;
  public float pitchMax = 1.05f;
}