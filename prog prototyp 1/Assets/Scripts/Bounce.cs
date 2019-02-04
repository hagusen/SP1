using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public float bounceForce = 8f;

    public Rigidbody2D playerRigidbody;

    BoxCollider2D col;
    public BoxCollider2D playerCol;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(playerRigidbody.velocity.y <= 0)
            {
                playerRigidbody.velocity = Vector3.up * bounceForce;
            }          
        }
    }
}
