public class DeathTransitionConditions : ITransitionCondition
{
    private readonly Health _health;

    public DeathTransitionConditions(Health health)
    {
        _health = health;
    }

    public bool IsDone()
    {
        return _health.IsAlive == false;
    }
}
