using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SimpleBar : MonoBehaviour
{
    protected Slider Slider;

    protected virtual void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected void SetValue(float value)
        => Slider.value = value;
}
