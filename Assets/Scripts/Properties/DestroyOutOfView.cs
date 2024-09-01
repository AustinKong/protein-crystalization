using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    void OnBecameInvisible()
    {
        // Destroy the game object when it leaves the camera's view
        Destroy(gameObject);
    }
}
