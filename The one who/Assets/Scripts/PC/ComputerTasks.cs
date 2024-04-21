using System;
using TMPro;
using UnityEngine;

public class ComputerTasks : MonoBehaviour, ITaskable
{
    public TextMeshProUGUI TextWaterRestart;
    public TextMeshProUGUI TextEmergencyEquipment;
    public bool IsCompleted { get; set; }

    public void TaskComplete()
    {
        IsCompleted = true;
        TextWaterRestart.text = "succesfull";
        TextEmergencyEquipment.text = "succesfull";
        TextWaterRestart.gameObject.SetActive(false);
        TextWaterRestart.gameObject.SetActive(false);
    }
}
