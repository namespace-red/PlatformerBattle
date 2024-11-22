using UnityEngine;

public class LookAtCamera2D : MonoBehaviour
{
    private Transform _target;

    private void Awake()
    {
        _target = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.rotation = _target.rotation;
    }
}
