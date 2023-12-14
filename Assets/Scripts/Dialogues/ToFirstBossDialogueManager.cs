using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToFirstBossDialogueManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpeechBubble;

    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue TMP text")]
    [SerializeField] private TextMeshProUGUI playerDialogueText;

    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;

    private bool dialogueStarted;

    private int playerIndex;

    private void startDialogue()
    {
        StartCoroutine(TypePlayerDialogue());
    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        StartCoroutine(ContinuePlayerDialogue());

        /* ContinuePlayerDialogue();*/
        /*playerContinueButton.SetActive(true);*/
    }

    public IEnumerator ContinuePlayerDialogue()
    {
        /*playerContinueButton.SetActive(false);*/

        if (playerIndex < playerDialogueSentences.Length - 1)
        {
            /* if (dialogueStarted)
             {
                 playerIndex++;
             }
             else
             {
                 dialogueStarted = true;
             }*/

            playerIndex++;

            yield return new WaitForSeconds(2);

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
