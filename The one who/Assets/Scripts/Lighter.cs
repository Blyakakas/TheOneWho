using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _lightValue = false;
    
    public void SwitchLight()
    {
        _animator.SetBool("OnLight", _lightValue);
        _lightValue = !_lightValue;
    }
}
