using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public GameObject healthPack;
    public GameObject ammoPack;
    public GameObject armorPack;
    public GameObject[] pickUps;

    private string curRoom;
    private GameObject room;

    private int numEnemiesAlive;

    private Vector3 playerStartingPos;
    private Quaternion playerStartingRot;

    private bool isRestart;
    private bool isWaiting; // Usually to spawn
    private bool roundOver;

    private float timer;

    private int enemiesKilled;
    private int maxEnemiesAtOnce;
    private int maxEnemiesTotal;
    private int enemiesSpawnedThisRound;

    public int round;

    // Start is called before the first frame update
    void Start()
    {
        playerStartingPos = player.GetComponent<Transform>().position;
        playerStartingRot = player.GetComponent<Transform>().rotation;

        curRoom = player.GetComponent<PlayerController>().getRoom();
        numEnemiesAlive = 0;
        Random.seed = System.Environment.TickCount;

        isRestart = false;
        isWaiting = false;
        roundOver = false;

        pickUps = new GameObject[] { healthPack, armorPack, ammoPack };

        enemiesKilled = 0;

        round = 0;
        enemy.GetComponentInChildren<EnemyHealth>().enemyMaxHealth = 100 + round * 10;
        enemy.GetComponent<EnemyAI>().enemyDamage = 10 + 2 * round;
        maxEnemiesAtOnce = round + 1;
        maxEnemiesTotal = 1 + 3 * round;
        enemiesSpawnedThisRound = 0;
        //newRound();
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
            }
        }
        else if(roundOver)
        {
            timer += Time.deltaTime;
            if (timer >= 10.0f)
            {
                timer = 0.0f;
                roundOver = false;
                Vector3 spawnCoord = GameObject.Find(curRoom).GetComponent<Transform>().position;
                int pickUpInd = Random.Range(0, 3);
                Instantiate(pickUps[pickUpInd], spawnCoord, Quaternion.identity, GameObject.Find(curRoom).GetComponent<Transform>());
            }
        }
        //else if(string.Compare(curRoom, "StartingRoom_DEMO") != 0)
        else
        {
            //print(numEnemies);
            if (numEnemiesAlive == 0)
            {
                if (enemiesSpawnedThisRound < maxEnemiesTotal)
                {
                    for (int i = 0; i < maxEnemiesAtOnce; i++)
                    {
                        numEnemiesAlive++;
                        int spawnInd = Random.Range(0, 4);
                        //Vector3 spawnCoord = GameObject.Find(curRoom + "/EnemySpawns/EnemySpawn" + spawnInd).GetComponent<Transform>().position;
                        Vector3 spawnCoordBounds = GameObject.Find(curRoom).GetComponent<Room>().boxCol.size;
                        Vector3 spawnCoord = GameObject.Find(curRoom).GetComponent<Transform>().position +
                            new Vector3(Random.Range(0, spawnCoordBounds.x - 2), -2, Random.Range(0, spawnCoordBounds.z - 2));
                        //spawnCoord.y += 3;

                        Instantiate(enemy, spawnCoord, Quaternion.identity, GameObject.Find(curRoom).GetComponent<Transform>());
                        enemiesSpawnedThisRound++;
                    }
                }
                else
                {
                    round++;
                    newRound();
                }
            }
        }
    }

    public void enemyKilled()
    {
        numEnemiesAlive--;
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
        numEnemiesAlive = 0;
    }

    // Modifies values for new round
    private void newRound()
    {
        enemy.GetComponentInChildren<EnemyHealth>().enemyMaxHealth = 100 + round * 10;
        enemy.GetComponent<EnemyAI>().enemyDamage = 10 + 2 * round;
        maxEnemiesAtOnce = round;
        maxEnemiesTotal = 1 + 3 * round;
        enemiesSpawnedThisRound = 0;
        roundOver = true;
    }
}
