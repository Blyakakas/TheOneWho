using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Killing : MonoBehaviour
{
    [SerializeField] private GameObject[] headPlayerParts;
    [SerializeField] private Transform _killingPoint;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private NavMeshAgent _ai;
    [SerializeField] private Camera _cameraForDie;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private GameObject _targetCamera;
    [SerializeField] private CameraControllerPanel _controllerPanel;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _interpolationSpeed;
    [SerializeField] private float _secondToKill;
    [SerializeField] private float _verticalCameraAxis;
    [SerializeField] private float _horizontalCameraAxis;

    public bool _startKilling;
    public float timer;

    private void Update()
    {
        if (_startKilling)
        {
            if (timer <= 0)
            {
                if(!EnemyVision.IsPlayerHide && !_enemyVision.IsBrokeDoor)
                {
                    foreach (var headPart in headPlayerParts)
                    {
                        headPart.SetActive(false);
                    }
                    _cameraForDie.enabled = true;
                    _playerMovement.enabled = false;
                    _playerMovement.transform.position = Vector3.Lerp(_playerMovement.transform.position, _killingPoint.position, _interpolationSpeed * Time.deltaTime);
                    _cameraForDie.transform.LookAt(_targetCamera.transform);
                    _ai.enabled = false;
                    _animator.SetTrigger("Kill");
                    _playerAnimator.SetTrigger("Kill");
                    _controllerPanel.enabled = false;
                    Destroy(_playerCamera.gameObject);
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            timer = _secondToKill;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            _startKilling = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _startKilling = false;
    }
}
