using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    [SerializeField] public Transform target; // target transform
    private bool isFacingRight = false;
    private bool hasArrived;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private enum MovementState { idle, running, jumping }

    [SerializeField] public SkyDialogueManager dialogueManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        hasArrived = false;
    }

    private void FixedUpdate()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (dialogueManager.dialogueFinished)
        {
            // Calculate the direction towards the player only in the X-axis
            Vector2 directionToTarget = new Vector2(target.position.x - transform.position.x, 1f);

            // Move towards the player in the X-axis
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, target.position.x, moveSpeed * Time.deltaTime), transform.position.y);

            if (!hasArrived)
            {
                anim.SetInteger("state", 1);
            }

            if (directionToTarget.x > 0 && !isFacingRight)
            {
                Flip();
            }

            // Check if the NPC has arrived at the target location
            if (!hasArrived && Mathf.Approximately(transform.position.x, target.position.x))
            {
                Debug.Log("sky has arrived!");
                hasArrived = true;
                anim.SetInteger("state", 0);
            }
        }

    }

    private void Flip()
    {
        // Switch the direction the NPC is facing
        isFacingRight = !isFacingRight;

        // Flip the NPC sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
