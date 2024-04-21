using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void Play(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
