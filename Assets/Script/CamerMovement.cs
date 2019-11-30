using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    
    public bool FreeCameraMovement = false;

    #region Free camera
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    float horizontalInput;
    float verticalInput;
    float wheelInput;
    #endregion

    #region Hero look
    public GameObject Hero;
    public float SensitivityX = 30F;
    #endregion

    /// <summary>
    /// Offset for camera from Hero
    /// </summary>
    private Vector3 DefaultCameraOffsetFromHero = new Vector3(0, 4, -3.5f);
    private Quaternion DefaultCameraRotation = Quaternion.Euler(35, 0, 0);
    private float CameraDistance = 3.5f;

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
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                CrazyOffsetCalculating();
            }
            else
            {
                transform.position = Hero.transform.position + DefaultCameraOffsetFromHero;
                transform.rotation = DefaultCameraRotation;
            }
        }
    }

    private void CrazyOffsetCalculating()
    {
        var horizontal = SensitivityX * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.RotateAround(Hero.transform.position, Vector3.up, horizontal);

        var angelInRadianY = transform.rotation.eulerAngles.y / 180 * Mathf.PI;
        var lookToHeroFromBack = -Mathf.PI / 2;
        var angelFromHeroPointOfView = lookToHeroFromBack - angelInRadianY;
        var x = Mathf.Cos(angelFromHeroPointOfView);
        var z = Mathf.Sin(angelFromHeroPointOfView);
        //Correct angel offset
        var offsetVector = new Vector3(x, 0, z);
        //Apply distanse for camera
        offsetVector *= CameraDistance;
        //Apply height of camera
        offsetVector += new Vector3(0, 4, 0);

        transform.position = Hero.transform.position + offsetVector;
    }
}
