using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    public GameObject Camera;
    public GameObject StairsDown;
    public HeroStuff HeroStuff;

    public LabGenerator LabGenerator;

    public float HeroSpeed = 1f;

    public float LookAngel { get; private set; } = 0;

    Animator Animator;
    Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SetVelocityToHero();

        CalculatedAnimationVariable();

        //Debug only
        if (Input.GetKey(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == StairsDown)
        {
            if (SceneManager.GetActiveScene().name == "Store")
            {
                HeroStuff.Save();
                SceneManager.LoadScene("Labirinth");
                return;
            }

            //Each X level is a store
            if (LabGenerator.DepthOfCurrentLevel % 5 == 0)
            {
                PlayerPrefs.SetInt("DepthOfCurrentLevel", LabGenerator.DepthOfCurrentLevel);
                HeroStuff.Save();
                SceneManager.LoadScene("Store");
                return;
            }

            var heroX = Mathf.RoundToInt(transform.position.x);
            var heroZ = Mathf.RoundToInt(transform.position.z);
            transform.position = new Vector3(heroX, 0, heroZ);
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            LabGenerator.GenerateLabyrinth(heroX, heroZ);
        }
    }

    private void CalculatedAnimationVariable()
    {
        //var lookUp = Animator.GetBool("LookUp");
        //var lookRight = Animator.GetBool("LookRight");

        //if (Input.GetKey(KeyCode.W)
        //    || Input.GetKey(KeyCode.S))
        //{
        //    if (lookRight)
        //    {
        //        Animator.SetTrigger("TurnAround");
        //    }

        //    Animator.SetBool("KeepGoing", true);
        //    Animator.SetBool("LookUp", true);
        //    Animator.SetBool("LookRight", false);
        //}

        //if (Input.GetKey(KeyCode.A)
        //    || Input.GetKey(KeyCode.D))
        //{
        //    if (lookUp)
        //    {
        //        Animator.SetTrigger("TurnAround");
        //    }

        //    Animator.SetBool("KeepGoing", true);
        //    Animator.SetBool("LookUp", false);
        //    Animator.SetBool("LookRight", true);
        //}

        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S))
        {
            Animator.SetBool("KeepGoing", true);
        }

        if (!Input.anyKey)
        {
            Animator.SetBool("KeepGoing", false);
        }
    }

    public void RotateHero(float angle)
    {
        LookAngel += angle;
        transform.eulerAngles = new Vector3(0, LookAngel, 0);
    }

    private void SetVelocityToHero()
    {
        Rigidbody.velocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.UpArrow))
        {
            Rigidbody.velocity = new Vector3(0, 0, 1) * HeroSpeed;
        }
        if (Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.DownArrow))
        {
            Rigidbody.velocity = new Vector3(0, 0, -1) * HeroSpeed;
        }

        if (Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.LeftArrow))
        {
            Rigidbody.velocity += new Vector3(-1, 0, 0) * HeroSpeed;
        }
        if (Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.RightArrow))
        {
            Rigidbody.velocity += new Vector3(1, 0, 0) * HeroSpeed;
        }


        Rigidbody.velocity = Quaternion.Euler(0, LookAngel, 0) * Rigidbody.velocity;
    }
}
