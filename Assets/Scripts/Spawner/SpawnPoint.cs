using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool IsFree => Owner == null;
    public IPickable Owner { get; private set; }

    public void Occupy(IPickable owner)
    {
        Owner = owner;
        Owner.Picked += Release;
    }

    private void Release()
    {
        Owner.Picked -= Release;
        Owner = null;
    }
}
