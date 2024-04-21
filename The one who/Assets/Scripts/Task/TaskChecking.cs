using UnityEngine;

public class TaskChecking : MonoBehaviour
{
    [SerializeField] private DoorTask[] doorTasks;
    [SerializeField] private Animator _animatorCompleted;
    [SerializeField] private string _triggerName;

    private bool _taskComplete = false;

    public void CheckTasks()
    {
        foreach(DoorTask task in doorTasks)
        {
            if(task.IsCompleted)
            {
                _taskComplete = true;
            }
            else
            {
                _taskComplete = false;
                return;
            }  
        }
        Debug.Log("COMPLETE");
        _animatorCompleted.SetTrigger(_triggerName);
    }
}
