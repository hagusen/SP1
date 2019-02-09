using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    public CameraController cam;
    public Hellfire hellfire;

    public Vector2 lastCheckpointPosition;
    public Vector2 hellfirePosition;

    private void Start()
    {
        CameraController cam = GetComponent<CameraController>();
        Hellfire hellfire = GetComponent<Hellfire>();
    }

    void Update ()
    {
        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (transform.position.y < cam.boundary)
        {
            RespawnAtLastCheckPointPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hellfire"))
        {
            RespawnAtLastCheckPointPosition();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RespawnAtLastCheckPointPosition();
        }
    }

    void RespawnAtLastCheckPointPosition()
    {
        transform.position = lastCheckpointPosition;

        if(cam.transform.position.y > 0)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, lastCheckpointPosition.y, cam.transform.position.z);
        }       

        hellfire.transform.position = hellfirePosition;
    }
}
