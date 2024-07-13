using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public Transform respawnPoint;
    public CameraShake cameraShake;
    public float intensity;
    public float duration;

    void OnTriggerEnter2D(Collider2D other)
    {
        cameraShake.SmoothShake(intensity, duration);
        
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform;
        }

        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            transform.position = respawnPoint.position;
        }
    }
}
