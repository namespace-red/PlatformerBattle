using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWithCoolDownView : MonoBehaviour
{
    [SerializeField] protected AbilityWithCoolDown Ability;
    [SerializeField] private ImageFiller _abilityImageFiller;
    [SerializeField] private ImageFiller _coolDownImageFiller;
    [SerializeField] private Button _button;

    protected virtual void Awake()
    {
        if (_abilityImageFiller == null)
            throw new NullReferenceException(nameof(_abilityImageFiller));
        
        if (_coolDownImageFiller == null)
            throw new NullReferenceException(nameof(_coolDownImageFiller));
        
        if (_button == null)
            throw new NullReferenceException(nameof(_button));
        
        if (Ability == null)
            throw new NullReferenceException(nameof(Ability));
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
        Ability.Ran += OnRan;
        Ability.Finished += OnFinished;
        Ability.CooledDown += OnCooledDown;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
        Ability.Ran -= OnRan;
        Ability.Finished -= OnFinished;
        Ability.CooledDown -= OnCooledDown;
    }

    protected virtual void OnRan()
    {
        _abilityImageFiller.StartFilling(Ability.AbilitySec);
    }

    protected virtual void OnFinished()
    {
        _coolDownImageFiller.StartFilling(Ability.CoolDownSec);
    }

    protected virtual void OnCooledDown()
    {
        _coolDownImageFiller.ResetFill();
        _abilityImageFiller.ResetFill();
    }

    private void OnButtonClicked()
    {
        Ability.TryRun();
    }
}
