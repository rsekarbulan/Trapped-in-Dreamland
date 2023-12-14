using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDialogueTrigger : MonoBehaviour
{
    [SerializeField] private SkyDialogueManager dialogueManager;

    public bool triggered;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.TriggerStartDialogue();
            triggered = true;
        }
    }

}
