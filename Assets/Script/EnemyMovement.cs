using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public float EnemyVelocity = 5;

    // Start is called before the first frame update
    void Start()
    {
        var xVelocity = Random.value * EnemyVelocity * 2 - EnemyVelocity;
        var zVelocity = Random.value * EnemyVelocity * 2 - EnemyVelocity;
        Rigidbody.velocity = new Vector3(xVelocity, 0, zVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody.AddForce(0, 0, EnemyVelocity * Time.deltaTime, ForceMode.Acceleration);
    }
}
