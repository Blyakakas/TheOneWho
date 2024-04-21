using UnityEngine;

public class ForwardCheck : MonoBehaviour
{
    [SerializeField] private DoorInteraction _door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            _door.MonsterLocations = MonsterLocation.Forward;
        }
    }
}
