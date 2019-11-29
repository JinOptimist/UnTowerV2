using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    public bool FreeCameraMovement = false;

    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    float horizontalInput;
    float verticalInput;
    float wheelInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        wheelInput = Input.GetAxis("Mouse ScrollWheel");
    }

    void FixedUpdate()
    {
        if (FreeCameraMovement)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || verticalInput != 0)
            {
                transform.position += moveSpeed * new Vector3(horizontalInput, 0, verticalInput);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                //Check that we stil upper than ground
                if (transform.position.y + -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed > 0)
                {
                    transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
                }
            }
        }
        

        //Debug.Log($"Camera position = {transform.position}");
    }
}
