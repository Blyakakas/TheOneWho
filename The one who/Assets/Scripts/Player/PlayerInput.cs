using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystickInput;
    [SerializeField] private DeviceChaking _device;
    [SerializeField] protected float _interpolationSpeed;
    public float X {get; private set;}
    public float Z { get; private set;}
    public static bool CanMove = true;

    private void Update()
    {
        if(CanMove)
        {
            if (_device.DeviceTypes == DeviceTypes.Android)
            {
                X = Mathf.Lerp(X, _joystickInput.Horizontal, _interpolationSpeed * Time.deltaTime);
                Z = Mathf.Lerp(Z, _joystickInput.Vertical, _interpolationSpeed * Time.deltaTime);
                return;
            }
            X = Mathf.Lerp(X, Input.GetAxis("Horizontal"), _interpolationSpeed * Time.deltaTime);
            Z = Mathf.Lerp(Z, Input.GetAxis("Vertical"), _interpolationSpeed * Time.deltaTime);
        }
        else
        {
            X = 0f;
            Z = 0f;
        }
    }

    public void ChangeMoveState(bool state)
    {
        CanMove = state;
    }
}
