using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private Button _interactionButton;
    [SerializeField] private TextMeshProUGUI _interactionText;

    private RaycastHit _hitInfo;
    private Ray _ray;

    public static bool InteractionEnable = true;

    private void Start()
    {
        _interactionButton.onClick.AddListener(ButtonInteraction);
    }

    private void ButtonInteraction()
    {
        IInteractable[] interactable = _hitInfo.collider.GetComponents<IInteractable>();
        foreach(var interact in interactable)
        {
            interact.Interact();
        }
    }

    private void Update()
    {
        _ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward,Color.yellow);
        if(Physics.Raycast(_ray, out _hitInfo, _rayLength,_interactionLayer) && InteractionEnable)
        {
            if (_hitInfo.collider.TryGetComponent(out IInteractable interactable))
            {
                _interactionButton.gameObject.SetActive(true);
                _interactionText.text = interactable.NameOfInteraction;
            }
            else
            {
                _interactionButton.gameObject.SetActive(false);
                _interactionText.text = "";
            }
        }
        else
        {
            _interactionButton.gameObject.SetActive(false);
            _interactionText.text = "";
        }
    }
}
