using UnityEngine;
using System.Collections;
public class AnimationInteraction : MonoBehaviour, IInteractable
{
    private enum AnimationType
    {
        Boolean,
        Trigger
    };

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _swap;
    [SerializeField] private bool _booleanCondition;
    [SerializeField] private bool _needStopPlayer = false;
    [SerializeField] private string _nameOfAnimation;
    [SerializeField] private AnimationType _type;
    [SerializeField] private string _targetTextInteraction;
    [SerializeField] private float _delay;
    [SerializeField] private float _stopPlayerDuration;
    [SerializeField] private bool _triggerAnimationStart = false;

    public string NameOfInteraction { get; set; }

    private void Start()
    {
        NameOfInteraction = _targetTextInteraction;
    }
    public void Interact()
    {
        StartCoroutine(StartAnimation());
        if (_needStopPlayer)
            StartCoroutine(PlayerStop());
    }

    private IEnumerator PlayerStop()
    {
        PlayerInput.CanMove = false;
        yield return new WaitForSeconds(_stopPlayerDuration);
        PlayerInput.CanMove = true;
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(_delay);
        if (_type == AnimationType.Boolean)
        {
            _animator.SetBool(_nameOfAnimation, _booleanCondition);
            if (_swap)
            {
                _booleanCondition = !_booleanCondition;
                yield return null;
            }
            else
            {
                this.enabled = false;
            }
        }
        else
        {
            _animator.SetTrigger(_nameOfAnimation);
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement _playerMovement))
        {
            _animator.SetTrigger(_nameOfAnimation);
          
            gameObject.SetActive(false);
        }
    }
}
