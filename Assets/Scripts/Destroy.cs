using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject objectToDestroy;
    public bool shown = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToDestroy.SetActive(!shown);
        }
    }
}
