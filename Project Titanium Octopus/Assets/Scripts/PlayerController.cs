using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = .2f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(movement * speed);
        transform.Translate(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Wall"))  // or if(gameObject.CompareTag("YourWallTag"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
