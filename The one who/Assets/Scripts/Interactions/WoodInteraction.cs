using UnityEngine;

public class WoodInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private float _destroyDelay;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;

    private Rigidbody _rigidbody;

    public string NameOfInteraction { get; set; } = "доска";
    public static bool CrowbarEquip = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        if(CrowbarEquip)
        {
            _source.Play();
            _rigidbody.isKinematic = false;
            Destroy(gameObject, _destroyDelay);
        }
    }
}
