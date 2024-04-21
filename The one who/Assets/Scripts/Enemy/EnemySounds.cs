using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _aggresiveAudioSource;
    [SerializeField] private AudioClip[] _horrorVoice;
    [SerializeField] private AudioClip _brokeDoorSound;
    [SerializeField] private AudioClip _brokeDoorSoundVariant_2;
    [SerializeField] private AudioClip _footStep;
    [SerializeField] private EnemyVisitor _enemyVisitor;
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private float _delayForNewScream;

    private void Start()
    {
        StartCoroutine(ScreamMonster());
    }

    private IEnumerator ScreamMonster()
    {
        int random = Random.Range(0, _horrorVoice.Length);
        if (!_enemyVisitor.NoTargetInVision || _enemyVision.AlwaysSeeTheTarget)
            _aggresiveAudioSource.PlayOneShot(_horrorVoice[random]);
        yield return new WaitForSeconds(_horrorVoice[random].length + _delayForNewScream);
        StartCoroutine(ScreamMonster());
    }

    public void PlayDoorBroke()
    {
        _audioSource.PlayOneShot(_brokeDoorSound);
    }
    public void PlayDoorBrokeVariant_2()
    {
        _audioSource.PlayOneShot(_brokeDoorSoundVariant_2);
    }
    public void PlayFootSound()
    {
        _audioSource.PlayOneShot(_footStep);
    }
}
