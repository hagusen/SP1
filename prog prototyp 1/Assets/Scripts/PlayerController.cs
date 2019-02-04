﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 7f;
    public float jumpForce = 8f;
    [Space(10)]
    public bool allowFly;
    public float flyMultiplier = 1f;
    [Space(10)]
    public bool ignorePlatformCollision;
    public LayerMask groundLayer;
    private bool move_left, move_right, move_up, jump;

    public Text coinCountText;
    private int coinCount;

    public bool allowCrossScreen = false;
    public CameraController cam;
    private float radius;
  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        radius = GetComponent<BoxCollider2D>().size.x / 2; //ändra boxcollider till den aktiva collidern på spelarobjektet    
        SetCountText();
    }

    private void Update()
    {
        PlayerInput();
        Movement();
        HandleBounds();
        SetCountText();
    }
    void Movement()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y,transform.position.z);       

        if (move_left)
        {
            position.x -= speed * Time.deltaTime;
        }
        if (move_right)
        {
            position.x += speed * Time.deltaTime;
        }

        if (jump)
        {

            if (!IsGrounded())
            {
                return;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (allowFly)
        {
            if (move_up)
            {
                rb.isKinematic = true; //ta bort fysiken från rigidbody
                position.y += speed * flyMultiplier * Time.deltaTime;             
            }   
        }
        if (!allowFly)
        {
            rb.isKinematic = false;   
        }
        //slutgiltlig transformation
        transform.localPosition = position;
    }

    void PlayerInput()
    {
        bool input_left = Input.GetKey(KeyCode.A);
        bool input_right = Input.GetKey(KeyCode.D);
        bool input_up = Input.GetKey(KeyCode.W);
        bool input_jump = Input.GetKeyDown(KeyCode.Space);

        move_left = input_left;
        move_right = input_right;
        move_up = input_up;
        jump = input_jump;
    }

    bool IsGrounded()
    {
        Vector3 position = transform.position;
        Vector3 direction = Vector3.down;
        float distance = 1f;

        Debug.DrawRay(position, direction, Color.green);
        //raycast under spelaren som kollar om spelaren är grounded eller inte grounded
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ignorePlatformCollision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>(), true);
                
            }
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coinCount++;
        }

        if (collision.gameObject.CompareTag("Hellfire"))
        {
            //GAME OVER
            SceneManager.LoadScene("start");
            coinCount = 0;
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ignorePlatformCollision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>(), false);
            }
        }
    }

    void HandleBounds()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //Skickar spelaren till andra sidan skärmen om han hamnar utanför
        if (allowCrossScreen)
        {
            if (position.x > cam.width / 2)
            {
                position.x = -cam.width / 2;
            }
            if (position.x < -cam.width / 2)
            {
                position.x = cam.width / 2;
            }
        }
        else
        {
            if (position.x >= cam.width / 2 - radius)
            {
                position.x = cam.width / 2 - radius;
            }
            if (position.x <= -cam.width / 2 + radius)
            {
                position.x = -cam.width / 2 + radius;
            }
        }

        if (transform.position.y < cam.boundary)
        {
            //GAME OVER
            SceneManager.LoadScene("main");
        }

        transform.localPosition = position;
    }

    void SetCountText()
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
    }
}