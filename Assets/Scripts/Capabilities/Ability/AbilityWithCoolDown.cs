using System;
using System.Collections;
using System.Timers;
using UnityEngine;

public abstract class AbilityWithCoolDown : MonoBehaviour
{
    protected const int MsecInSec = 1000;
    
    [field:SerializeField, Min(0)] public float AbilitySec { get; private set; }
    [field:SerializeField, Min(0)] public float CoolDownSec { get; private set; }

    private Timer _timer = new Timer();
    
    public event Action Ran;
    public event Action Finished;
    public event Action CooledDown;

    public bool CanRun => _timer.Enabled == false;

    protected virtual void Awake()
    {
        _timer.AutoReset = false;
    }

    public bool TryRun()
    {
        if (CanRun == false)
            return false;

        StartCoroutine(Run());
        
        return true;
    }

    protected abstract void AbilityLogic();
    
    private IEnumerator Run()
    {
        Ran?.Invoke();
        yield return RunAbilityLogic();
        
        Finished?.Invoke();
        yield return RunCoolDownLogic();
        
        CooledDown?.Invoke();
    }

    private IEnumerator RunAbilityLogic()
    {
        _timer.Interval = AbilitySec * MsecInSec;
        _timer.Start();
        
        do
        {
            AbilityLogic();
            yield return null;
        } while (_timer.Enabled);
    }

    private IEnumerator RunCoolDownLogic()
    {
        _timer.Interval = CoolDownSec * MsecInSec;
        _timer.Start();
        yield return new WaitWhile(() => _timer.Enabled);
    }
}
