using System.Collections;
using TMPro;
using UnityEngine;

public class MoonLightDialogueManager : MonoBehaviour
{
    [Header("Dialogue Components")]
    [SerializeField] private SpriteRenderer playerSpeechBubble;
    [SerializeField] private SpriteRenderer moonSpeechBubble;
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI moonDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;
    [SerializeField] private GameObject moonContinueButton;

    [Header("Animation Controllers")]
    [SerializeField] private Animator playerSpeechBubbleAnimator;
    [SerializeField] private Animator moonSpeechBubbleAnimator;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    [TextArea]
    [SerializeField] private string[] moonDialogueSentences;

    private bool npcDialoguesFinished = false;
    private bool dialogueFinished = false;

    private float typingSpeed = 0.05f;
    private float speechBubbleAnimationDelay = 0.6f;

    private MCMovement playerMovementScript;

    void Start()
    {
        playerMovementScript = FindObjectOfType<MCMovement>();
    }

    private void Update()
    {
        if (!dialogueFinished)
        {
            if (playerContinueButton.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (!npcDialoguesFinished)
                    {
                        StartCoroutine(ContinuePlayerDialogue());
                    }
                    else
                    {
                        EndDialogue();
                    }
                }
            }

            if (moonContinueButton.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (!npcDialoguesFinished)
                    {
                        StartCoroutine(ContinueMoonDialogue());
                    }
                    else
                    {
                        EndDialogue();
                    }
                }
            }
        }
    }

    public void TriggerStartDialogue()
    {
        StartCoroutine(StartDialogue());
        playerMovementScript.NotRun();
    }

    private IEnumerator StartDialogue()
    {
        playerMovementScript.ToggleInteraction();

        playerSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypePlayerDialogue());
    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialogueSentences[0].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerContinueButton.SetActive(true);
    }

    private IEnumerator ContinuePlayerDialogue()
    {
        playerContinueButton.SetActive(false);
        moonDialogueText.text = string.Empty;

        moonSpeechBubbleAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        playerDialogueText.text = string.Empty;
        playerSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        // Player's dialogues finished, trigger NPC's dialogues
        StartCoroutine(StartMoonDialogue());
    }

    private IEnumerator StartMoonDialogue()
    {
        moonSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypeMoonDialogue());
    }

    private IEnumerator TypeMoonDialogue()
    {
        foreach (char letter in moonDialogueSentences[0].ToCharArray())
        {
            moonDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        moonContinueButton.SetActive(true);
    }

    private IEnumerator ContinueMoonDialogue()
    {
        moonContinueButton.SetActive(false);
        playerDialogueText.text = string.Empty;

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        moonDialogueText.text = string.Empty;
        moonSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        // Set the flag to indicate NPC's dialogues finished
        npcDialoguesFinished = true;

        EndDialogue();
    }

    private void EndDialogue()
    {
        // Clean up, toggle interaction, or perform any other actions when the dialogue is finished
        playerMovementScript.ToggleInteraction();
        dialogueFinished = true;

        // Close both player and NPC speech bubbles and clear text
        playerSpeechBubbleAnimator.SetTrigger("Close");
        moonSpeechBubbleAnimator.SetTrigger("Close");

        playerDialogueText.text = string.Empty;
        moonDialogueText.text = string.Empty;
    }
}
