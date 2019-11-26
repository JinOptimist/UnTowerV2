using Assets.Script;
using UnityEngine;

public class ShotMovement : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            Destroy(this);
        }
    }
}
