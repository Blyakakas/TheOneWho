using UnityEngine;
using UnityEngine.AI;

public class NoiseInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private EnemyVision _vision;
    [SerializeField] private Transform _noisePoint;
    [SerializeField] private NavMeshAgent _ai;
    [SerializeField] private float _distanceStopChaking;

    private float _distanceToNoise;

    public string NameOfInteraction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Update()
    {
        if(EnemyVision.IsChaking)
        {
            _distanceToNoise = Vector3.Distance(_ai.transform.position,_noisePoint.position);
            _ai.SetDestination(_noisePoint.position);
            if(_distanceToNoise <= _distanceStopChaking)
            {
                EnemyVision.IsChaking = false;
            }
        }
    }

    public void Interact()
    {
        EnemyVision.IsChaking = true;
    }
}
