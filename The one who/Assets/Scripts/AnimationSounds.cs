using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audio;

    public void PlayAudio()
    {
        _audioSource.PlayOneShot(_audio);
    }
}
