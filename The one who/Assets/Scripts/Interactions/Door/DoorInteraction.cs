using System.Collections;
using UnityEngine;

public enum MonsterLocation
{
    Forward,
    Backward
}

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openSound;
    [SerializeField] private AudioClip _closeSound;
    [SerializeField] private AudioClip _keyOpenSound;
    [SerializeField] private AudioClip _tryDoorOpen;
    [SerializeField] private float _keyOpenTime;
    [SerializeField] private Animator _animator;
    [SerializeField] private LockerInteraction _locker;
    [SerializeField] private string _nameOfAnimations;

    private Collider _lockerCollider;

    public MonsterLocation MonsterLocations;
    public string NameOfInteraction { get; set; }
    public bool Locked = false;
    public bool NeedKey = false;
    public bool _broken = false;
    public bool IsOpen = false;
    [HideInInspector] public bool HaveKey = false;

    private void Start()
    {
        NameOfInteraction = "відкрити";
        _lockerCollider = _locker.GetComponent<Collider>();
    }

    public void Interact()
    {
        if(!_broken)
        {
            if (!NeedKey && !Locked)
            {
                IsOpen = !IsOpen;
                _animator.SetBool(_nameOfAnimations, IsOpen);
                if (IsOpen)
                {
                    _audioSource.PlayOneShot(_openSound);
                    NameOfInteraction = "закрити";
                    _lockerCollider.enabled = false;
                }
                else
                {
                    _audioSource.PlayOneShot(_closeSound);
                    NameOfInteraction = "відкрити";
                    _lockerCollider.enabled = true;
                }
            }
            else if (HaveKey && !Locked)
            {
                StartCoroutine(KeyOpenDoor());
            }
            else
            {
                _audioSource.PlayOneShot(_tryDoorOpen);
            }
        }
        else
        {
            StopCoroutine(KeyOpenDoor());
        }
    }

    public void EnemyOpen()
    {
        IsOpen = !IsOpen;
        _animator.SetBool(_nameOfAnimations, IsOpen);
        if (IsOpen)
        {
            _audioSource.PlayOneShot(_openSound);
        }
        else
        {
            _audioSource.PlayOneShot(_closeSound);
        }
    }

    public void Broke()
    {
        if(MonsterLocations == MonsterLocation.Forward)
        {
            _animator.SetTrigger("Broke");
            Debug.Log("forward");
        }
        else
        {
            _animator.SetTrigger("BrokeVariant2");
        }
        _broken = true;
    }

    private IEnumerator KeyOpenDoor()
    {
        _audioSource.PlayOneShot(_keyOpenSound);
        gameObject.layer = LayerMask.NameToLayer("Default");
        CameraControllerPanel.CameraEnabled = false;
        PlayerInput.CanMove = false;
        yield return new WaitForSeconds(_keyOpenTime);
        CameraControllerPanel.CameraEnabled = true;
        PlayerInput.CanMove = true;
        NeedKey = false;
        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }
}
