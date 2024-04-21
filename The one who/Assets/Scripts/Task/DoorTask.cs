using UnityEngine;

public class DoorTask : MonoBehaviour, ITaskable, IInteractable
{
    [SerializeField] private DoorStates _doorStates;
    [SerializeField] private TaskChecking _taskChecking;
    [SerializeField] private DoorInteraction _door;
    [SerializeField] private string _targetTextInteraction;

    public bool IsCompleted { get; set; }
    public string NameOfInteraction { get; set; }

    private void OnValidate()
    {
        NameOfInteraction = _targetTextInteraction;
    }

    private void Start()
    {
        if(_doorStates == DoorStates.Close)
        {
            IsCompleted = true;
            _taskChecking.CheckTasks();
        }
    }

    public void Interact()
    {
        if(_doorStates == DoorStates.Open && !_door.IsOpen)
        {
            IsCompleted = true;
            _taskChecking.CheckTasks();
        }
        else if(_doorStates == DoorStates.Close && _door.IsOpen)
        {
            IsCompleted = true;
            _taskChecking.CheckTasks();
        }
        else
        {
            IsCompleted = false;
        }
    }
}

public enum DoorStates
{
    Open,
    Close
};
