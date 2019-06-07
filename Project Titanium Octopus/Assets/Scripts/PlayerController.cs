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
        if (collider.gameObject.name.Contains("HitBox"))
        {
            // print("player hurt");
            //print(collider.gameObject.name);

            int dmg = collider.gameObject.GetComponent<Transform>().parent.GetComponent<EnemyAI>().enemyDamage;

            if (this.GetComponent<PlayerUI>().armor > 0)
                this.GetComponent<PlayerUI>().armor -= dmg / 2;
            else
                this.GetComponent<PlayerUI>().currenthealth -= dmg;

            collider.gameObject.GetComponent<Transform>().parent.GetComponent<EnemyAI>().hitPlayer = true;
            //collider.gameObject.GetComponent<EnemyHealth>().enemyDamage;
            //print(this.GetComponent<PlayerUI>().currenthealth);
            // TODO: Take armor into consideration

            Vector3 impactDirection = new Vector3(collider.GetComponent<Transform>().position.x - this.GetComponent<Transform>().position.x, 0.0f, 
                collider.GetComponent<Transform>().position.z - this.GetComponent<Transform>().position.z);

            this.GetComponent<ImpactReceiver>().AddImpact(-1 * impactDirection, 50.0f);

        }
        if (collider.gameObject.name.Contains("Room"))
        {
            print(collider.gameObject.name);
            curRoom = collider.gameObject.name;
        }
        if (collider.gameObject.name.Contains("Ammo"))
        {
            print("pickup");
            this.GetComponent<PlayerUI>().reserve += 15;
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.name.Contains("Armor"))
        {
            print("pickup");
            // TODO: Increase Armor
            this.GetComponent<PlayerUI>().armor += 15;
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.name.Contains("Health"))
        {
            print("pickup");
            if (this.GetComponent<PlayerUI>().currenthealth >= 100)
                this.GetComponent<PlayerUI>().currenthealth += 5;
            else
                this.GetComponent<PlayerUI>().currenthealth += 20;
            Destroy(collider.gameObject);
        }
    }

    public string getRoom()
    {
        return curRoom;
    }
}
