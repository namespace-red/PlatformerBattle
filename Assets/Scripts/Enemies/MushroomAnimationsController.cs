using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MushroomAnimationsController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalkState(bool state) 
        => _animator.SetBool(Params.IsWalking, state);

    private static class Params
    {
        public const string IsWalking = nameof(IsWalking);
    }
}
