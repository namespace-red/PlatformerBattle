using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAnimationsController : MonoBehaviour, IMoveAnimation, IDamageableAnimation
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetMoveState(bool state)
        => _animator.SetBool(Params.IsFlying, state);

    public void PlayTakeHit()
        => _animator.SetTrigger(State.TakeHit);

    public void PlayDeath()
        => _animator.SetTrigger(State.Death);

    private static class Params
    {
        public const string IsFlying = nameof(IsFlying);
    }

    private static class State
    {
        public const string TakeHit = nameof(TakeHit);
        public const string Death = nameof(Death);
    }
}
