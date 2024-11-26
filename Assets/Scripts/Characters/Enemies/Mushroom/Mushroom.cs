using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsPatrol))]
[RequireComponent(typeof(HorizontalRotater2D))]
[RequireComponent(typeof(EnemyCollisionDetector))]
[RequireComponent(typeof(Attacker))]
public class Mushroom : Enemy
{
    [SerializeField] private MushroomAnimationsController _animationsController;

    private PhysicsPatrol _patrol;
    private HorizontalRotater2D _rotater;
    private StateMachine _stateMachine;

    protected override void Awake()
    {
        if (_animationsController == null)
            throw new NullReferenceException(nameof(_animationsController));
        
        base.Awake();
        
        _patrol = GetComponent<PhysicsPatrol>();
        _rotater = GetComponent<HorizontalRotater2D>();
    }
    
    private void Start()
    {
        Health.ValueDecreased += OnHealthDecreased;

        InitStateMachine();
    }

    private void OnEnable()
    {
        if (Health != null)
            Health.ValueDecreased += OnHealthDecreased;
    }


    private void OnDisable()
    {
        Health.ValueDecreased -= OnHealthDecreased;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void InitStateMachine()
    {
        var idleState = new IdleState();
        var patrolState = new PhysicsPatrolState(_animationsController, _patrol, _rotater);
        var deathState = new DeathState(GetComponent<Collider2D>(), _animationsController);
        
        var emptyTransitionConditions = new EmptyTransitionConditions();
        var deathTransitionConditions = new DeathTransitionConditions(Health);
        
        _stateMachine = new StateMachine();
        _stateMachine.AddTransition(idleState, patrolState, emptyTransitionConditions);
        _stateMachine.AddTransitionFromAnyStates(deathState, deathTransitionConditions);
        _stateMachine.SetFirstState(idleState);
    }

    private void OnHealthDecreased()
    {
        _animationsController.PlayTakeHit();
    }
}
