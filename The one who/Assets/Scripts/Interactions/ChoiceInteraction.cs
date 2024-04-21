using UnityEngine;

public class ChoiceInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _answerPanel;
    [SerializeField] private GameObject _canvasInput;
    public string NameOfInteraction { get; set; } = "зробити вибір";

    public void Interact()
    {
        _answerPanel.SetActive(true);
        _canvasInput.SetActive(false);
    }
}
