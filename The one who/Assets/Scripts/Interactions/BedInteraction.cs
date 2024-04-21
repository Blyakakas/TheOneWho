using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BedInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _pointNearBed;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private CameraControllerPanel _cameraControllerPanel;
    [SerializeField] private Crouching _crouching;
    [SerializeField] private Button _crouchingButton;
    [SerializeField] private float _verticalCameraAxis;
    [SerializeField] private float _horizontalCameraAxis;
    [SerializeField] private float _interpolationSpeed;
    [SerializeField] private float _delayAnimationStart;

    private CinemachinePOV _POV;
    private bool _isMove;
    private bool _underBed = false;

    public string NameOfInteraction { get; set; }

    private void Start()
    {
        _POV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        NameOfInteraction = "ë³æêî";
    }

    private void Update()
    {
       if(_isMove)
       {
            _playerMovement.enabled = false;
            _POV.m_HorizontalAxis.Value = Mathf.Lerp(_POV.m_HorizontalAxis.Value, _horizontalCameraAxis, _interpolationSpeed * Time.deltaTime);
            _POV.m_VerticalAxis.Value = Mathf.Lerp(_POV.m_VerticalAxis.Value, _verticalCameraAxis, _interpolationSpeed * Time.deltaTime);
            _playerMovement.transform.position = Vector3.Lerp(_playerMovement.transform.position, _pointNearBed.position, _interpolationSpeed * Time.deltaTime);
       }
       if(_underBed)
       {
            _crouchingButton.gameObject.SetActive(false);
        }
       else
       {
            _crouchingButton.gameObject.SetActive(true);
        }
    }
    public void Interact()
    {
        if(!_crouching.IsCrouch)
        {
            EnemyVision.IsPlayerHide = !EnemyVision.IsPlayerHide;
            _underBed = !_underBed;
            StartCoroutine(AnimationStart());
        }
    }

    private IEnumerator AnimationStart()
    {
        _playerMovement.enabled = false;
        _cameraControllerPanel.enabled = false;
        _isMove = true;
        if(_underBed)
            yield return new WaitForSeconds(_delayAnimationStart);
        _playerAnimator.SetBool("underBed", _underBed);
        _isMove = false;
        _cameraControllerPanel.enabled = true;
        _playerMovement.enabled = true;
    }
}
