using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover2D : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    public Rigidbody2D Rigidbody2D { get; protected set; }
    
    protected void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Vector2 step = direction * (_speed * Time.fixedDeltaTime);
        Rigidbody2D.MovePosition(Rigidbody2D.position + step);
    }
}
