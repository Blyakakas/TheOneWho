using UnityEngine;

[RequireComponent (typeof(PlayerInput))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Transform _aimTargerIK;
    [SerializeField] private Camera _mainCamera;

    private Animator _animator;
    private PlayerInput _input;
    private Vector3 _desigredTargetPostion;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        _animator.SetFloat("X", _input.X);
        _animator.SetFloat("Z", _input.Z);

        Ray desigredTargetRay = _mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        _desigredTargetPostion = desigredTargetRay.origin + desigredTargetRay.direction * 0.7f;
        _aimTargerIK.position = _desigredTargetPostion;
    }
}
