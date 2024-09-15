using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{
    public Transform enemy;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.transform.parent = null;
        }
    }
}
