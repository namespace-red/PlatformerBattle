using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EntityLook : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(float direction)
    {
        if (IsNeedFlip(direction))
            FlipLook();
    }

    private bool IsNeedFlip(float direction)
    {
        return (direction < 0) && (_renderer.flipX == false) 
               || (direction > 0) && _renderer.flipX;
    }
    
    private void FlipLook()
    {
        _renderer.flipX = !_renderer.flipX;
    }
}
