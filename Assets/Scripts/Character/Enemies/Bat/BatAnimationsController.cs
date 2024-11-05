using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAnimationsController : MonoBehaviour, IMoveAnimation
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetMoveState(bool state)
    {
        _animator.SetBool(Params.IsFlying, state);
    }

    private static class Params
    {
        public const string IsFlying = nameof(IsFlying);
    }
}
