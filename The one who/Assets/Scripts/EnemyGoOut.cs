using UnityEngine;

public class EnemyGoOut : MonoBehaviour
{
    [SerializeField] private EnemyVision _enemyVision;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _enemyVision.GoOut = true;
        }
    }
}
