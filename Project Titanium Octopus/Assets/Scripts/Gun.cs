using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject camera;
    public GameObject bullet;
    public GameObject player;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create Bullet
        GameObject tempBullet = Instantiate(bullet);
        tempBullet.SetActive(true);
        
        Vector3 pos = bullet.GetComponent<Transform>().position;

        tempBullet.GetComponent<Transform>().position = new Vector3(pos.x, pos.y, pos.z);
        tempBullet.GetComponent<Transform>().rotation = bullet.GetComponent<Transform>().rotation;

        tempBullet.GetComponent<Transform>().localScale = new Vector3(7.5f, 7.5f, 7.5f);

        // Impart velocity to bullet

        tempBullet.GetComponent<Rigidbody>().AddForce(1000.0f * (camera.GetComponent<Transform>().forward));

    }
}
