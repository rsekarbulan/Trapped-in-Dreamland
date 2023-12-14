using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector2 posSekarang;

    private void Start() {
        posSekarang = transform.position;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            transform.position = posSekarang;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Checkpoint")) {
            posSekarang = transform.position;

            Debug.Log(transform.position);
        }
    }
}