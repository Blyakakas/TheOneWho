using UnityEngine;
using UnityEngine.EventSystems;

public class Running : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerMovement _player;

    public void OnPointerDown(PointerEventData eventData)
    {
        _player.IsRunning = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _player.IsRunning = false;
    }
}
