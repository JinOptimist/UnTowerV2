using Assets.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    public GameObject Hero;
    public GameObject MinimapCamera;

    public bool FreeCameraMovement = false;
    public float CameraHeight = 4f;
    public float CameraDistance = 3.5f;

    /// <summary>
    /// For look around with space
    /// </summary>
    public float SensitivityX = 30F;

    #region Free camera
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;
    #endregion

    private Quaternion DefaultCameraRotation = Quaternion.Euler(35, 0, 0);

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (FreeCameraMovement)
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
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
                var horizontal = SensitivityX * Input.GetAxis("Mouse X") * Time.deltaTime;
                transform.RotateAround(Hero.transform.position, Vector3.up, horizontal);

                CrazyOffsetCalculating();
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    RotateCamera(90);
                }

                if (Input.GetKeyUp(KeyCode.Q))
                {
                    RotateCamera(-90);
                }

                transform.rotation = DefaultCameraRotation;
                CrazyOffsetCalculating();
            }
        }
    }

    private void RotateCamera(float angel)
    {
        transform.RotateAround(Hero.transform.position, Vector3.up, angel);
        var heroMovement = Hero.GetComponent<HeroMovement>();
        heroMovement.RotateHero(angel);
        DefaultCameraRotation = transform.rotation;

        MinimapCamera.transform.eulerAngles = new Vector3(90, heroMovement.LookAngel, 0);
    }

    private void CrazyOffsetCalculating()
    {
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
        offsetVector += new Vector3(0, CameraHeight, 0);

        transform.position = Hero.transform.position + offsetVector;
    }
}
