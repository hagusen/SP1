using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellfire : MonoBehaviour
{
    public float speed = 3f;
    public bool hellfire = true;

    SpriteRenderer _SpriteRenderer;

    private void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (hellfire)
        {
            position.y += speed * Time.deltaTime;
            _SpriteRenderer.color = Color.yellow;
        }
        else
        {
            _SpriteRenderer.color = Color.blue;
        }
        transform.localPosition = position;
	}
}
