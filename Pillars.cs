using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pillars : MonoBehaviour
{
    public Inventory player;
    public UseItems inventory;
    public GameObject orb;
    public BigOrb[] bigOrb;

    public bool orbActive;

    [Header("Text")]
    [SerializeField] GameObject textSpace;
    [SerializeField] TextMeshProUGUI interactableText;

    [Header("Colour")]
    public bool redOrb;
    public bool greenOrb;
    public bool blueOrb;
    public bool purpleOrb;

    [Header("Pedestal Material")]
    public Renderer pedestalRenderer;
    [Space]
    public Material emissionMaterial;
    public Material normalMaterial;

    [Header("Light")]
    public bool drawLight;

    public LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lightSpeed;

    [Header("Animation")]
    [SerializeField] Animator pedestalAnim;

    void Start()
    {
        orbActive = false;

        lineRenderer = lineRenderer.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);

        dist = Vector3.Distance(origin.position, destination.position);

        pedestalAnim = pedestalAnim.GetComponent<Animator>();

        pedestalRenderer = pedestalRenderer.GetComponent<Renderer>();
        pedestalRenderer.enabled = true;
        pedestalRenderer.sharedMaterial = normalMaterial;
    }

    void Update()
    {
        if (drawLight)
        {
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!orbActive)
            {
                if (inventory.redOrbEquiped || inventory.greenOrbEquiped || inventory.blueOrbEquiped || inventory.purpleOrbEquiped)
                {
                    textSpace.SetActive(true);
                    interactableText.text = "[E] Use Orb";
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                textSpace.SetActive(false);

                if (!orbActive)
                {
                    if (redOrb)
                    {
                        if (inventory.redOrbEquiped)
                        {
                            inventory.redOrbEquiped = false;
                            inventory.redOrb.SetActive(false);
                            inventory.itemEquiped = false;
                            inventory.canEquipItem = true;

                            player.hasRedOrb = false;
                            orb.SetActive(true);
                            drawLight = true;
                            bigOrb[0].redOrb = true;
                            bigOrb[1].redOrb = true;

                            orbActive = true;

                            pedestalRenderer.sharedMaterial = emissionMaterial;

                            pedestalAnim.SetBool("IsActivated", true);
                        }

                        if (inventory.greenOrbEquiped || inventory.blueOrbEquiped || inventory.purpleOrbEquiped)
                        {
                            textSpace.SetActive(true);
                            interactableText.text = "Incorrect Orb";
                        }
                    }

                    if (greenOrb)
                    {
                        if (inventory.greenOrbEquiped)
                        {
                            inventory.greenOrbEquiped = false;
                            inventory.greenOrb.SetActive(false);
                            inventory.itemEquiped = false;
                            inventory.canEquipItem = true;

                            player.hasGreenOrb = false;
                            orb.SetActive(true);
                            drawLight = true;
                            bigOrb[0].greenOrb = true;
                            bigOrb[1].greenOrb = true;

                            orbActive = true;

                            pedestalRenderer.sharedMaterial = emissionMaterial;

                            pedestalAnim.SetBool("IsActivated", true);
                        }

                        if (inventory.redOrbEquiped || inventory.blueOrbEquiped || inventory.purpleOrbEquiped)
                        {
                            textSpace.SetActive(true);
                            interactableText.text = "Incorrect Orb";
                        }
                    }

                    if (blueOrb)
                    {
                        if (inventory.blueOrbEquiped && !orbActive)
                        {
                            inventory.blueOrbEquiped = false;
                            inventory.blueOrb.SetActive(false);
                            inventory.itemEquiped = false;
                            inventory.canEquipItem = true;

                            player.hasBlueOrb = false;
                            orb.SetActive(true);
                            drawLight = true;
                            bigOrb[0].blueOrb = true;
                            bigOrb[1].blueOrb = true;

                            orbActive = true;

                            pedestalRenderer.sharedMaterial = emissionMaterial;

                            pedestalAnim.SetBool("IsActivated", true);
                        }

                        if (inventory.redOrbEquiped || inventory.greenOrbEquiped || inventory.purpleOrbEquiped)
                        {
                            textSpace.SetActive(true);
                            interactableText.text = "Incorrect Orb";
                        }
                    }

                    if (purpleOrb)
                    {
                        if (inventory.purpleOrbEquiped && !orbActive)
                        {
                            inventory.purpleOrbEquiped = false;
                            inventory.purpleOrb.SetActive(false);
                            inventory.itemEquiped = false;
                            inventory.canEquipItem = true;

                            player.hasPurpleOrb = false;
                            orb.SetActive(true);
                            drawLight = true;
                            bigOrb[0].purpleOrb = true;
                            bigOrb[1].purpleOrb = true;

                            orbActive = true;

                            pedestalRenderer.sharedMaterial = emissionMaterial;

                            pedestalAnim.SetBool("IsActivated", true);
                        }

                        if (inventory.redOrbEquiped || inventory.greenOrbEquiped || inventory.blueOrbEquiped)
                        {
                            textSpace.SetActive(true);
                            interactableText.text = "Incorrect Orb";
                        }
                    }
                }
            }

            if (orbActive)
            {
                textSpace.SetActive(true);
                interactableText.text = "[E] Remove Orb";
            }

            //---Revome Orbs---
            if (Input.GetKeyDown(KeyCode.E))
            {
                textSpace.SetActive(false);

                if (orbActive)
                {
                    if (redOrb)
                    {
                        if (!player.hasRedOrb)
                        {
                            player.hasRedOrb = true;
                            orb.SetActive(false);

                            drawLight = false;

                            bigOrb[0].redOrb = false;
                            bigOrb[1].redOrb = false;

                            orbActive = false;

                            pedestalRenderer.sharedMaterial = normalMaterial;

                            pedestalAnim.SetBool("IsActivated", false);
                        }
                    }

                    if (greenOrb)
                    {
                        if (!player.hasGreenOrb)
                        {
                            player.hasGreenOrb = true;
                            orb.SetActive(false);

                            drawLight = false;

                            bigOrb[0].greenOrb = false;
                            bigOrb[1].greenOrb = false;

                            orbActive = false;

                            pedestalRenderer.sharedMaterial = normalMaterial;

                            pedestalAnim.SetBool("IsActivated", false);
                        }
                    }

                    if (blueOrb)
                    {
                        if (!player.hasBlueOrb)
                        {
                            player.hasBlueOrb = true;
                            orb.SetActive(false);

                            drawLight = false;

                            bigOrb[0].blueOrb = false;
                            bigOrb[1].blueOrb = false;

                            orbActive = false;

                            pedestalRenderer.sharedMaterial = normalMaterial;

                            pedestalAnim.SetBool("IsActivated", false);
                        }
                    }

                    if (purpleOrb)
                    {
                        if (!player.hasPurpleOrb)
                        {
                            player.hasPurpleOrb = true;
                            orb.SetActive(false);

                            drawLight = false;

                            bigOrb[0].purpleOrb = false;
                            bigOrb[1].purpleOrb = false;

                            orbActive = false;

                            pedestalRenderer.sharedMaterial = normalMaterial;

                            pedestalAnim.SetBool("IsActivated", false);
                        }
                    }
                }
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        textSpace.SetActive(false);
    }
}
