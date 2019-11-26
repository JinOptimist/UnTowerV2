using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public float EnemyVelocityMin = 5;
    public float EnemyVelocityMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        var xVelocity = Random.value * (EnemyVelocityMax - EnemyVelocityMin) + EnemyVelocityMin;
        var zVelocity = Random.value * (EnemyVelocityMax - EnemyVelocityMin) + EnemyVelocityMin;
        var v = new Vector3(xVelocity, 0, zVelocity);
        //Rigidbody.velocity = v;
        Rigidbody.AddForce(v);
    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody.AddForce(0, 0, EnemyVelocity * Time.deltaTime, ForceMode.Acceleration);
    }
}
