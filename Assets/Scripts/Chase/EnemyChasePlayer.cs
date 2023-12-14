using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChasePlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    private Transform player;
    public LayerMask obstacleLayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            /*transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);*/
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, player.position.x, speed * Time.deltaTime), transform.position.y);
        }

        if(transform.position == player.position)
        {
            SceneManager.LoadScene("Chase Scene");
        }

    }

    private void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}


