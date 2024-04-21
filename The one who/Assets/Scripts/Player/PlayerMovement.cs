using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _speedInterpolation;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _speedFOW;
    [SerializeField] private float _normalFOW;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera _CVC;


    private CharacterController _characterController;
    private PlayerInput _input;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Animator _animator;

    public bool IsRunning = false;
    public bool GravityEnabled;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        _normalFOW = _CVC.m_Lens.FieldOfView;
    }

    private void Update()
    {
        if (_input.Z >= 0.5f && IsRunning)
        {
            _CVC.m_Lens.FieldOfView = Mathf.Lerp(_CVC.m_Lens.FieldOfView, _speedFOW, _speedInterpolation * Time.deltaTime);
            _currentSpeed = Mathf.Lerp(_currentSpeed, _runSpeed, _speedInterpolation * Time.deltaTime);
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _CVC.m_Lens.FieldOfView = Mathf.Lerp(_CVC.m_Lens.FieldOfView, _normalFOW, _speedInterpolation * Time.deltaTime);
            _animator.SetBool("isRunning", false);
            _currentSpeed = Mathf.Lerp(_currentSpeed, _normalSpeed, _speedInterpolation * Time.deltaTime);
        }
        _direction = _input.X * transform.right + _input.Z * transform.forward;   
        _characterController.Move(_direction * _currentSpeed * Time.deltaTime);
        if (GravityEnabled)
            SetGravity();

        if(Input.GetKey(KeyCode.LeftShift))
            IsRunning = true;
        if(Input.GetKeyUp(KeyCode.LeftShift))
            IsRunning = false;
    }

    private void SetGravity()
    {
        _velocity.y = _gravity + Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void CrouchSpeedChange(float crouchSpeed,ref bool crouch)
    {
        float startSpeed = _currentSpeed;
                crouch = !crouch;
        if (crouch)
            _currentSpeed = crouchSpeed;
        else
            _currentSpeed = startSpeed;
    }
}