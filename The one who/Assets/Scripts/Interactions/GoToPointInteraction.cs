using Cinemachine;
using System.Collections;
using UnityEngine;

public class GoToPointInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _verticalCameraAxis;
    [SerializeField] private float _horizontalCameraAxis;
    [SerializeField] private float _interpolationSpeed;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private string _nameInteraction;

    private CinemachinePOV _POV;

    public string NameOfInteraction { get; set; }

    private void Start()
    {
        NameOfInteraction = _nameInteraction;
        _POV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        if(_isMoving)
        {
            _playerMovement.enabled = false;
            _playerMovement.transform.position = Vector3.Lerp(_playerMovement.transform.position, _targetPoint.position, _interpolationSpeed * Time.deltaTime);
            _POV.m_HorizontalAxis.Value = Mathf.Lerp(_POV.m_HorizontalAxis.Value, _horizontalCameraAxis, _interpolationSpeed * Time.deltaTime);
            _POV.m_VerticalAxis.Value = Mathf.Lerp(_POV.m_VerticalAxis.Value, _verticalCameraAxis, _interpolationSpeed * Time.deltaTime);
        }
    }

    public void Interact()
    {
        _isMoving = true;
        _playerMovement.enabled = false;
        StartCoroutine(GoToPoint());
    }

    private IEnumerator GoToPoint()
    {
        _joystick.enabled = false;
        _isMoving = true;
        _playerMovement.enabled = false;
        yield return new WaitForSeconds(_moveTime);
        _joystick.enabled = true;
        _isMoving = false;
        _playerMovement.enabled = true;
    }
}
