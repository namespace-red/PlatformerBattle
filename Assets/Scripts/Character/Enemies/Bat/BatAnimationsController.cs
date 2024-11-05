using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAnimationsController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetFlyState(bool state) 
        => _animator.SetBool(Params.IsFlying, state);

    private static class Params
    {
        public const string IsFlying = nameof(IsFlying);
    }
}
