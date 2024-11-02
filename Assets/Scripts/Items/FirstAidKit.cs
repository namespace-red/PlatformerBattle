using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FirstAidKit : MonoBehaviour, IPickable
{
    [field:SerializeField] public float HealthValue = 1;

    private void OnValidate()
    {
        if (HealthValue < 0)
            HealthValue = 0;
    }
    
    public void PickUp()
    {
        Destroy(gameObject);
    }
}
