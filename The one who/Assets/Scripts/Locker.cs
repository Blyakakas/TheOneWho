using UnityEngine;

public class Locker : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private HideZone _lockerHideZone;

    private void Update()
    {
        if(!_animator.GetBool("isOpen"))
            _lockerHideZone.gameObject.SetActive(true);
        else
            _lockerHideZone.gameObject.SetActive(false);
    }
}
