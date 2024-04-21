using UnityEngine;
using UnityEngine.UI;

public class CrouchZone : MonoBehaviour
{
    [SerializeField] private Button _crouchButton;
    [SerializeField] private Image _buttonParentImage;
    [SerializeField] private Button _runButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Crouching crouch))
        {
            if(crouch.IsCrouch)
            {
                _crouchButton.enabled = false;
                _crouchButton.image.enabled = false;
                _buttonParentImage.enabled = false;
                _runButton.enabled = false;
                _runButton.image.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Crouching crouch))
        {
            if (crouch.IsCrouch)
            {
                _crouchButton.enabled = true;
                _crouchButton.image.enabled = true;
                _buttonParentImage.enabled = true;
                _runButton.enabled = true;
                _runButton.image.enabled = true;
            }
        }
    }
}
