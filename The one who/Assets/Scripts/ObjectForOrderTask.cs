using UnityEngine;

public class ObjectForOrderTask : MonoBehaviour
{
    [SerializeField] private string _correctTag;

    [HideInInspector] public bool isPassed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _correctTag)
        {
            isPassed = true;
        }
    }
}
