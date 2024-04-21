using UnityEngine;

public class HideZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            EnemyVision.IsPlayerHide = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            EnemyVision.IsPlayerHide = false;
        }
    }
}
