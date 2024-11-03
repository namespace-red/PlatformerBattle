using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationsController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayJump()
        => _animator.SetTrigger(State.Jump);
    
    public void PlayFall()
        => _animator.SetTrigger(State.Fall);
    
    public void OnGrounded()
        => _animator.SetTrigger(State.Grounded);
    
    public void SetRunState(bool state) 
        => _animator.SetBool(Params.IsRunning, state);
    
    private static class Params
    {
        public const string IsRunning = nameof(IsRunning);
    }
    
    private static class State
    {
        public const string Jump = nameof(Jump);
        public const string Fall = nameof(Fall);
        public const string Grounded = nameof(Grounded);
    }
}
