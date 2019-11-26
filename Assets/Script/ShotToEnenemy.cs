using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotToEnenemy : MonoBehaviour
{
    public GameObject BulletTemplate;
    public float SpeedOfShoot = 1;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Start");
        InvokeRepeating("ShotToEnemy", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject GenerateBullet()
    {
        var bullet = Instantiate(BulletTemplate);
        bullet.transform.position = transform.position + new Vector3(0, 5, 0);
        bullet.SetActive(true);
        Debug.Log($"Bullet generate: {bullet.transform.position}");
        return bullet;
    }

    private void ShotToEnemy()
    {
        Debug.Log($"Shoot");
        var bullet = GenerateBullet();
        var enemies = GameObject.FindGameObjectsWithTag(ConstStore.EnemyTag);
        if (enemies.Length > 0)
        {
            var goal = enemies[0];

            var shotRigid = bullet.GetComponent<Rigidbody>();

            var goalPosition = goal.GetComponent<Rigidbody>().position;
            var vectorDirection = goalPosition - shotRigid.position;
            var speedRectification = vectorDirection.magnitude / SpeedOfShoot;
            vectorDirection = vectorDirection / speedRectification;
            shotRigid.velocity = vectorDirection;

            transform.LookAt(goalPosition);
            transform.Rotate(90, 0, 0);
        }
    }
}
