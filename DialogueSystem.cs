using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueTrigger;
    public GameObject textBox;
    public TextMeshProUGUI dialogueText;
    public PauseMenu pause;
    public GameObject hint;
    [Space]
    public bool tutorial;
    [Space]
    [TextArea(1, 3)]
    public string[] texts;

    private int index;

    bool textActive;
    bool endDialogue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!tutorial)
            {
                textBox.SetActive(true);
                index = 0;
                dialogueText.text = texts[index];
                textActive = true;
            }
            if(tutorial)
            {
                hint.SetActive(true);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (tutorial && Input.GetKeyUp(KeyCode.Q))
            {
                textBox.SetActive(true);
                index = 0;
                dialogueText.text = texts[index];
                textActive = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            textBox.SetActive(false);
            if(tutorial)
            {
                hint.SetActive(false);
            }
        }
    }

    void Update()
    {
        if(textActive)
        {
            if (!pause.menuOpen)
            {
                if (Input.GetKeyUp(KeyCode.E) && textBox.activeSelf)
                {
                    if (++index < texts.Length)
                    {
                        dialogueText.text = texts[index];
                    }
                    else
                    {
                        if (index == texts.Length)
                        {
                            textBox.SetActive(false);
                            endDialogue = true;
                            textActive = false;
                        }
                    }
                }
            }
        }

        if(endDialogue)
        {
            if(!tutorial)
            {
                dialogueTrigger.SetActive(false);
            }
        }
    }
}
