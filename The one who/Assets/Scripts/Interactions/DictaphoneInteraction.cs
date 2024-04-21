using System.Collections;
using TMPro;
using UnityEngine;

public class DictaphoneInteraction : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _on;
    [SerializeField] private AudioClip _recorded;
    [Header("Subtitles")]
    [SerializeField] private string[] _subtitlesPart;
    [SerializeField] private float[] _subtitlesDelay;
    [SerializeField] private TextMeshProUGUI _targetText;

    private int _index = 0;
    public string NameOfInteraction { get; set; } = "диктофон";

    public void Interact()
    {
        _index = 0;
        _audioSource.PlayOneShot(_on);
        _audioSource.PlayOneShot(_recorded);
        StartCoroutine(DictophoneDelay());
        StartCoroutine(StartSubtitles());
    }

    private IEnumerator StartSubtitles()
    {
        _targetText.text = _subtitlesPart[_index];
        yield return new WaitForSeconds(_subtitlesDelay[_index]);
        _index++;
        if (_index + 1 <= _subtitlesPart.Length)
        {
            StartCoroutine(StartSubtitles());
        }
        else
        {
            _targetText.text = string.Empty;
        }
    }

    private IEnumerator DictophoneDelay()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(_recorded.length);
        GetComponent<Collider>().enabled = true;
    }
}
