using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerGenerateBulet : MonoBehaviour
{
    public GameObject BulletTemplate;

    public float CooldownOfShoot = 1;
    public float SpeedOfShoot = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShotToEnemy", 0, CooldownOfShoot);
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
        var enemies = GameObject.FindGameObjectsWithTag(ConstStore.EnemyTag);
        if (enemies.Length > 0)
        {
            var bullet = GenerateBullet();
            var goal = enemies.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();
            //var goal = enemies[0];

            var shotRigid = bullet.GetComponent<Rigidbody>();

            var goalPosition = goal.GetComponent<Rigidbody>().position;
            var vectorDirection = goalPosition - shotRigid.position;
            var speedRectification = vectorDirection.magnitude / SpeedOfShoot;
            vectorDirection = vectorDirection / speedRectification;
            shotRigid.velocity = vectorDirection;

            shotRigid.transform.LookAt(goalPosition);
            shotRigid.transform.Rotate(90, 0, 0);
        }
    }
}
