using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsPatrol))]
[RequireComponent(typeof(HorizontalRotater2D))]
[RequireComponent(typeof(EnemyCollisionDetector))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Health))]
public class Mushroom : MonoBehaviour
{
    private const float SecUntilDeath = 2f;

    [SerializeField] private MushroomAnimationsController _animationsController;

    private PhysicsPatrol _patrol;
    private HorizontalRotater2D _rotater;
    private StateMachine _stateMachine;
    
    public Health Health { get; private set; }
    
    private void OnEnable()
    {
        if (Health != null)
            Health.ValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        Health.ValueChanged -= OnHealthChanged;
    }

    private void Start()
    {
        if (_animationsController == null)
            throw new NullReferenceException(nameof(_animationsController));
        
        _patrol = GetComponent<PhysicsPatrol>();
        _rotater = GetComponent<HorizontalRotater2D>();
        Health = GetComponent<Health>();
        
        Health.ValueChanged += OnHealthChanged;

        InitStateMachine();
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
        var deathState = new DeathState(GetComponent<Collider2D>(), _animationsController, SecUntilDeath);
        
        var emptyTransitionConditions = new EmptyTransitionConditions();
        var deathTransitionConditions = new DeathTransitionConditions(Health);
        
        _stateMachine = new StateMachine();
        _stateMachine.AddTransition(idleState, patrolState, emptyTransitionConditions);
        _stateMachine.AddTransitionFromAnyStates(deathState, deathTransitionConditions);
        _stateMachine.SetFirstState(idleState);
    }

    private void OnHealthChanged(float value, float maxValue)
    {
        _animationsController.PlayTakeHit();
    }
}
