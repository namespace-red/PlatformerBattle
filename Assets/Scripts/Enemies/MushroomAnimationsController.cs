using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MushroomAnimationsController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveSpeed(float speed) 
        => _animator.SetFloat(Params.Speed, speed);

    private static class Params
    {
        public const string Speed = nameof(Speed);
    }
}
