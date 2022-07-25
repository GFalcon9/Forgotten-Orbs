using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsMovement : MonoBehaviour
{
    public float speed = 3f;

    public Transform pathHolder; 

    public bool active;

    public bool switchActive;

    public bool wait;

    public GameObject character;
    public GameObject platform;

    Vector3[] waypoints;
    int targetWaypointIndex;
    Vector3 targetWaypoint;

    [Header("Material")]
    public Material emissionMaterial;
    public Material normalMaterial;
    Renderer objectRenderer;

    void Start()
    {
        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y, waypoints[i].z);
        }

        transform.position = waypoints[0];
        targetWaypointIndex = 1;
        targetWaypoint = waypoints[targetWaypointIndex];

        objectRenderer = transform.GetChild(0).GetComponent<Renderer>();
        objectRenderer.enabled = true;
        objectRenderer.sharedMaterial = normalMaterial;
    }

    void Update()
    {
        if (active || switchActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

            if (transform.position == targetWaypoint)
            {
                if(wait)
                {
                    StartCoroutine(WaitTime());
                }
                
                else
                {
                    targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                    targetWaypoint = waypoints[targetWaypointIndex];
                }
            }

            objectRenderer.sharedMaterial = emissionMaterial;
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0], speed * Time.deltaTime);

            objectRenderer.sharedMaterial = normalMaterial;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            character.transform.parent = platform.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        character.transform.parent = null;
    }

    IEnumerator WaitTime()
    {
        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;

        yield return new WaitForSeconds(2);

        targetWaypoint = waypoints[targetWaypointIndex];
    }

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

}
