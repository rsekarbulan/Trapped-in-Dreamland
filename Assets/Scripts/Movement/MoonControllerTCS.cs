using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoonControllerTCS : MonoBehaviour
{
    public float moveSpeed = 1f;
    [SerializeField] public Transform target; // target transform
    private bool isFacingRight;
    private bool hasArrived;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private enum MovementState { idle, running, jumping }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        hasArrived = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check the name of the previous scene
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        // Run specific code based on the previous scene
        if (currentScene == 3)
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
                Debug.Log("Moon has arrived!");
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
