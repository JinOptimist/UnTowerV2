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
        var xVelocity = GetRandomInRange();
        var zVelocity = GetRandomInRange();
        var v = new Vector3(xVelocity, 0, zVelocity);
        //var v = new Vector3(0, 0, EnemyVelocityMin);
        Rigidbody.velocity = v;
        //Rigidbody.AddForce(v);
    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody.AddForce(0, 0, EnemyVelocity * Time.deltaTime, ForceMode.Acceleration);
    }

    private float GetRandomInRange()
    {
        var range = 2 * (EnemyVelocityMax - EnemyVelocityMin);
        var answer = Random.value * range;
        if (answer < 3)
        {
            answer -= EnemyVelocityMin;
        }
        else
        {
            answer += EnemyVelocityMin / 2;
        }

        return answer;
    }
}
