using UnityEngine;

public class PCTaskCheck : MonoBehaviour
{
    [SerializeField] private ComputerTasks _computerTask;

    [SerializeField] private Animator _animator;
    public void TaskCheck()
    {
        if(_computerTask.TextEmergencyEquipment.gameObject.activeSelf == true && _computerTask.TextEmergencyEquipment.text == "succesfull")
        {
            Debug.Log("2300S");
            _animator.SetTrigger("Open");
        }
    }
}
