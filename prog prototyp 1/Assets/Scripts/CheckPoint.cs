using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    SpriteRenderer sprite;
    private bool activated;
    
    public CheckPointManager cpManager;
    public Hellfire hellfire;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (sprite)
        {
            if (!activated)
            {
                sprite.color = Color.blue;
            }
            else
            {
                sprite.color = Color.green;
            }
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activated = true;
            cpManager.lastCheckpointPosition = transform.position;
            cpManager.hellfirePosition = hellfire.transform.position;
        }
    }
}
