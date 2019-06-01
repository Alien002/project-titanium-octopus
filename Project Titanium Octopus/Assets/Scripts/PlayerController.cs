using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody rb;

    private string curRoom; //current room

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curRoom = "default";
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = new Vector3(moveDirection.x * speed, 0, moveDirection.z * speed);
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Wall"))  // or if(gameObject.CompareTag("YourWallTag"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.name);
        if (collider.gameObject.name.Contains("Room"))
        {
            curRoom = collider.gameObject.name;
        }
        if (collider.gameObject.name.Contains("enemy"))
        {
            this.GetComponent<PlayerUI>().currenthealth-= collider.gameObject.GetComponent<EnemyAI>().enemyDamage;
            // TODO: Take armor into consideration
        }
        if (collider.gameObject.name.Contains("AmmoPack"))
        {
            this.GetComponent<PlayerUI>().reserve += 45;
        }
        if (collider.gameObject.name.Contains("ArmorPack"))
        {
            // TODO: Increase Armor
        }
        if (collider.gameObject.name.Contains("HealthPack"))
        {
            this.GetComponent<PlayerUI>().currenthealth += 20;
        }
    }

    public string getRoom()
    {
        return curRoom;
    }
}
