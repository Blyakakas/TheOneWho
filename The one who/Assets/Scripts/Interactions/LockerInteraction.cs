using System;
using UnityEngine;

public class LockerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorInteraction _door;
    [SerializeField] private string _nameOfInteraction;

    public string NameOfInteraction { get; set; }

    private void Start()
    {
        NameOfInteraction = _nameOfInteraction;
    }

    public void Interact()
    {
            _door.Locked = !_door.Locked;
    }
}
