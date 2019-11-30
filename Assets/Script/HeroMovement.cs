using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    public GameObject Camera;

    public float HeroSpeed = 1f;
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

    //private void FixedUpdate()
    // Update is called once per frame
    void Update()
    {
        SetVelocityToHero();

        CalculatedAnimationVariable();

        Camera.transform.position = transform.position + new Vector3(0, 4, -3.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            var coin = collision.gameObject;
            coin.GetComponent<Animator>().Play("CoinDestroy");
            //coin.compo
            //Destroy(coin);

            CoinCountText.text = (++HeroCoin).ToString();
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
