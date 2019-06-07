using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private GameObject gameDirector;

    private float speed = 2f;
    private float searchDist = 20.0f;

    public int enemyDamage;

    public bool hitPlayer;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameDirector = GameObject.Find("GameDirector");
        enemyDamage = 10;

        hitPlayer = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        Vector3 curPos = this.GetComponent<Transform>().position;

        Transform playerTransform = player.GetComponent<Transform>();

        if (hitPlayer)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                hitPlayer = false;
            }
        }
        else
        {
            //Vector3 moveVec = Vector3.MoveTowards(curPos, playerPos, speed * Time.deltaTime);
            //this.GetComponent<Transform>().position = new Vector3(moveVec.x, this.GetComponent<Transform>().position.y, moveVec.z);

            NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

            if (Vector3.Distance(playerPos, curPos) >= 15)
                agent.speed = 10.0f;
            else
                agent.speed = 2.0f;

            agent.destination = playerTransform.position;
        }
        this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x, -2.06f, this.GetComponent<Transform>().position.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        /*if (collider.gameObject.name.Contains("Player"))
        {
            //gameDirector.GetComponent<GameDirector>().restartLevel();
            Destroy(this.gameObject);
        }*/

        /*if (collider.gameObject.name.Contains("Bullet"))
        {
            gameDirector.GetComponent<GameDirector>().enemyKilled();
            Destroy(this.gameObject);
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.name.Contains("Bullet"))
        {
            gameDirector.GetComponent<GameDirector>().enemyKilled();
            Destroy(this.gameObject);
        }*/
    }
}
