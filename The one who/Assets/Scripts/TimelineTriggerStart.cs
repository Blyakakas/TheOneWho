using UnityEngine;
using UnityEngine.Playables;

public class TimelineTriggerStart : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            _playableDirector.Play();
            Destroy(gameObject);
        }
    }
}
