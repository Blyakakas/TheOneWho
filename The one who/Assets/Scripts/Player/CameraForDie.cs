using UnityEngine;

public class CameraForDie : MonoBehaviour
{
    [SerializeField] private Camera _cameraPlayer;
    [SerializeField] private Camera _cameraDie;

    private void Update()
    {
        if(_cameraPlayer.enabled)
            _cameraDie.transform.rotation = _cameraPlayer.transform.rotation;
    }
}
