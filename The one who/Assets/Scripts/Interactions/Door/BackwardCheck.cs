using UnityEngine;

public class BackwardCheck : MonoBehaviour
{
    [SerializeField] private DoorInteraction _door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _door.MonsterLocations = MonsterLocation.Backward;
        }
    }
}
