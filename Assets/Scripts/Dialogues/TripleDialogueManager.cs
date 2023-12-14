using System.Collections;
using TMPro;
using UnityEngine;

public class TripleDialogueManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpeechBubble;
    [SerializeField] private SpriteRenderer moonSpeechBubble;
    [SerializeField] private SpriteRenderer lightSpeechBubble;

    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue TMP text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI moonDialogueText;
    [SerializeField] private TextMeshProUGUI lightDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;
    [SerializeField] private GameObject moonContinueButton;
    [SerializeField] private GameObject lightContinueButton;

    [Header("Animation Controllers")]
    [SerializeField] private Animator playerSpeechBubbleAnimator;
    [SerializeField] private Animator moonSpeechBubbleAnimator;
    [SerializeField] private Animator lightSpeechBubbleAnimator;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    [TextArea]
    [SerializeField] private string[] moonDialogueSentences;
    [TextArea]
    [SerializeField] private string[] lightDialogueSentences;

    private bool dialogueStarted;

    private int playerIndex;
    private int moonIndex;
    private int lightIndex;

    private bool moonSpeaking;
    private bool lightSpeaking;

    public bool dialogueFinished = false;

    private float speechBubbleAnimationDelay = 0.6f;

    private MCMovement playerMovementScript;

    void Start()
    {
        playerMovementScript = FindObjectOfType<MCMovement>();

        TriggerStartDialogue();
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
                if (moonSpeaking)
                {
                    TriggerContinuemoonDialogue();
                }
                else if (lightSpeaking)
                {
                    TriggerContinuelightDialogue();
                }
            }
        }

        if (moonContinueButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TriggerContinuePlayerDialogue();
            }
        }

        if (lightContinueButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TriggerContinuePlayerDialogue();
            }
        }
    }

    /*private IEnumerator StartDialogue()
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
            if (Random.Range(0, 2) == 0) // Randomly decide which NPC speaks first
            {
                moonSpeaking = true;
                moonSpeechBubbleAnimator.SetTrigger("Open");

                yield return new WaitForSeconds(speechBubbleAnimationDelay);
                StartCoroutine(TypemoonDialogue());
            }
            else
            {
                lightSpeaking = true;
                lightSpeechBubbleAnimator.SetTrigger("Open");

                yield return new WaitForSeconds(speechBubbleAnimationDelay);
                StartCoroutine(TypelightDialogue());
            }
        }
    }*/

    private IEnumerator StartDialogue()
    {
        playerMovementScript.ToggleInteraction();

        // Player speaks first
        playerSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypePlayerDialogue());

        // moon speaks second
        yield return new WaitForSeconds(typingSpeed + 1f); // Add a delay between player and moon speeches
        moonSpeaking = true;
        moonSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypemoonDialogue());

        // light speaks third
        yield return new WaitForSeconds(typingSpeed + 1f); // Add a delay between moon and light speeches
        moonSpeaking = false;
        lightSpeaking = true;
        lightSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypelightDialogue());
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

    private IEnumerator TypemoonDialogue()
    {
        foreach (char letter in moonDialogueSentences[moonIndex].ToCharArray())
        {
            moonDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        moonContinueButton.SetActive(true);
    }

    private IEnumerator TypelightDialogue()
    {
        foreach (char letter in lightDialogueSentences[lightIndex].ToCharArray())
        {
            lightDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        lightContinueButton.SetActive(true);
    }

    private IEnumerator ContinuePlayerDialogue()
    {
        playerContinueButton.SetActive(false);

        if (moonSpeaking)
        {
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

            StartCoroutine(TypePlayerDialogue());
        }
        else if (lightSpeaking)
        {
            lightDialogueText.text = string.Empty;
            lightSpeechBubbleAnimator.SetTrigger("Close");

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

            StartCoroutine(TypePlayerDialogue());
        }
    }

    private IEnumerator ContinuemoonDialogue()
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

        StartCoroutine(TypemoonDialogue());

        moonSpeaking = false;
        lightSpeaking = true;
        lightSpeechBubbleAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(speechBubbleAnimationDelay);
        StartCoroutine(TypelightDialogue());

    }

    private IEnumerator ContinuelightDialogue()
    {
        lightContinueButton.SetActive(false);
        playerDialogueText.text = string.Empty;

        playerSpeechBubbleAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        lightDialogueText.text = string.Empty;

        lightSpeechBubbleAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        if (dialogueStarted)
        {
            lightIndex++;
        }
        else
        {
            dialogueStarted = true;
        }

        StartCoroutine(TypelightDialogue());
    }

    public void TriggerContinuePlayerDialogue()
    {
        if (playerIndex >= playerDialogueSentences.Length - 1)
        {
            playerDialogueText.text = string.Empty;

            playerSpeechBubbleAnimator.SetTrigger("Close");

            playerMovementScript.ToggleInteraction();

            dialogueFinished = true;
        }
        else
        {
            StartCoroutine(ContinuePlayerDialogue());
        }
    }

    public void TriggerContinuemoonDialogue()
    {
        moonContinueButton.SetActive(false);

        if (moonIndex >= moonDialogueSentences.Length - 1)
        {
            moonDialogueText.text = string.Empty;

            moonSpeechBubbleAnimator.SetTrigger("Close");

            dialogueFinished = true;
        }
        else
        {
            StartCoroutine(ContinuemoonDialogue());
        }
    }

    public void TriggerContinuelightDialogue()
    {
        lightContinueButton.SetActive(false);

        if (lightIndex >= lightDialogueSentences.Length - 1)
        {
            lightDialogueText.text = string.Empty;

            lightSpeechBubbleAnimator.SetTrigger("Close");

            dialogueFinished = true;
        }
        else
        {
            StartCoroutine(ContinuelightDialogue());
        }
    }
}
