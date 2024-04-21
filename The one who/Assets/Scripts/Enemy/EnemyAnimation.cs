using UnityEngine;

public enum MonsterState
{
    Walk,
    Run,
    BrokeDoor,
    OpenDoor,
    Looking,
    Kill,
    KillUnderBed
}

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public MonsterState AnimationState;

    private void Update()
    {
        if(AnimationState == MonsterState.Walk)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isRunning", false);
            _animator.SetBool("Broking", false);
            _animator.SetBool("Opening", false);
            _animator.SetBool("IsLooking", false);
        }
        else if(AnimationState == MonsterState.Run)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", true);
            _animator.SetBool("Opening", false);
            _animator.SetBool("IsLooking", false);
            _animator.SetBool("Broking", false);
        }
        else if (AnimationState == MonsterState.BrokeDoor)
        {
            _animator.SetBool("Broking", true);
        }
        else if(AnimationState == MonsterState.OpenDoor)
        {
            _animator.SetBool("Opening", true);
        }
        else if (AnimationState == MonsterState.KillUnderBed)
        {
            _animator.SetTrigger("UnderBedKill");
        }
        else
        {
            _animator.SetBool("IsLooking", true);
        }
    }
}
