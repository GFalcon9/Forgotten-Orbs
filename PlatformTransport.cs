using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTransport : MonoBehaviour
{
    public bool hasPlayer;
    public bool isActive;
    public bool respawn;

    [Header("Movement")]
    public float speed = 2f;
    public Transform pathHolder;
    public GameObject player;
    public GameObject platform;

    [Header("Type")]
    public SwitchCheck switchCheck;
    public bool finalRoom;
    public bool purpleRoom;

    [Header("Material")]
    public Material emissionMaterial;
    public Material normalMaterial;
    Renderer objectRenderer;

    Vector3[] waypoints;
    int targetWaypointIndex;
    Vector3 targetWaypoint;

    void Start()
    {
        //---Waypoints Stuff---
        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y, waypoints[i].z);
        }

        transform.position = waypoints[0];
        targetWaypointIndex = 1;
        targetWaypoint = waypoints[targetWaypointIndex];


        //---Materials---
        objectRenderer = GetComponentInChildren<Renderer>();
        objectRenderer.enabled = true;
        objectRenderer.sharedMaterial = normalMaterial;
    }

    void Update()
    {
        if (purpleRoom)
        {
            if (switchCheck.activatePlatform)
            {
                isActive = true;

                if (hasPlayer)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

                    if (transform.position == targetWaypoint)
                    {
                        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                        targetWaypoint = waypoints[targetWaypointIndex];
                    }

                    objectRenderer.sharedMaterial = emissionMaterial;
                }
                else
                {
                    objectRenderer.sharedMaterial = normalMaterial;
                }
            }

            else
            {
                isActive = false;
                targetWaypointIndex = 1;
                targetWaypoint = waypoints[targetWaypointIndex];
                transform.position = Vector3.MoveTowards(transform.position, waypoints[0], speed * Time.deltaTime);

                objectRenderer.sharedMaterial = normalMaterial;
            }
        }

        if (finalRoom)
        {
            if(!respawn)
            {
                isActive = true;
                if (isActive)
                {
                    if (hasPlayer)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

                        if (transform.position == targetWaypoint)
                        {
                            targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                            targetWaypoint = waypoints[targetWaypointIndex];
                        }

                        objectRenderer.sharedMaterial = emissionMaterial;
                    }
                    else
                    {
                        objectRenderer.sharedMaterial = normalMaterial;
                    }
                }
            }
            else
            {
                isActive = false;
                targetWaypointIndex = 1;
                targetWaypoint = waypoints[targetWaypointIndex];
                transform.position = Vector3.MoveTowards(transform.position, waypoints[0], speed * Time.deltaTime);

                objectRenderer.sharedMaterial = normalMaterial;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.parent = platform.transform;
            hasPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        player.transform.parent = null;
        hasPlayer = false;
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
