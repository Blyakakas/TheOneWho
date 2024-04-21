using UnityEngine;

public class ObjectActiveInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private bool _condition;
    [SerializeField] private bool _destroyAfter;
    [SerializeField] private bool _needSound;
    [SerializeField] private bool _swap;
    [SerializeField] private float _destroyDelay;
    [SerializeField] private string _targetTextInteraction;
    public string NameOfInteraction { get; set; }

    private void Start()
    {
        NameOfInteraction = _targetTextInteraction;
    }

    public void Interact()
    {
        _targetObject.SetActive(_condition);
        if(_needSound)
            _audio.Play();
        if(_destroyAfter)
            Destroy(gameObject,_destroyDelay);
        if (_swap)
            _condition = !_condition;
    }
}
