using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1f;
    public float waitTime = 0.5f;
    public bool loop = true;
    public Vector3[] waypoints;

    private int currentWaypointIndex = 0;
    private float lastWaypointSwitchTime;

    void Start()
    {
        if (waypoints.Length > 0) transform.position = waypoints[0];
        lastWaypointSwitchTime = Time.time;
    }

    void FixedUpdate()
    {
        if (waypoints.Length <= 1) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

        if (transform.position == targetPosition)
        {
            if (Time.time - lastWaypointSwitchTime < waitTime) return;

            currentWaypointIndex++;
            lastWaypointSwitchTime = Time.time;

            if (currentWaypointIndex >= waypoints.Length)
            {
                if (loop) currentWaypointIndex = 0;
                else return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.SetParent(null);
    }
}
