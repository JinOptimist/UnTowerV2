using Assets.Script;
using System.Linq;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private float SpeedOfShoot = 9;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag(ConstStore.EnemyTag);
        if (enemies.Length > 0)
        {
            var bullet = this.gameObject;
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

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet colide");
        if (collision.gameObject.CompareTag(ConstStore.EnemyTag))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(ConstStore.GroundTag))
        {
            Destroy(gameObject);
        }
    }
}
