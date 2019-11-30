using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    public GameObject Camera;
    public GameObject StairsDown;

    public LabGenerator LabGenerator;

    public float HeroSpeed = 1f;

    [Header("UI elements")]
    public Text CoinCountText;

    Animator Animator;
    Rigidbody Rigidbody;

    
    private int HeroCoin = 0;
    
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            var coin = collision.gameObject;
            //After animation coin will be destroyed
            coin.GetComponent<Animator>().Play("CoinDestroy");
            //Remove a few components from coin object. 
            //After that, the Hero can go through the Coin
            Destroy(coin.GetComponent<Rigidbody>());
            Destroy(coin.GetComponent<CapsuleCollider>());

            CoinCountText.text = (++HeroCoin).ToString();
        }

        if (collision.gameObject == StairsDown)
        {
            var heroX = Mathf.RoundToInt(transform.position.x);
            var heroZ = Mathf.RoundToInt(transform.position.z);
            transform.position = new Vector3(heroX, 0, heroZ);
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            LabGenerator.GenerateLabyrinth(heroX, heroZ);
        }
    }

    private void CalculatedAnimationVariable()
    {
        var lookUp = Animator.GetBool("LookUp");
        var lookRight = Animator.GetBool("LookRight");

        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S))
        {
            if (lookRight)
            {
                Animator.SetTrigger("TurnAround");
            }

            Animator.SetBool("KeepGoing", true);
            Animator.SetBool("LookUp", true);
            Animator.SetBool("LookRight", false);
        }

        if (Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D))
        {
            if (lookUp)
            {
                Animator.SetTrigger("TurnAround");
            }

            Animator.SetBool("KeepGoing", true);
            Animator.SetBool("LookUp", false);
            Animator.SetBool("LookRight", true);
        }

        if (!Input.anyKey)
        {
            Animator.SetBool("KeepGoing", false);
        }
    }

    private void SetVelocityToHero()
    {
        Rigidbody.velocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody.velocity = new Vector3(0, 0, 1) * HeroSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody.velocity = new Vector3(0, 0, -1) * HeroSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody.velocity += new Vector3(-1, 0, 0) * HeroSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody.velocity += new Vector3(1, 0, 0) * HeroSpeed;
        }
    }
}
