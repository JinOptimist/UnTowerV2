using Assets.Script;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
