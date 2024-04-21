using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVisitor : MonoBehaviour
{
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private WayPoint[] _points;
    [SerializeField] private float _distanceToChangePoint;
    [SerializeField] private float _timeChecking;

    public bool NoTargetInVision = true;
    public bool NeedGoPatrul = true;

    private NavMeshAgent _agent;
    private int _currentPointIndex = 0;
    private float pointDistance;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(NoTargetInVision && !EnemyVision.IsChaking && !_enemyVision.Busy && NeedGoPatrul)
        {
            _enemyAnimation.AnimationState = MonsterState.Walk;
            _agent.SetDestination(_points[_currentPointIndex].transform.position);
            pointDistance = Vector3.Distance(gameObject.transform.position, _points[_currentPointIndex].transform.position);
            if (pointDistance < _distanceToChangePoint)
            {
                if (_points[_currentPointIndex].CheckingPoint && !_points[_currentPointIndex].Checked)
                {
                    StartCoroutine(CheckPoint());
                }
                _currentPointIndex++;
                _currentPointIndex %= _points.Length;
            }
            if(_currentPointIndex <= _points.Length - 1)
            {
                foreach(var point in _points)
                {
                    point.Checked = false;
                }
            }
        }
    }

    private IEnumerator CheckPoint()
    {
        _enemyVision.Busy = true;
        _agent.enabled = false;
        NeedGoPatrul = false;
        _enemyAnimation.AnimationState = MonsterState.Looking;
        yield return new WaitForSeconds(_timeChecking);
        _points[_currentPointIndex].Checked = true;
        _enemyAnimation.AnimationState = MonsterState.Walk;
        _agent.enabled = true;
        NeedGoPatrul = true;
        _enemyVision.Busy = false;
    }
}
