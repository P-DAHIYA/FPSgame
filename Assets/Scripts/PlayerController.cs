using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 5f;
    public Camera cam;
    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
    }

    void PlayerMovement()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");

        Vector3 movePlayer = new Vector3(movX,0,movY);
        transform.Translate(movePlayer * Time.deltaTime * speed, Space.Self);
    }

    void PlayerRotation()
    {
        float rotateY = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0,rotateY,0) * sensitivity;
        
        rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));

        
        float rotateX = Input.GetAxisRaw("Mouse Y");
        Vector3 camRotation = new Vector3(rotateX,0,0) * sensitivity;
        cam.transform.Rotate(-camRotation);
        
    }
}
