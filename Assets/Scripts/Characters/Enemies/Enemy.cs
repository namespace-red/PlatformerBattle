using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Pusher Pusher;

    private void Awake()
    {
        Pusher = GetComponent<Pusher>();
    }
}
