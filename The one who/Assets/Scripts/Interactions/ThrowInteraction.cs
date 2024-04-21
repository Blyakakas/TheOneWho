using UnityEngine;
using UnityEngine.UI;

public class ThrowInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Button _pickDownButton;
    [SerializeField] private Camera _cameraPlayer;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private Transform _normalParent;
    [SerializeField] private string _targetTextInteraction;
    [SerializeField] private float _throwForce;
    
    private Rigidbody _rigidbody;
    private bool _canThrow;

    public static bool CanRaise = true;

    public string NameOfInteraction { get; set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        NameOfInteraction = _targetTextInteraction;
    }

    public void PickDown()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _canThrow = false;
        CanRaise = true;
        _pickDownButton.image.enabled = false;
        transform.parent = _normalParent;
    }

    public void Interact()
    {
        _pickDownButton.image.enabled = true;
        _pickDownButton.onClick.AddListener(PickDown);
        if(CanRaise)
        {
            transform.position = _throwPoint.position;
            transform.parent = _throwPoint;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _canThrow = true;
            CanRaise = false;
            transform.localEulerAngles = Vector3.zero;
        }
        else if(_canThrow)
        {
            _pickDownButton.image.enabled = false;
            CanRaise = true;
            _canThrow = false;
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            transform.parent = _normalParent;
            _rigidbody.AddForce(_cameraPlayer.transform.forward * _throwForce, ForceMode.Impulse);
        }
    }
}
