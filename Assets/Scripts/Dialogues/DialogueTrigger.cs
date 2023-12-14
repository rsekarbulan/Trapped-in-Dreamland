using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private MoonDialogueManager dialogueManager;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            dialogueManager.TriggerStartDialogue();
            triggered = true;
        }
    }

}
