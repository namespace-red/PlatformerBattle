using System;
using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class Player : MonoBehaviour
{
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickable pickated))
        {
            switch (pickated)
            {
                case Money money:
                    _wallet.AddMoney(money.Value);
                    money.PickUp();
                    break;

                default:
                    throw new ArgumentException($"Not correct type {pickated}");
            }
        }
    }
}
