using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    public float slowdownFactor = 0.5f;
    public float slowdownDuration = 1.25f;
    public float particleDuration = 1.75f;
    public ParticleSystem particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CollectKey());
        }
    }

    private IEnumerator CollectKey()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        // make-shift hitstop
        Time.timeScale = slowdownFactor;

        door.SetActive(false);

        particles.Play();

        yield return new WaitForSecondsRealtime(slowdownDuration);

        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(particleDuration - slowdownDuration);

        particles.Stop();

        Destroy(gameObject);
    }
}