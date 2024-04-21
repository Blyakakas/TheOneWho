using UnityEngine;

public class LampPickUp : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _lampPoint;
    public string NameOfInteraction { get; set; } = "Лампа";

    public void Interact()
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.SetParent(_player);
        transform.position = _lampPoint.position;
    }
}
