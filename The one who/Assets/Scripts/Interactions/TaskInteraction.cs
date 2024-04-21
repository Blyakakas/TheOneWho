using UnityEngine;

public class TaskInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private ComputerTasks task; 
    public string NameOfInteraction { get; set; }

    public void Interact()
    {
        task.TaskComplete();
    }
}
