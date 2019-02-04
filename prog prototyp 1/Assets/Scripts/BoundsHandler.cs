using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundsHandler : MonoBehaviour
{
    public bool allowCrossScreen = false;

    public CameraController cam; 
    private float radius;

    void Start ()
    {
        radius = GetComponent<BoxCollider2D>().size.x / 2; //ändra boxcollider till den aktiva collidern på spelarobjektet
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleBounds();
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

        if(transform.position.y < cam.boundary)
        {
            //GAME OVER
            SceneManager.LoadScene("main");
        }

        transform.localPosition = position;
    }
}
