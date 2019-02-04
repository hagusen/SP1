using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //spelarobjektet

    public float smoothDampTime = 0.05f; //styrka på damp vid närmande av target

    private Vector3 smoothDampVelocity = Vector3.zero;
    private bool camLock = false;

    public float boundsOffset = 0.5f; //längden på bounds utanför kameran (y-led)

    [SerializeField]
    public float width, height;
    [SerializeField]
    public float boundary;

    private void Start()
    {
        //hämtar höjd och bredd men funkar bara på ortografisk cam, kan ändra om behövs
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        UpdateBounds();
    }

    private void Update()
    {
        StopIfFalling();
        SmoothMovement();
        UpdateBounds();
    }

    private void SmoothMovement()
    {
        if (target)
        {
            if (!camLock)
            {
                float targetY = target.position.y;
                float y = Mathf.SmoothDamp(transform.position.y, targetY, ref smoothDampVelocity.y, smoothDampTime);

                transform.position = new Vector3(0, y, transform.position.z);
            }

            if (camLock)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }
    }

    private void StopIfFalling()
    {
        if (target.position.y < transform.position.y)
        {
            camLock = true;
        }
        if (target.position.y >= transform.position.y)
        {
            camLock = false;
        }
    }     
    
    void UpdateBounds()
    {
        boundary = transform.position.y - height / 2 - boundsOffset; //uppdaterar boundary under spelaren (-y-led)
    }
}
