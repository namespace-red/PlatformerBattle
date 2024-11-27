public class PlayerDetectorTransitionConditions : ITransitionCondition
{
    private PlayerDetector _playerDetector;

    public PlayerDetectorTransitionConditions(PlayerDetector playerDetector)
    {
        _playerDetector = playerDetector;
    }
    public bool IsDone()
    {
        return _playerDetector.Detect() != null;
    }
}
