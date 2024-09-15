using UnityEngine;

public class Appear : MonoBehaviour
{
    public GameObject objectToShow;
    public bool shown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToShow.SetActive(!shown);
        }
    }
}
