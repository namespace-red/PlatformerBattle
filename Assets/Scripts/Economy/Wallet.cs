using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [field:SerializeField] public int Value;

    public void AddMoney(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();
        
        Value += value;
    }
}
