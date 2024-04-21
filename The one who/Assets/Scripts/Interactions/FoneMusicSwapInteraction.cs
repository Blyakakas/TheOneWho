using UnityEngine;

public class FoneMusicSwapInteraction : MonoBehaviour, IInteractable
{
    public string NameOfInteraction { get; set; }

    public void Interact()
    {
        FoneMusic.OnAudioTypeChanged?.Invoke();
    }
}
