using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOrb : MonoBehaviour
{
    [Header("Orbs Check")]
    public bool redOrb;
    public bool greenOrb;
    public bool blueOrb;
    public bool purpleOrb;

    [Header("Light")]
    public bool drawWhiteLight;
    public GameObject whiteLight;


    public LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lightSpeed;

    [Header("Door")]
    public Doors door;

    void Start()
    {
        lineRenderer = lineRenderer.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);

        dist = Vector3.Distance(origin.position, destination.position);
    }

    void Update()
    {
        if (redOrb && greenOrb && blueOrb && purpleOrb)
        {
            StartCoroutine(ShootLight());
        }
    }

    IEnumerator ShootLight()
    {
        yield return new WaitForSeconds(1);

        drawWhiteLight = true;

        door.locked = false;
        door.puzzleComplete = true;

        if(drawWhiteLight)
        {
            whiteLight.SetActive(true);

            if (counter < dist)
            {
                counter += 0.1f / lightSpeed;

                float x = Mathf.Lerp(0, dist, counter);

                Vector3 pointA = origin.position;
                Vector3 pointB = destination.position;

                Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

                lineRenderer.SetPosition(1, pointAlongLine);
            }
        }
        else
        {
            whiteLight.SetActive(false);
        }
    }
}
