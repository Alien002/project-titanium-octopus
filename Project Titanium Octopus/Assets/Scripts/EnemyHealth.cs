using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrHealth;
    public int enemyDamage;
    private GameObject gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurrHealth = enemyMaxHealth;
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        enemyDamage = this.gameObject.GetComponent<Transform>().parent.gameObject.GetComponent<EnemyAI>().enemyDamage;
        if (enemyCurrHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyCurrHealth -= damage;
    }

    public void KillEnemy()
    {
        //Destroy(this.gameObject);
        Destroy(this.transform.parent.gameObject);
        gameDirector.GetComponent<GameDirector>().enemyKilled();
        GameObject.Find("Player").GetComponent<PlayerUI>().points += 10;
    }

    public void SetMaxEnemyHealth()
    {
        enemyCurrHealth = enemyMaxHealth;
    }


    void OnCollisionEnter(Collision collision)
    {
        print("enemyhealth");
        if (collision.gameObject.name == ("Player"))
        {
            collision.gameObject.GetComponent<PlayerUI>().currenthealth -= enemyDamage;
        }
        /*if (collider.gameObject.name.Contains("Bullet"))
        {
            print("body");
            DamageEnemy(50);
        }*/
    }
}