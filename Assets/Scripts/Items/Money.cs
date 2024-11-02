using UnityEngine;

public class Money : MonoBehaviour, IPickable
{
    [field:SerializeField] public int Value = 1;

    private void OnValidate()
    {
        if (Value < 0)
            Value = 0;
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }
}