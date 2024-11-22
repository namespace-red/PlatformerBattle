using UnityEngine;

public class HorizontalRotater2D : MonoBehaviour
{
    public void Rotate(float horizontalDirection)
    {
        transform.right = new Vector3(horizontalDirection , 0 , 0);
    }
}
