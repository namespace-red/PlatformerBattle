using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] private float _force = 1000f;
    [SerializeField] private float _horizontalMultiplier = 3f;

    public void Push(Rigidbody2D rigidbody2D, Vector2 direction)
    {
        Vector2 velocity = new Vector2(direction.x * _force * _horizontalMultiplier, direction.y * _force);
        Debug.Log(velocity);
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(velocity, ForceMode2D.Force);
    }
}
