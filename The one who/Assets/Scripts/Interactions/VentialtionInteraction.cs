using UnityEngine;

public class VentialtionInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private int countForBroke;
    [SerializeField] private Animator _animator;

    public string NameOfInteraction { get; set; } = "вентил€ц≥€";

    public void Interact()
    {
        countForBroke--;
        if (countForBroke <= 0)
            _animator.SetTrigger("Broke");
    }
}
