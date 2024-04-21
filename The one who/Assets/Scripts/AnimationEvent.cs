using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private enum AnimationType
    {
        Trigger,
        Bool
    };

    [SerializeField] private string _animationName;
    [SerializeField] private AnimationType _animationType;
    [SerializeField] private bool _booleanCondition;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        if (_animationType == AnimationType.Trigger)
            _animator.SetTrigger(_animationName);
        else
            _animator.SetBool(_animationName, _booleanCondition); 
    }
}
