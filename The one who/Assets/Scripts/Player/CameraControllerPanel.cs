using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControllerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private DeviceChaking _device;
    [SerializeField] private CinemachineVirtualCamera _CVC;
    [SerializeField] private float _sensitivity;

    private bool _pressed = false;
    private CinemachinePOV _CPOV;
    private int _fingerId;
    public float test;

    public static bool CameraEnabled = true;

    private void Start()
    {
        _CPOV = _CVC.GetCinemachineComponent<CinemachinePOV>();
        if (_device.DeviceTypes == DeviceTypes.Android)
        {
            _CPOV.m_HorizontalAxis.m_MaxSpeed = _sensitivity;
            _CPOV.m_VerticalAxis.m_MaxSpeed = _sensitivity;
            _CPOV.m_HorizontalAxis.m_InputAxisName = "";
            _CPOV.m_VerticalAxis.m_InputAxisName = "";
        }
        else
        {
            _CPOV.m_HorizontalAxis.m_InputAxisName = "Mouse X";
            _CPOV.m_VerticalAxis.m_InputAxisName = "Mouse Y";
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            _pressed = true;
            _fingerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        _CPOV.m_VerticalAxis.m_InputAxisValue = 0;
        _CPOV.m_HorizontalAxis.m_InputAxisValue = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (_pressed && CameraEnabled)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == _fingerId)
                {
                    test = touch.deltaPosition.x;
                    if (touch.phase == TouchPhase.Moved)
                    {
                        _CPOV.m_VerticalAxis.m_InputAxisValue = touch.deltaPosition.y;
                        _CPOV.m_HorizontalAxis.m_InputAxisValue = touch.deltaPosition.x;
                    }
                    if (touch.phase == TouchPhase.Stationary)
                    {
                        _CPOV.m_VerticalAxis.m_InputAxisValue = 0;
                        _CPOV.m_HorizontalAxis.m_InputAxisValue = 0;
                    }
                }
            }
        }
    }
}