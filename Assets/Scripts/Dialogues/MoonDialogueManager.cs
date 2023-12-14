using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoonDialogueManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpeechBubble;
    [SerializeField] private SpriteRenderer moonSpeechBubble;

    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue TMP text")]
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

    private bool dialogueStarted;

    private int playerIndex;
    private int moonIndex;
    public bool dialogueFinished = false;

    private float speechBubbleAnimationDelay = 0.6f;

    private MCMovement playerMovementScript;

    void Start()
    {
        playerMovementScript = FindObjectOfType<MCMovement>();
    }

    public void TriggerStartDialogue()
    {
        StartCoroutine(StartDialogue());
        playerMovementScript.NotRun();
    }

    private void Update()
    {
        if (playerContinueButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TriggerContinueMoonDialogue();
            }
        }

        if (moonContinueButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TriggerContinuePlayerDialogue();
            }
        }
    }

    private IEnumerator StartDialogue()
    {
        playerMovementScript.ToggleInteraction();

        if (PlayerSpeakingFirst)
        {
            playerSpeechBubbleAnimator.SetTrigger("Open");

            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypePlayerDialogue());
        }
        else
        {
            moonSpeechBubbleAnimator.SetTrigger("Open");

            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypeMoonDialogue());
        }
    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerContinueButton.SetActive(true);
    }

    private IEnumerator TypeMoonDialogue()
    {
        foreach (char letter in moonDialogueSentences[moonIndex].ToCharArray())
        {
            moonDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        moonContinueButton.SetActive(true);
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


        if (dialogueStarted)
        {
            playerIndex++;
        }
        else
        {
            dialogueStarted = true;
        }

        /*playerIndex++;*/

        StartCoroutine(TypePlayerDialogue());
    }

    private IEnumerator ContinueMoonDialogue()
    {
        moonContinueButton.SetActive(false);
        playerDialogueText.text = string.Empty;

        playerSpeechBubbleAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        moonDialogueText.text = string.Empty;

        moonSpeechBubbleAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        if (dialogueStarted)
        {
            moonIndex++;
        }
        else
        {
            dialogueStarted = true;
        }

        /* moonIndex++;*/

        StartCoroutine(TypeMoonDialogue());
    }

    public void TriggerContinuePlayerDialogue()
    {
        moonContinueButton.SetActive(false);
        if (playerIndex >= playerDialogueSentences.Length - 1)
        {
            moonDialogueText.text = string.Empty;

            moonSpeechBubbleAnimator.SetTrigger("Close");

            playerMovementScript.ToggleInteraction();

            dialogueFinished = true;
        }
        else
        {
            StartCoroutine(ContinuePlayerDialogue());
        }

    }

    public void TriggerContinueMoonDialogue()
    {
        playerContinueButton.SetActive(false);
        if (moonIndex >= moonDialogueSentences.Length - 1)
        {
            playerDialogueText.text = string.Empty;

            playerSpeechBubbleAnimator.SetTrigger("Close");

            playerMovementScript.ToggleInteraction();

            dialogueFinished = true;
        }
        else
        {
            StartCoroutine(ContinueMoonDialogue());
        }
    }

}