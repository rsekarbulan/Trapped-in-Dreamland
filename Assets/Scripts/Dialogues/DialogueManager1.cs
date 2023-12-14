using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager1 : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpeechBubble;

    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue TMP text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;

    private bool dialogueStarted;

    private int playerIndex;

    public void startDialogue()
    {
        if (PlayerSpeakingFirst)
        {
            StartCoroutine(TypePlayerDialogue());
        }

    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach(char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerContinueButton.SetActive(true);
    }

    public void ContinuePlayerDialogue()
    {
        playerContinueButton.SetActive(false);

        if (playerIndex < playerDialogueSentences.Length - 1)
        {
            /*if (dialogueStarted)
            {
                playerIndex++;
            }
            else
            {
                dialogueStarted = true;
            }*/

            playerIndex++;

            playerDialogueText.text = string.Empty;
            StartCoroutine(TypePlayerDialogue());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
