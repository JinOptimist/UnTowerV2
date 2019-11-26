using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject EnemyTemplate;
    public float CooldownOfRespawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, CooldownOfRespawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GenerateEnemy()
    {
        var enemy = Instantiate(EnemyTemplate);
        enemy.transform.position = transform.position;
        enemy.SetActive(true);
        return enemy;
    }

    private void SpawnEnemy()
    {
        var enemy = GenerateEnemy();
    }



    //private void ShotToEnemy()
    //{
    //    var bullet = GenerateBullet();
    //    var enemies = GameObject.FindGameObjectsWithTag(ConstStore.EnemyTag);
    //    if (enemies.Length > 0)
    //    {
    //        var goal = enemies[0];

    //        var shotRigid = bullet.GetComponent<Rigidbody>();

    //        var goalPosition = goal.GetComponent<Rigidbody>().position;
    //        var vectorDirection = goalPosition - shotRigid.position;
    //        var speedRectification = vectorDirection.magnitude / SpeedOfShoot;
    //        vectorDirection = vectorDirection / speedRectification;
    //        shotRigid.velocity = vectorDirection;

    //        shotRigid.transform.LookAt(goalPosition);
    //        shotRigid.transform.Rotate(90, 0, 0);
    //    }
    //}
}
