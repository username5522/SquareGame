using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public Transform respawnPoint;
    public CameraShake cameraShake;
    public float intensity;
    public float duration;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            cameraShake.SmoothShake(intensity, duration);
            respawnPoint = other.transform;
        }

        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            cameraShake.SmoothShake(intensity, duration);
            transform.position = respawnPoint.position;
        }
    }
}
