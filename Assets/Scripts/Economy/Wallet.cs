using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _value;

    public int Value => _value;

    public void AddMoney(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();
        
        _value += value;
    }
}
