using UnityEngine;

public class MannequenInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Mannesquin[] _mannesquins;
    public string NameOfInteraction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Interact()
    {
        foreach(var man in _mannesquins)
        {
            man.NeedRotating = true;
            Quaternion lookRotation = Quaternion.LookRotation(_player.transform.position - man.transform.position);
            man.transform.rotation = new Quaternion(man.transform.rotation.x, lookRotation.y, man.transform.rotation.z, lookRotation.w);
        }
    }
}
