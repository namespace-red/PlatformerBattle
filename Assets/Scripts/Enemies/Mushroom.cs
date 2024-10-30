using UnityEngine;

[RequireComponent(typeof(HorizontalMoverByPoints))]
[RequireComponent(typeof(HorizontalRotater2D))]
public class Mushroom : MonoBehaviour
{
    private HorizontalMoverByPoints _mover;
    private HorizontalRotater2D _rotater;

    private void Awake()
    {
        _mover = GetComponent<HorizontalMoverByPoints>();
        _rotater = GetComponent<HorizontalRotater2D>();
    }

    private void FixedUpdate()
    {
        _rotater.Rotate(_mover.HorizontalDirection);
    }
}
