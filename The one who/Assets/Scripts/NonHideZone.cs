using UnityEngine;

public class NonHideZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            EnemyVision.IsPlayerHide = false;
        }
    }
}
