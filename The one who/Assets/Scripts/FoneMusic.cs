using System;
using UnityEngine;

public class FoneMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _normalOST;
    [SerializeField] protected AudioSource _horrorOST;
    [SerializeField] private float volumeUpSpeed;
    [SerializeField, Range(0f, 1f)] private float _maxNormalOSTVolume;
    [SerializeField, Range(0f, 1f)] private float _maxScreamOSTVolume;

    public bool HorrorMoment = false;
    public static Action OnAudioTypeChanged;

    private void Awake()
    {
        OnAudioTypeChanged += AudioTypeSwap;
    }

    private void OnDisable()
    {
        OnAudioTypeChanged -= AudioTypeSwap;
    }

    private void Update()
    {
        if(HorrorMoment)
        {
            _horrorOST.volume = Mathf.Lerp(_horrorOST.volume, _maxScreamOSTVolume, Time.deltaTime * volumeUpSpeed);
            _normalOST.volume = Mathf.Lerp(_normalOST.volume, 0, Time.deltaTime * volumeUpSpeed);
        }
        else
        {
            _normalOST.volume = Mathf.Lerp(_normalOST.volume, _maxNormalOSTVolume, Time.deltaTime * volumeUpSpeed);
            _horrorOST.volume = Mathf.Lerp(_horrorOST.volume, 0, Time.deltaTime * volumeUpSpeed);
        }
    }

    private void AudioTypeSwap()
    {
        HorrorMoment = !HorrorMoment;
    }
}
