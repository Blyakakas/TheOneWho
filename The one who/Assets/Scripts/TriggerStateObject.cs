using UnityEngine;

public class TriggerStateObject : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private bool _condition;
    [SerializeField] private bool _stopPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_stopPlayer)
            {
                PlayerInput.CanMove = false;
            }
            _targetObject.SetActive(_condition);
        }
    }
}
