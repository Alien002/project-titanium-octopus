using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private string curRoom;
    private GameObject room;

    private int numEnemies;

    private Vector3 playerStartingPos;
    private Quaternion playerStartingRot;

    private bool isRestart;
    private bool isWaiting; // Usually to spawn

    private float timer;

    private int enemiesKilled;
    private int maxEnemies;

    // Start is called before the first frame update
    void Start()
    {
        playerStartingPos = player.GetComponent<Transform>().position;
        playerStartingRot = player.GetComponent<Transform>().rotation;

        curRoom = player.GetComponent<PlayerController>().getRoom();
        numEnemies = 0;
        Random.seed = System.Environment.TickCount;

        isRestart = false;
        isWaiting = false;

        enemiesKilled = 0;
        maxEnemies = 1;
    }

    // Update is called once per frame
    void Update()
    {
        curRoom = player.GetComponent<PlayerController>().getRoom();

        // Checks if recently restarted. If not done, enemy spawns as player restarts, which is not intended
        if(isRestart)
        {
            timer += Time.deltaTime;
            if(timer >= 1.0f)
            {
                timer = 0.0f;
                isRestart = false;
            }
        }
        else if(isWaiting)
        {
            timer += Time.deltaTime;
            if (timer >= 5.0f)
            {
                timer = 0.0f;
                isWaiting = false;
                maxEnemies++;
            }
        }
        else if(string.Compare(curRoom, "StartingRoom_DEMO") != 0)
        {
            print(numEnemies);
            if (numEnemies == 0)
            {
                for (int i = 0; i < maxEnemies; i++)
                {
                    numEnemies++;
                    int spawnInd = Random.Range(0, 4);
                    Vector3 spawnCoord = GameObject.Find(curRoom + "/EnemySpawns/EnemySpawn" + spawnInd).GetComponent<Transform>().position;
                    //spawnCoord.y += 3;

                    Instantiate(enemy, spawnCoord, Quaternion.identity, GameObject.Find(curRoom).GetComponent<Transform>());
                }
            }
        }
    }

    public void enemyKilled()
    {
        numEnemies--;
        enemiesKilled++;
        isWaiting = true;
        timer = 0.0f;
    }

    // Restarts level and destroys passed object
    public void restartLevel()
    {
        isRestart = true;
        timer = 0.0f;

        player.GetComponent<Transform>().position = playerStartingPos;
        player.GetComponent<Transform>().rotation = playerStartingRot;
        curRoom = "RoomStart";
        numEnemies = 0;
    }
}
