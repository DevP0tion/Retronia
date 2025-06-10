using UnityEngine;

namespace Retronia.Core
{
  [ AddComponentMenu( "Audio/Audio Helper" )]
  public class AudioHelper : MonoBehaviour
  {
    public void Play(AudioClip clip, AudioType type)
    {
      if (!clip || !AudioManager.Loaded) return;
      AudioManager.Play(clip, type);
    }

    public void Stop(AudioType type)
    {
      if (!AudioManager.Loaded) return;
      AudioManager.Stop(type);
    }
  }
}