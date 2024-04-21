using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private int _raysCount;
    [SerializeField] private int _distance;
    [SerializeField] private float _checkDoorDistance;
    [SerializeField] private float _angle;
    [SerializeField] private float _distanceToReacton = 2f;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _doorNormalOpenTime;
    [SerializeField] private float _doorBrokeTime;
    [SerializeField] private float _checkForPlayerTime;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _estimatedTarget;
    [SerializeField] private Transform _pointToGoOut;
    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _ai;
    [SerializeField] private EnemyVisitor _enemyVisitor;
    [SerializeField] private EnemyAnimation _enemyAnimation;

    private float _distanceToTarget;
    private RaycastHit _hitDoor;
    public bool _needCheck = false;
    [HideInInspector] public bool Busy = false;
    public bool AlwaysSeeTheTarget = false;
    public bool SeePlayerWhenHide = false;
    public bool GoOut = false;
    public bool IsBrokeDoor = false;

    public static bool IsPlayerHide = false;
    public static bool IsChaking = false;

    void Update()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _target.position);
        if (!Busy && !GoOut)
        {
            if (_distanceToTarget < _distance)
            {
                RayToCheckDoor();
                if (RayToScan() && !SeePlayerWhenHide && !Busy && !AlwaysSeeTheTarget && !IsPlayerHide)
                {
                    _enemyAnimation.AnimationState = MonsterState.Run;
                    _ai.enabled = true;
                    if (!IsChaking)
                    {
                        _ai.SetDestination(_target.position);
                        _needCheck = true;
                        if (IsPlayerHide)
                            SeePlayerWhenHide = true;
                        _estimatedTarget.position = _target.position;
                    }
                    _enemyVisitor.NoTargetInVision = false;
                }
                else if (_distanceToTarget <= _distanceToReacton && !SeePlayerWhenHide && !Busy && !AlwaysSeeTheTarget)
                {
                    if(!IsPlayerHide)
                    {
                        _enemyAnimation.AnimationState = MonsterState.Run;
                        _ai.enabled = true;
                        if (!IsChaking)
                        {
                            Debug.Log(IsPlayerHide);
                            _ai.SetDestination(_target.position);
                            _needCheck = true;
                            if (IsPlayerHide)
                                SeePlayerWhenHide = true;
                            _estimatedTarget.position = _target.position;
                            _enemyVisitor.NoTargetInVision = false;
                        }
                    }
                }
                else if(!Busy && AlwaysSeeTheTarget && !IsPlayerHide)
                {                   
                    _estimatedTarget.position = _target.position;
                    _ai.SetDestination(_target.position);
                    _enemyAnimation.AnimationState = MonsterState.Run;
                }
                else if(!Busy && !AlwaysSeeTheTarget && _needCheck && !SeePlayerWhenHide)
                {
                    _ai.SetDestination(_estimatedTarget.position);
                    if(Vector3.Distance(_ai.transform.position,_estimatedTarget.position) <= 1.5f)
                    {
                        StartCoroutine(GoCheckingForPlayer());
                    }
                }
                else if(AlwaysSeeTheTarget && IsPlayerHide)
                {
                    _estimatedTarget.position = _target.position;
                    _ai.SetDestination(_estimatedTarget.position);
                    if (Vector3.Distance(_ai.transform.position, _estimatedTarget.position) <= 1.5f)
                    {
                        _enemyAnimation.AnimationState = MonsterState.KillUnderBed;
                    }
                    else
                    {
                        _enemyAnimation.AnimationState = MonsterState.Run;
                    }
                }
                else if(!_needCheck && !Busy)
                {
                    _enemyVisitor.NoTargetInVision = true;
                }
            }
            if (_enemyAnimation.AnimationState == MonsterState.Walk)
            {
                _ai.speed = _normalSpeed;
            }
            else
            {
                _ai.speed = _runningSpeed;
            }
        }
        else if(GoOut)
        {
            _ai.SetDestination(_pointToGoOut.position);
            if(AlwaysSeeTheTarget)
                _enemyAnimation.AnimationState = MonsterState.Walk;
            if (Vector3.Distance(_ai.transform.position, _pointToGoOut.position) <= 1.5f)
                Destroy(gameObject);
        }
    }

    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 position = transform.position + _offset;
        if (Physics.Raycast(position, dir, out hit, _distance))
        {
            if (hit.transform == _target)
            {
                result = true;
                Debug.DrawLine(position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(position, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(position, dir * _distance, Color.red);
        }
        return result;
    }

    bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;

        for (int i = 0; i < _raysCount; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += +_angle * Mathf.Deg2Rad / _raysCount;

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
            if (GetRaycast(dir)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(dir)) b = true;
            }
        }
        if (a || b) result = true;
        return result;
    }

    private void RayToCheckDoor()
    {
        if(Physics.Raycast(transform.position, transform.forward, out _hitDoor,_checkDoorDistance))
        {
            if (_hitDoor.transform.TryGetComponent(out DoorInteraction door))
            {
                if (door.Locked && !door._broken)
                {
                    door.Broke();
                    StartCoroutine(BrokeDoor());
                }
                else if(!door.Locked && !door._broken && !door.IsOpen && !door.NeedKey)
                {
                    door.EnemyOpen();
                    StartCoroutine(NormalDoorOpen());
                }
            }
        }
    }

    private IEnumerator BrokeDoor()
    {
        Busy = true;
        _ai.enabled = false;
        IsBrokeDoor = true;
        _enemyAnimation.AnimationState = MonsterState.BrokeDoor;
        yield return new WaitForSeconds(_doorBrokeTime);
        IsBrokeDoor = false;
        _ai.enabled = true;
        Busy = false;
    }

    private IEnumerator NormalDoorOpen()
    {
        _enemyAnimation.AnimationState = MonsterState.OpenDoor;
        Busy = true;
        _ai.enabled = false;
        yield return new WaitForSeconds(_doorNormalOpenTime);
        _ai.enabled = true;
        Busy = false;
    }

    private IEnumerator GoCheckingForPlayer()
    {
        _ai.enabled = false;
        _enemyVisitor.NeedGoPatrul = false;
        _enemyAnimation.AnimationState = MonsterState.Looking;
        yield return new WaitForSeconds(2);
        _needCheck = false;
        _ai.enabled = true;
        _enemyVisitor.NeedGoPatrul = true;
    }
}