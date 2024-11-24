using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWithCoolDownView : MonoBehaviour
{
    [SerializeField] private ImageFiller _activityImageFiller;
    [SerializeField] private ImageFiller _coolDownImageFiller;
    [SerializeField] private Button _button;
    [SerializeField] private AbilityWithCoolDown _ability;

    private void Awake()
    {
        if (_activityImageFiller == null)
            throw new NullReferenceException(nameof(_activityImageFiller));
        
        if (_coolDownImageFiller == null)
            throw new NullReferenceException(nameof(_coolDownImageFiller));
        
        if (_button == null)
            throw new NullReferenceException(nameof(_button));
        
        if (_ability == null)
            throw new NullReferenceException(nameof(_ability));
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
        _ability.Ran += OnRan;
        _ability.Finished += OnFinished;
        _ability.CooledDown += OnCooledDown;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
        _ability.Ran -= OnRan;
        _ability.Finished -= OnFinished;
        _ability.CooledDown -= OnCooledDown;
    }

    private void OnButtonClicked()
    {
        _ability.TryRun();
    }

    private void OnRan()
    {
        _activityImageFiller.StartFilling(_ability.AbilitySec);
    }
    
    private void OnFinished()
    {
        _coolDownImageFiller.StartFilling(_ability.CoolDownSec);
    }

    private void OnCooledDown()
    {
        _coolDownImageFiller.ResetFill();
        _activityImageFiller.ResetFill();
    }
}
