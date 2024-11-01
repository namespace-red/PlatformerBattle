using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CollisionDetector : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickable pickated))
        {
            switch (pickated)
            {
                case Money money:
                    _player.Wallet.AddMoney(money.Value);
                    money.PickUp();
                    break;

                default:
                    throw new ArgumentException($"Not correct type {pickated}");
            }
        }
    }
}
