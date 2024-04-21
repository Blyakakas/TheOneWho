using UnityEngine;

public class Mannesquin : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    public bool NeedRotating = false;

    private void OnBecameInvisible()
    {
        if (NeedRotating)
        {
            Quaternion lookRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
            transform.rotation = new Quaternion(transform.rotation.x, lookRotation.y, transform.rotation.z, lookRotation.w);
        }
    } 
}
