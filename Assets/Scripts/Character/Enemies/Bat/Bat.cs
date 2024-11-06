using System;
using UnityEngine;

[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(TargetPursuer))]
[RequireComponent(typeof(HorizontalRotater2D))]
[RequireComponent(typeof(EnemyCollisionDetector))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Health))]

public class Bat : MonoBehaviour
{
    [SerializeField] private BatAnimationsController _animationsController;
    [SerializeField] private float _playerDetectorRadius = 5f;

    private Patrol _patrol;
    private TargetPursuer _targetPursuer;
    private HorizontalRotater2D _rotater;
    private Attacker _attacker;
    private StateMachine _stateMachine;
    
    public Health Health { get; private set; }

    private void Start()
    {
        if (_animationsController == null)
            throw new NullReferenceException(nameof(_animationsController));
        
        _patrol = GetComponent<Patrol>();
        _targetPursuer = GetComponent<TargetPursuer>();
        _rotater = GetComponent<HorizontalRotater2D>();
        _attacker = GetComponent<Attacker>();
        Health = GetComponent<Health>();
        
        var idleState = new IdleState(_animationsController);
        var patrolState = new PatrolState(_animationsController, _patrol, _rotater);
        var targetPursuerState = new PlayerPursuerState(_animationsController, _targetPursuer, _rotater, _playerDetectorRadius);
        
        var emptyTransitionConditions = new EmptyTransitionConditions();
        var playerDetectorTransitionConditions = new PlayerDetectorTransitionConditions(transform, _playerDetectorRadius);
        
        _stateMachine = new StateMachine();
        _stateMachine.AddTransition(idleState, patrolState, emptyTransitionConditions);
        _stateMachine.AddTransition(patrolState, targetPursuerState, playerDetectorTransitionConditions);
        _stateMachine.SetFirstState(idleState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _playerDetectorRadius);
    }
}
