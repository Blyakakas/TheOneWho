using UnityEngine;

public class Crowbar : MonoBehaviour, IInteractable
{
    public string NameOfInteraction { get; set; } = "���";

    public void Interact()
    {
        WoodInteraction.CrowbarEquip = true;
        Destroy(gameObject);
    }
}
