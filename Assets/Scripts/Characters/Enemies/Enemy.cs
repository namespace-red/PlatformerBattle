using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public Health Health { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
    }
}
