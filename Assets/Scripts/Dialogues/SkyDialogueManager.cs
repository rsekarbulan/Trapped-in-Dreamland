using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkyDialogueManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpeechBubble;
    [SerializeField] private SpriteRenderer skySpeechBubble;

    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue TMP text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI skyDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;
    [SerializeField] private GameObject skyContinueButton;

    [Header("Animation Controllers")]
    [SerializeField] private Animator playerSpeechBubbleAnimator;
    [SerializeField] private Animator skySpeechBubbleAnimator;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    [TextArea]
    [SerializeField] private string[] skyDialogueSentences;

    private bool dialogueStarted;

    private int playerIndex;
    private int skyIndex;
    public bool dialogueFinished = false;

    private float speechBubbleAnimationDelay = 0.6f;

    private MCMovement playerMovementScript;
    private SkyDialogueTrigger skyTrigger;

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
                    TriggerContinueskyDialogue();
                }
            }

            if (skyContinueButton.activeSelf)
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
            skySpeechBubbleAnimator.SetTrigger("Open");

            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypeskyDialogue());
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

    private IEnumerator TypeskyDialogue()
    {
        foreach (char letter in skyDialogueSentences[skyIndex].ToCharArray())
        {
            skyDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        skyContinueButton.SetActive(true);
    }

    private IEnumerator ContinuePlayerDialogue()
    {
        playerContinueButton.SetActive(false);
        skyDialogueText.text = string.Empty;

        skySpeechBubbleAnimator.SetTrigger("Close");

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

    private IEnumerator ContinueskyDialogue()
    {
        skyContinueButton.SetActive(false);
        playerDialogueText.text = string.Empty;

        playerSpeechBubbleAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        skyDialogueText.text = string.Empty;

        skySpeechBubbleAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        if (dialogueStarted)
        {
            skyIndex++;
        }
        else
        {
            dialogueStarted = true;
        }

        /* skyIndex++;*/

        StartCoroutine(TypeskyDialogue());
    }

    public void TriggerContinuePlayerDialogue()
    {
        skyContinueButton.SetActive(false);
        if (playerIndex >= playerDialogueSentences.Length - 1)
        {
            skyDialogueText.text = string.Empty;

            skySpeechBubbleAnimator.SetTrigger("Close");

            playerMovementScript.ToggleInteraction();

            dialogueFinished = true;
        }
        else
        {
            StartCoroutine(ContinuePlayerDialogue());
        }

    }

    public void TriggerContinueskyDialogue()
    {
        playerContinueButton.SetActive(false);
        if (skyIndex >= skyDialogueSentences.Length - 1)
        {
            playerDialogueText.text = string.Empty;

            playerSpeechBubbleAnimator.SetTrigger("Close");

            playerMovementScript.ToggleInteraction();

            dialogueFinished = true;
        }
        else
        {
            StartCoroutine(ContinueskyDialogue());
        }
    }

}