using UnityEngine;

public class KeyInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorInteraction _door;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private string _targetTextInteraction;
    public string NameOfInteraction { get; set; }

    private void Start()
    {
        NameOfInteraction = _targetTextInteraction;
    }

    public void Interact()
    {
        _audioSource.PlayOneShot(_audioClip);
        _door.HaveKey = true;
        Destroy(gameObject);
    }
}
