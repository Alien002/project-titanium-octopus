using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private GameObject gameDirector;

    private float speed = .05f;
    private float searchDist = 20.0f;

    public int enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameDirector = GameObject.Find("GameDirector");
        enemyDamage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        Vector3 curPos = this.GetComponent<Transform>().position;

        if (Vector3.Distance(playerPos, curPos) <= searchDist)
        {
            Vector3 moveVec = Vector3.MoveTowards(curPos, playerPos, speed * Time.deltaTime);
            this.GetComponent<Transform>().position = new Vector3(moveVec.x, this.GetComponent<Transform>().position.y, moveVec.z);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Player"))
        {
            //gameDirector.GetComponent<GameDirector>().restartLevel();
            Destroy(this.gameObject);
        }

        if (collider.gameObject.name.Contains("Bullet"))
        {
            gameDirector.GetComponent<GameDirector>().enemyKilled();
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            gameDirector.GetComponent<GameDirector>().enemyKilled();
            Destroy(this.gameObject);
        }
    }
}
