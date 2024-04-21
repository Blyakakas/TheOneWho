using TMPro;
using UnityEngine;

public class InputTextCheck : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private GameObject _objectNoActive;

    public void Check(string needText)
    {
        if (_inputField.text == needText)
        {
            _targetObject.SetActive(true);
            _objectNoActive.SetActive(false);
        }
    }
}
