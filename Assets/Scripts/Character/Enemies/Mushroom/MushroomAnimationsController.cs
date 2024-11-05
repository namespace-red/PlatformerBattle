using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MushroomAnimationsController : MonoBehaviour, IMoveAnimation
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveState(bool state) 
        => _animator.SetBool(Params.IsWalking, state);

    private static class Params
    {
        public const string IsWalking = nameof(IsWalking);
    }
}
