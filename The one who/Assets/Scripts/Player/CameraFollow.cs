using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    private Transform _playerTransform => _player.transform;

    public static bool NeedRotaing = true;
    private void Update()
    {
        if(NeedRotaing)
        {
            _playerTransform.rotation =
           Quaternion.Euler(_playerTransform.eulerAngles.x, transform.eulerAngles.y, _playerTransform.eulerAngles.z);
        }
    }
}
