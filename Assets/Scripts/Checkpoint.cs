using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Animator animator;

    private bool hasCollided = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasCollided)
        {
            other.GetComponent<KillPlayer>().respawnPoint = transform;
            animator.SetTrigger("CheckpointReached");
            hasCollided = true;
        }
    }
}
