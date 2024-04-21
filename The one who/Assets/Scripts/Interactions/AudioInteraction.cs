using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip[] _auidoClips;
    [SerializeField] private bool _oneAttempt;
    [SerializeField] private bool _needDelay;
    [SerializeField] private bool _randomSound;

    public string NameOfInteraction { get; set; }

    private float _delay;
    private AudioInteraction _audioInteraction;
    private int randomValue;

    private void Start()
    {
        _audioInteraction = GetComponent<AudioInteraction>();
    }

    public void Interact()
    {
        randomValue = Random.Range(0, _auidoClips.Length);
        if (!_randomSound)
            _audioSource.PlayOneShot(_audioClip);
        else
            _audioSource.PlayOneShot(_auidoClips[randomValue]);
        if (_needDelay)
            StartCoroutine(AudioDelay());
        if (_oneAttempt)
            Destroy(_audioInteraction);
    }

    private IEnumerator AudioDelay()
    {
        if (_randomSound)
            _delay = _auidoClips[randomValue].length;
        else
             _delay = _audioClip.length;
        Interaction.InteractionEnable = false;
        yield return new WaitForSeconds(_delay);
        Interaction.InteractionEnable = true;
    }
}
