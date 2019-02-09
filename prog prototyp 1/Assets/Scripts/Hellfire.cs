using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellfire : MonoBehaviour
{
    public bool hellfire = true;

    public float speed = 3f;

    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);

        if (hellfire)
        {
            position.y += speed * Time.deltaTime;
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.cyan;
        }
        transform.localPosition = position;
	}
}
