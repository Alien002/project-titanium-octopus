using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private GameObject gameDirector;

    private float speed = .02f;
    private float searchDist = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        Vector3 curPos = this.GetComponent<Transform>().position;

        if (Vector3.Distance(playerPos, curPos) <= searchDist)
        {
            this.GetComponent<Transform>().position = Vector3.MoveTowards(curPos, playerPos, speed);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Player"))
        {
            gameDirector.GetComponent<GameDirector>().restartLevel();
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
