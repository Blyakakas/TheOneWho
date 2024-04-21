using System.Collections;
using TMPro;
using UnityEngine;

public class Subtitles : MonoBehaviour
{
    [SerializeField] private SubtitlesTriggerType triggerType;
    [Header("Subtitles")]
    [SerializeField] private string[] _subtitlesPart;
    [SerializeField] private float[] _subtitlesDelay;
    [SerializeField] private TextMeshProUGUI _targetText;

    private int _index = 0;

    public void Start()
    {
        if(triggerType == SubtitlesTriggerType.OnStart)
        {
            _index = 0;
            StartCoroutine(StartSubtitles());
        }
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
}

public enum SubtitlesTriggerType
{
    Trigger,
    OnStart
};
