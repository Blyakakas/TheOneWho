using TMPro;
using UnityEngine;

public class NoteInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _letter;
    [SerializeField, TextArea(10,10)] private string _currentText;
    [SerializeField] private TextMeshProUGUI _targetText;

    public string NameOfInteraction { get; set; } = "записка";

    public void Interact()
    {
        _letter.SetActive(true);
        _targetText.text = _currentText;
    }

    private void OnValidate()
    {
        _targetText.text = _currentText;
    }
}
