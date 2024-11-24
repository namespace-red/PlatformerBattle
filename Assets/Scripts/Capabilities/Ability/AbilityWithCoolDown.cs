using System;
using System.Collections;
using UnityEngine;

public abstract class AbilityWithCoolDown : MonoBehaviour
{
    [field:SerializeField, Min(0)] public float AbilitySec { get; private set; }
    [field:SerializeField, Min(0)] public float CoolDownSec { get; private set; }

    private float _abilityTimer;
    
    public bool CanRun { get; set; } = true;
    
    public event Action Ran;
    public event Action Finished;
    public event Action CooledDown;
    
    public bool TryRun()
    {
        if (CanRun == false)
            return false;

        CanRun = false;
        
        StartCoroutine(Run());
        
        return true;
    }

    protected abstract void AbilityLogic();
    
    private IEnumerator Run()
    {
        Ran?.Invoke();

        do
        {
            AbilityLogic();
            yield return null;
            _abilityTimer += Time.deltaTime;
        } while (_abilityTimer < AbilitySec);

        Finished?.Invoke();
        yield return new WaitForSeconds(CoolDownSec);
        
        _abilityTimer = 0;
        CanRun = true;
        CooledDown?.Invoke();
    }
}
