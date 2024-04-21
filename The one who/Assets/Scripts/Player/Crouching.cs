using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMovement))]
public class Crouching : MonoBehaviour
{
    [Header("CrouchProperty")]
    [SerializeField] private float crouchDelay;
    [SerializeField] private float crouchSpeed;

    [Header("CharacterCrouchAtribute")]
    [SerializeField] private Vector3 _centerOfCharacter;
    [SerializeField] private float _characterHeight;
    [SerializeField] private float _characterWidth;

    private Vector3 _startCenterOfCharacter;
    private float _startCharacterHeight;
    private float _startCharacterWidth;

    private CharacterController _characterController;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    public bool IsCrouch;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();

        _startCenterOfCharacter = _characterController.center;
        _startCharacterHeight = _characterController.height;
        _startCharacterWidth = _characterController.radius;
    }

    public void Crouch()
    {
        StartCoroutine(CrouchGravityDelay());
        _playerMovement.CrouchSpeedChange(crouchSpeed, ref IsCrouch);
        if (IsCrouch)
        {
            _characterController.center = _centerOfCharacter;
            _characterController.height = _characterHeight;
            _characterController.radius = _characterWidth;
        }
        else
        {
            _characterController.center = _startCenterOfCharacter;
            _characterController.height = _startCharacterHeight;
            _characterController.radius = _startCharacterWidth;
        }
        _animator.SetBool("isCrouch", IsCrouch);
    }

    private IEnumerator CrouchGravityDelay()
    {
        _playerMovement.GravityEnabled = false;
        yield return new WaitForSeconds(crouchDelay);
        _playerMovement.GravityEnabled = true;
    }
}
